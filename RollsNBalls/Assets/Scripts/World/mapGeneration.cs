using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : MonoBehaviour
{
    [System.NonSerialized]
    public gameModesEnum gameMode = gameModesEnum.Ball;
    public GameObject[] carStagesPrefabs;
    public GameObject[] ballStagesPrefabs;
    public GameObject[] planeStagesPrefabs;
    public GameObject startCarStagePrefab;
    public GameObject startBallStagePrefab;
    public GameObject startPlaneStagePrefab;
    private GameObject currentStage;
    private GameObject nextStage;
    public float startSpeed = 4f;
    [System.NonSerialized]
    public float speed = 4f;
    [System.NonSerialized]
    public bool pause = true;

    public void restart() {
        //Destroy(currentStage.gameObject);
        //Destroy(currentStage);
        //Destroy(nextStage.gameObject);
        //currentStage = Instantiate(startStagePrefab, new Vector3(0, 0, 10), startStagePrefab.transform.rotation);
        initializeGeneration();
    }

    public void stop()
    {
        pause = true;
    }

    public void resume() {
        pause = false;
    }

    float getStageSize(GameObject stage) {
        return stage.transform.localScale.z * stage.GetComponent<Collider>().bounds.size.z;
    }

    void generateRandomStage() {
        if (gameMode == gameModesEnum.Car)
		    nextStage = Instantiate(carStagesPrefabs[Random.Range(0, carStagesPrefabs.Length)], currentStage.transform.position, currentStage.transform.rotation);
        else if (gameMode == gameModesEnum.Ball)
            nextStage = Instantiate(ballStagesPrefabs[Random.Range(0, ballStagesPrefabs.Length)], currentStage.transform.position, currentStage.transform.rotation);
        else if (gameMode == gameModesEnum.Plane)
            nextStage = Instantiate(planeStagesPrefabs[Random.Range(0, planeStagesPrefabs.Length)], currentStage.transform.position, currentStage.transform.rotation);
        nextStage.transform.position += new Vector3(0, 0, getStageSize(currentStage) / 2 + getStageSize(nextStage) / 2);
    }

    void moveForward() {
        if (currentStage.transform.position.z < -getStageSize(currentStage) / 2 - 5) {
            Destroy(currentStage);
            currentStage = nextStage;
            generateRandomStage();
        }
        Vector3 direction = new Vector3(0, 0, -1);
        currentStage.transform.position += direction * speed * Time.deltaTime;
        nextStage.transform.position += direction * speed * Time.deltaTime;
    }

    public void initializeGeneration() {
        speed = startSpeed;
        Destroy(currentStage);
        Destroy(nextStage);
        if (gameMode == gameModesEnum.Car)
            currentStage = Instantiate(startCarStagePrefab, new Vector3(0, 0, 10), startCarStagePrefab.transform.rotation);
        else if (gameMode == gameModesEnum.Ball)
            currentStage = Instantiate(startBallStagePrefab, new Vector3(0, 0, 10), startCarStagePrefab.transform.rotation);
        else if (gameMode == gameModesEnum.Plane)
            currentStage = Instantiate(startPlaneStagePrefab, new Vector3(0, 0, 10), startCarStagePrefab.transform.rotation);
        generateRandomStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
            moveForward();
    }
}
