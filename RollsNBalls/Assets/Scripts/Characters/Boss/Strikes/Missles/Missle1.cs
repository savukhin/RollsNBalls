using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle1 : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onBump;

    private Vector3 _pausedVelocity;
    private Vector3 _pausedAngularVelocity;

    public void Pause() {
        var rigidbody = GetComponent<Rigidbody>();
        _pausedVelocity = rigidbody.velocity;
        _pausedAngularVelocity = rigidbody.angularVelocity;
        rigidbody.isKinematic = true;
    }

    public void Resume() {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = _pausedVelocity;
        rigidbody.angularVelocity = _pausedAngularVelocity;
    }

    void Update() {
        if (transform.position.z < -5) {
            onBump.Invoke();
        }
    }

    void OnTriggerEnter(Collider collider) {
        print(collider);
        if (collider.tag != "Obstacle")
        onBump.Invoke();
    }
}
