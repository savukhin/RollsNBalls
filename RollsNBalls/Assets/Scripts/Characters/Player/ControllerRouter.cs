using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRouter : MonoBehaviour
{
    private gameModesEnum gameMode = gameModesEnum.Ball;
    public CarController car;
    public BallController ball;
    public PlaneController plane;
    private Vector3 ballPausedVelocity = new Vector3(0, 0, 0);
    private Vector3 ballPausedAngularVelocity = new Vector3(0, 0, 0);

    public void startMoving() {
        Rigidbody rigidbody = ball.gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = ballPausedVelocity;
        rigidbody.angularVelocity = ballPausedAngularVelocity;
        ball.enabled = true;

        car.enabled = true;
        plane.enabled = true;        
    }

    public void stopMoving() {
        Rigidbody rigidbody = ball.gameObject.GetComponent<Rigidbody>();
        ballPausedVelocity = rigidbody.velocity;
        ballPausedAngularVelocity = rigidbody.angularVelocity;
        rigidbody.isKinematic = true;
        ball.enabled = false;

        car.enabled = false;
        plane.enabled = false;
    }

    public void changeGameMode(gameModesEnum mode) {
        gameMode = mode;
        switch (gameMode)
        {
            case gameModesEnum.Ball:
                ball.gameObject.SetActive(true);
                car.gameObject.SetActive(false);
                plane.gameObject.SetActive(false);
                break;
            case gameModesEnum.Car:
                ball.gameObject.SetActive(false);
                car.gameObject.SetActive(true);
                plane.gameObject.SetActive(false);
                break;
            case gameModesEnum.Plane:
                ball.gameObject.SetActive(false);
                car.gameObject.SetActive(false);
                plane.gameObject.SetActive(true);
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
            case gameModesEnum.Plane:
                plane.restart();
                break;
            default:
                break;
        }
        Start();
    }
}
