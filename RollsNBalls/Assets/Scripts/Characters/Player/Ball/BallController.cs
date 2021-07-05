using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : BaseController
{
    public float radius = 5;
    private Vector2 touchPosition;
    private Vector3 pausedVelocity;
    private Vector3 pausedAngularVelocity;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        transform.position = new Vector3(0, 1.5f, 0);
    }

    public override void stopMoving()
    {
        base.stopMoving();
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        pausedVelocity = rigidbody.velocity;
        pausedAngularVelocity = rigidbody.angularVelocity;
        rigidbody.isKinematic = true;
    }

    public override void startMoving() {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = pausedVelocity;
        rigidbody.angularVelocity = pausedAngularVelocity;
    }

    private void Joystick() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && isGrounded)
        {
            Vector3 mousePosition = Input.mousePosition;
            float deltaSwipe = mousePosition.x - touchPosition.x;
            if (Mathf.Abs(deltaSwipe) > 200) {
                touchPosition.x = mousePosition.x - deltaSwipe;
            }

            float X = Mathf.Min(radius, transform.position.x);
            float angle = Mathf.Atan(2 * X / Mathf.Sqrt(radius * radius - X * X)) * 180 / Mathf.PI;
            angle = Mathf.Min(angle, 60);
            angle = Mathf.Max(angle, -60);
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * transform.right;            
            direction.Normalize();

            //Debug.DrawRay(transform.position, transform.TransformDirection(direction) * deltaSwipe / 80f, Color.yellow);

            GetComponent<Rigidbody>().AddForce(direction * deltaSwipe * 3);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Joystick();
    }
}
