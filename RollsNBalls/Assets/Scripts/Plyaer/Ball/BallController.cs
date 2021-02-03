using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : BaseController
{
    public float radius = 5;
    private Vector2 touchPosition;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        transform.position = new Vector3(0, 1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            float deltaSwipe = Mathf.Min(mousePosition.x - touchPosition.x, 250);
            //float deltaSwipe = mousePosition.x - touchPosition.x;
            float X = Mathf.Min(radius, transform.position.x);
            float angle = Mathf.Atan(2 * X / Mathf.Sqrt(radius * radius - X * X)) * 180 / Mathf.PI;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * transform.right;
            direction.Normalize();
            //print("Direction " + direction + " Angle " + angle + " delta " + deltaSwipe);
            GetComponent<Rigidbody>().AddForce(direction * deltaSwipe * 3);
        }
    }
}
