using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrike1 : BaseStrike
{
    private GameObject missle;
    
    public override void Launch(Transform from, gameModesEnum gameMode, GameObject boss)
    {
        base.Launch(from, gameMode, boss);
        missle = Instantiate(modelPrefab, from.position, Quaternion.Euler(-90, 0, 0));
        var rigidbody = missle.AddComponent(typeof(Rigidbody)) as Rigidbody;
        if (gameMode != gameModesEnum.Ball)
        {
            rigidbody.AddForce(new Vector3(0, 20, 0), ForceMode.VelocityChange);
        }
        else
        {
            float X = Mathf.Min(5, from.transform.position.x);
            float angle = Mathf.Atan(2 * X / Mathf.Sqrt(5 * 5 - X * X)) * 180 / Mathf.PI;
            angle = Mathf.Min(angle, 60);
            angle = Mathf.Max(angle, -60);
            angle = angle / 3f;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * from.transform.up;
            direction.Normalize();
            rigidbody.AddForce(direction * 20, ForceMode.VelocityChange);
            rigidbody.rotation =  Quaternion.Euler(0, 0, angle);
        }
        rigidbody.useGravity = false;
        missle.GetComponent<Missle1>().onBump.AddListener(Bump);

        if (gameMode == gameModesEnum.Car) {
            int position = Random.Range(-1, 2) * 2;
        }
        else if (gameMode == gameModesEnum.Plane)
        {
            float positionX = Random.Range(-3f, 3f);
            float positionY = Random.Range(1f, 3f);
        }
        else
        {
            float positionX = Random.Range(-3f, 3f);
            float positionY = Random.Range(1f, 3f);
        }
        StartCoroutine("Strike");
    }

    IEnumerator Strike() {
        yield return new WaitForSeconds(0.1f);
        var rigidbody = missle.GetComponent<Rigidbody>();
        var offset = ((target.transform.position - missle.transform.position).normalized - missle.transform.forward).normalized;
        offset = offset * (-20);
        for (int i = 1, j = 1; j < 40; i <<= 1, j++) {
            if (j < 28)
                rigidbody.AddForce(new Vector3(0, -j/20f, 0), ForceMode.VelocityChange);
            
            var direction = (target.transform.position - missle.transform.position).normalized;
            var position = target.transform.position + offset;
            var q = Quaternion.LookRotation(position - missle.transform.position);
            missle.transform.rotation = Quaternion.RotateTowards(missle.transform.rotation, q, 20f * (50 - j) * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 1, j = 1;; i <<= 1, j++) {
            var q = Quaternion.LookRotation(target.transform.position - missle.transform.position);
            missle.transform.rotation = Quaternion.RotateTowards(missle.transform.rotation, q, 400f * i * Time.deltaTime);
            Vector3 direction = missle.transform.forward;
            rigidbody.AddForce(direction*j / 1f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Destroy() {
        Destroy(missle);
        Destroy(gameObject);
    }

    void Bump()
    {
        this.Destroy();
    }

    public override void Pause()
    {
        base.Pause();
        missle.GetComponent<Missle1>().Pause();
    }

    public override void Resume()
    {
        base.Resume();
        missle.GetComponent<Missle1>().Resume();
    }
}
