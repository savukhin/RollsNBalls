using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRouter : BaseCharacter
{
    private gameModesEnum gameMode = gameModesEnum.Ball;
    public GameObject mainCamera;
    public GameObject carPrefab;
    public GameObject ballPrefab;
    public GameObject planePrefab;
    public BaseController controller;
    private Vector3 ballPausedVelocity = new Vector3(0, 0, 0);
    private Vector3 ballPausedAngularVelocity = new Vector3(0, 0, 0);
    public int moneyPoints = 0;

    public void startMoving() {
        controller.startMoving();
        controller.enabled = true;
    }

    public void stopMoving() {
        controller.stopMoving();
    }

    public void changeGameMode(gameModesEnum mode) {
        if (controller)
            Destroy(controller.gameObject);
        gameMode = mode;
        GameObject instance = new GameObject();
        switch (gameMode)
        {
            case gameModesEnum.Ball:
                instance = Instantiate(ballPrefab, transform);
                break;
            case gameModesEnum.Car:
                instance = Instantiate(carPrefab, transform);
                break;
            case gameModesEnum.Plane:
                instance = Instantiate(planePrefab, transform);
                break;
            default:
                break;
        }
        controller = instance.GetComponent<BaseController>();
        controller.mainCamera = mainCamera;
        controller.player = this;
    }

    public void gameOver()
    {
        world.gameOver();
    }

    public override void takeDamage(int damage = 1)
    {
        base.takeDamage(damage);
        if (healthPoints <= 0)
            gameOver();
    }

    public void heal(int count=1)
    {
        healthPoints += count;
        if (healthPoints > maxHealthPoints)
            healthPoints = maxHealthPoints;
        world.updateHUD();
    }

    public void takeMoney(int money = 1)
    {
        moneyPoints += money;
        world.updateHUD();
    }

    public void attack()
    {
        var strike = Instantiate(strikePrefab);
        strike.GetComponent<BaseStrike>().Launch(controller.transform, world.gameMode, world.boss.gameObject);
        world.events.pauseEvent.AddListener(strike.GetComponent<BaseStrike>().Pause);
        world.events.resumeEvent.AddListener(strike.GetComponent<BaseStrike>().Resume);
        world.events.gameOverEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = maxHealthPoints;
        world.updateHUD();
        if (controller)
            stopMoving();
    }

    public void restart() {
        changeGameMode(gameMode);
        Start();
    }
}
