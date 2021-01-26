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
    private Vector2 touchPosition;
    private int side;
    private bool moving = false;
    public int healthPoints = 3;
    public generalWorld world;

    private void gameOver()
    {
        world.gameOver();
    }

    IEnumerator changeLine(bool left)
    {
        moving = true;
        if (left)
        {
            for (int i = 2; i < 33; i = (i << 1))
            {
                model.transform.position -= new Vector3(2.0f / i, 0, 0);
                yield return new WaitForSeconds(0.001f);
            }            
        } else
        {
            for (int i = 2; i < 33; i = (i << 1))
            {
                model.transform.position += new Vector3(2.0f / i, 0, 0);
                yield return new WaitForSeconds(0.001f);
            }            
        }
        model.transform.position = new Vector3(side, model.transform.position.y, model.transform.position.z);
        moving = false;
        yield return null;        
    }

    void slideProcessing() 
    {
        if (Input.GetMouseButtonDown(0)) {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 deltaSwipe = touchPosition - new Vector2(mousePosition.x, mousePosition.y);
            if (Mathf.Abs(deltaSwipe.x) > 50.0f && !moving)
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

    //void OnCollisionEnter(Collision collision)
    public void takeDamage(int damage=1)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            gameOver();
        }
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
        slideProcessing();
    }
}
