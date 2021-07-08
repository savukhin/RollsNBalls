using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float desiredRot;
    public float rotSpeed = 100;
    
    // Update is called once per frame
    void Update()
    {
        desiredRot += rotSpeed * Time.deltaTime;
        
        var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, desiredRot, transform.eulerAngles.z);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime);
    }
}
