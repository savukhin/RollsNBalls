using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum sideEnum 
{
    Left = -2,
    Middle = 0,
    Right = -2
}

public class CarController : MonoBehaviour
{
    public GameObject model;
    public float moveSpeed = 2.4f;
    private Vector2 touchPosition;
    private int side;

    void SlideProcessing() 
    {
        if (Input.GetMouseButtonDown(0)) {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 deltaSwipe = touchPosition - new Vector2(mousePosition.x, mousePosition.y);
            if (Mathf.Abs(deltaSwipe.x) > 50.0f) 
            {
                side = side + (deltaSwipe.x < 0 ? 2 : -2);
                if (side < -2)
                    side = -2;
                if (side > 2)
                    side = 2;
                
                model.transform.position = new Vector3(side, model.transform.position.y, model.transform.position.z);
            }
        }
    }

    void MoveForward() 
    {
        Vector3 direction = new Vector3(0, 0, 1);
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        side = (int)sideEnum.Middle;
        model.transform.position = new Vector3(side, model.transform.position.y, model.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        SlideProcessing();
        MoveForward();
    }
}
