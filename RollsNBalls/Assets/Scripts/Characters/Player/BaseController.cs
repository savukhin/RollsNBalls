using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : BaseCharacter
{
    public GameObject mainCamera;
    protected bool isGrounded;

    protected void gameOver()
    {
        world.gameOver();
    }

    public void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Obstacle":
                world.takeDamage(1);
                break;
            case "Stage Object":
                if (collider.GetComponent<Coin>() != null)
                {
                    world.takeMoney(collider.GetComponent<Coin>().cost);
                }
                else if (collider.GetComponent<Effect>() != null)
                {
                    var effect = collider.GetComponent<Effect>();
                    switch (effect.type)
                    {
                        case EffectTypesEnum.Heal:
                            world.heal(effect.multiper);
                            break;
                        case EffectTypesEnum.Attack:
                            print("Switching " + effect.type);
                            var strike = Instantiate(strikePrefab);
                            strike.GetComponent<BaseStrike>().Launch(transform, world.gameMode, world.boss.gameObject);
                            world.events.pauseEvent.AddListener(strike.GetComponent<BaseStrike>().Pause);
                            world.events.resumeEvent.AddListener(strike.GetComponent<BaseStrike>().Resume);
                            world.events.gameOverEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
                            break;
                    }
                }
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
