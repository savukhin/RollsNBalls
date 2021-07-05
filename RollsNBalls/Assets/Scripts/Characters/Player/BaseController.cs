using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [System.NonSerialized] public ControllerRouter player;
    [System.NonSerialized] public GameObject mainCamera;
    protected bool isGrounded;

    public virtual void stopMoving() {
        this.enabled = false;
    }

    public virtual void startMoving() {
        this.enabled = true;
    }

    protected void gameOver()
    {
        //world.gameOver();
        player.gameOver();
    }

    public void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Obstacle":
                player.takeDamage(1);
                break;
            case "Boss Missle":
                player.takeDamage(1);
                break;
            case "Stage Object":
                if (collider.GetComponent<Coin>() != null)
                {
                    player.takeMoney(collider.GetComponent<Coin>().cost);
                }
                else if (collider.GetComponent<Effect>() != null)
                {
                    var effect = collider.GetComponent<Effect>();
                    switch (effect.type)
                    {
                        case EffectTypesEnum.Heal:
                            player.heal(effect.multiper);
                            break;
                        case EffectTypesEnum.Attack:
                            player.attack();
                            break;
                    }
                }
                break;
            case "Gate":
                player.collideGate(collider.GetComponent<Gate>().type);
                break;
            default:
                break;
      }
    }
    
    private void OnCollisionExit(Collision other)
    {    
        if (other.collider.transform.tag == "Ground")
            isGrounded = false;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.transform.tag == "Ground")
            isGrounded = true;
    }

    public void restart()
    {
        Start();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCamera.transform.position = new Vector3(-0.15f, 5.28f, -7.94f);
    }

    protected void CameraMovement() 
    {
        mainCamera.transform.position = new Vector3(-0.15f + transform.position.x / 3,
                                mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CameraMovement();
    }
}
