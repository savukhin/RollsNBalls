using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike1 : BaseStrike
{
    public float strikeDelay = 0.5f;
    public GameObject spotPointPrefab;
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

        missle2 = Instantiate(modelPrefab, new Vector3(0, 10f, 0), Quaternion.Euler(90, 0, 0));
        rigidbody = missle2.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rigidbody.useGravity = false;
        if (gameMode == gameModesEnum.Car) {
            int position = Random.Range(-1, 2) * 2;
            spot = Instantiate(spotPointPrefab, new Vector3(position, 0.5f, 0), Quaternion.identity);
            missle2.transform.position = new Vector3(position, 10f, 0);
        }
        Invoke("Strike", strikeDelay);
    }

    void Strike() {
        missle2.GetComponent<Rigidbody>().AddForce(new Vector3(0, -15f, 0), ForceMode.VelocityChange);
        missle2.GetComponent<Missle1>().onBump.AddListener(Bump);
    }

    void Bump()
    {
        Destroy(spot);
        Destroy(missle1);
        Destroy(missle2);
        Destroy(gameObject);
    }
}
