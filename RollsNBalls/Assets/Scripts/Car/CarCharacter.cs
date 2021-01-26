using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCharacter : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Obstacle")
            transform.parent.GetComponent<CarController>().takeDamage();
    }
}
