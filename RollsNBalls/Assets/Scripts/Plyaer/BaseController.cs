using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    //public GameObject model;
    public generalWorld world;

    protected void gameOver()
    {
        world.gameOver();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Obstacle")
            world.takeDamage(1);
    }

    public void restart()
    {
        Start();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
