using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRouter : MonoBehaviour
{
    private gameModesEnum gameMode = gameModesEnum.Ball;
    public CarController car;
    public BallController ball;

    public void startMoving() {
        ball.enabled = true;
        car.enabled = true;
    }

    public void stopMoving() {
        ball.enabled = false;
        car.enabled = false;
    }

    public void changeGameMode(gameModesEnum mode) {
        gameMode = mode;
        switch (gameMode)
        {
            case gameModesEnum.Ball:
                ball.gameObject.SetActive(true);
                car.gameObject.SetActive(false);
                break;
            case gameModesEnum.Car:
                ball.gameObject.SetActive(false);
                car.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stopMoving();
    }

    public void restart() {
        switch (gameMode)
        {
            case gameModesEnum.Ball:
                ball.restart();
                break;
            case gameModesEnum.Car:
                car.restart();
                break;
            default:
                break;
        }
        Start();
    }
}
