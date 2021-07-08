using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private float desiredRot;
    public float rotSpeed = 10;
    public float maxSpeed = 10;
    
    // Update is called once per frame
    void Update()
    {
        if (desiredRot > maxSpeed && rotSpeed > 0)
            rotSpeed *= -1;
        if (desiredRot < -maxSpeed && rotSpeed < 0)
            rotSpeed *= -1;
        desiredRot += rotSpeed * Time.deltaTime;
        
        var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, desiredRot, transform.eulerAngles.z);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime);
    }
}
