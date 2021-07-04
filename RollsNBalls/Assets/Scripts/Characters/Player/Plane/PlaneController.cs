using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : BaseController
{
    private Vector2 touchPosition;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        transform.position = new Vector3(0, 1.4f, 0);
    }

    private void Joystick() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 deltaSwipe = mousePosition - touchPosition;
            touchPosition = mousePosition;

            /*float resistance = 10f;
            deltaSwipe.x = Mathf.Min(deltaSwipe.x, resistance);
            deltaSwipe.x = Mathf.Max(deltaSwipe.x, -resistance);
            deltaSwipe.y = Mathf.Min(deltaSwipe.y, resistance);
            deltaSwipe.y = Mathf.Max(deltaSwipe.y, -resistance);*/
            
            Vector3 direction = new Vector3(deltaSwipe.x, deltaSwipe.y, 0);
            direction.x = direction.x * 1.1f;
            direction.y = direction.y * 0.7f;

            GetComponent<Rigidbody>().velocity = direction;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Joystick();
    }
}
