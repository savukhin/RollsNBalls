using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike1 : BaseStrike
{
    public float strikeDelay = 0.5f;
    public GameObject spotPointPrefab;
    public GameObject spotLinePrefab;
    private GameObject missle1;
    private GameObject missle2;
    private GameObject spot;
    
    public override void Launch(Transform from, gameModesEnum gameMode)
    {
        base.Launch(from, gameMode);
        missle1 = Instantiate(modelPrefab, from.position, Quaternion.Euler(-90, 0, 0));
        var rigidbody = missle1.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rigidbody.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);
        rigidbody.useGravity = false;

        if (gameMode == gameModesEnum.Car) {
            int position = Random.Range(-1, 2) * 2;
            spot = Instantiate(spotPointPrefab, new Vector3(position, 0.5f, 0), Quaternion.identity);
        }
        else if (gameMode == gameModesEnum.Plane)
        {
            float positionX = Random.Range(-3f, 3f);
            float positionY = Random.Range(1f, 3f);
            spot = Instantiate(spotLinePrefab, new Vector3(positionX, positionY, 0), Quaternion.identity);
        }
        else
        {
            float positionX = Random.Range(-3f, 3f);
            float positionY = Random.Range(1f, 3f);
            spot = Instantiate(spotLinePrefab, new Vector3(positionX, positionY, 0), Quaternion.identity);
        }
        Invoke("Strike", strikeDelay);
    }

    void Strike() {
        missle2 = Instantiate(modelPrefab);
        var rigidbody = missle2.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rigidbody.useGravity = false;
        if (gameMode == gameModesEnum.Car) {
            missle2.transform.position = new Vector3(0, 10f, 0);
            missle2.transform.rotation = Quaternion.Euler(90, 0, 0);
            missle2.GetComponent<Rigidbody>().AddForce(new Vector3(0, -15f, 0), ForceMode.VelocityChange);
        }
        else 
        {
            missle2.transform.position = spot.transform.position + new Vector3(0, 0, 50f);
            missle2.transform.rotation = Quaternion.Euler(180, 0, 0);
            missle2.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -10f), ForceMode.VelocityChange);
            missle2.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -10f), ForceMode.Impulse);
        }
        missle2.GetComponent<Missle1>().onBump.AddListener(Bump);
    }

    public override void Destroy() {
        Destroy(spot);
        Destroy(missle1);
        Destroy(missle2);
        Destroy(gameObject);
    }

    void Bump()
    {
        this.Destroy();
    }

    public override void Pause()
    {
        base.Pause();
        missle1.GetComponent<Missle1>().Pause();
        if (missle2)
            missle2.GetComponent<Missle1>().Pause();
    }

    public override void Resume()
    {
        base.Resume();
        missle1.GetComponent<Missle1>().Resume();
        if (missle2)
            missle2.GetComponent<Missle1>().Resume();
    }
}
