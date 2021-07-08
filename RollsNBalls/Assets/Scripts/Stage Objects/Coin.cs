using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int cost;

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player")
            Destroy(gameObject);
    }
}
