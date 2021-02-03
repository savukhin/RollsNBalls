using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum sideEnum 
{
    Left = -2,
    Middle = 0,
    Right = -2
}

public class CarController : BaseController
{
    private Vector2 touchPosition;
    private int side;
    private bool sliding = false;    

    IEnumerator changeLine(bool left)
    {
        sliding = true;
        if (left)
        {
            for (int i = 2; i < 33; i = (i << 1))
            {
                transform.position -= new Vector3(2.0f / i, 0, 0);
                yield return new WaitForSeconds(0.001f);
            }            
        } else
        {
            for (int i = 2; i < 33; i = (i << 1))
            {
                transform.position += new Vector3(2.0f / i, 0, 0);
                yield return new WaitForSeconds(0.001f);
            }            
        }
        transform.position = new Vector3(side, transform.position.y, transform.position.z);
        sliding = false;
        yield return null;        
    }

    private void slideProcessing() 
    {
        if (Input.GetMouseButtonDown(0)) {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 deltaSwipe = touchPosition - new Vector2(mousePosition.x, mousePosition.y);
            if (Mathf.Abs(deltaSwipe.x) > 50.0f && !sliding)
            {
                side = side + (deltaSwipe.x < 0 ? 2 : -2);

                if (side < -2)
                    side = -2;
                else if (side > 2)
                    side = 2;
                else
                    StartCoroutine("changeLine", deltaSwipe.x > 0);

            }
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        side = (int)sideEnum.Middle;
        transform.position = new Vector3(side, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        slideProcessing();
    }
}
