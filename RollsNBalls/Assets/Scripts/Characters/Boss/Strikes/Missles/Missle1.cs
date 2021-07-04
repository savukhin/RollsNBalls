using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle1 : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onBump;

    void OnTriggerEnter(Collider collider) {
        onBump.Invoke();
    }
}
