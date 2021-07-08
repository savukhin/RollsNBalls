using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRouter : BaseCharacter
{
    public class ChosenModels
    {
        public GameObject ball;
        public GameObject car;
        public GameObject plane;
    }
    public ChosenModels chosenModels = new ChosenModels();
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

    public void collideGate(gameModesEnum mode)
    {
        world.changeGameMode(mode);
        this.gameMode = mode;
        changeGameMode(mode);
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
                instance.GetComponent<BallController>().setModel(chosenModels.ball);
                break;
            case gameModesEnum.Car:
                instance = Instantiate(carPrefab, transform);
                instance.GetComponent<CarController>().setModel(chosenModels.car);
                break;
            case gameModesEnum.Plane:
                instance = Instantiate(planePrefab, transform);
                instance.GetComponent<PlaneController>().setModel(chosenModels.plane);
                break;
            default:
                break;
        }
        controller = instance.GetComponent<BaseController>();
        controller.mainCamera = mainCamera;
        controller.player = this;
        controller.setCamera();
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
        world.events.winEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
        world.events.exitEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
    }

    public void setModels(GameObject ballModelPrefab, GameObject carModelPrefab, GameObject planeModelPrefab)
    {
        /*ballPrefab.GetComponent<BallController>().changeModel(ballModelPrefab);
        carPrefab.GetComponent<CarController>().changeModel(carModelPrefab);
        planePrefab.GetComponent<PlaneController>().changeModel(carModelPrefab);*/
        chosenModels.ball = ballModelPrefab;
        chosenModels.car = carModelPrefab;
        chosenModels.plane = planeModelPrefab;
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

    public void Revive()
    {
        changeGameMode(gameMode);
        healthPoints = maxHealthPoints;
    }
}
