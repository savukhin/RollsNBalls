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

    [System.Serializable] public class Gates {
        public GameObject carGatePrefab;
        public GameObject ballGatePrefab;
        public GameObject planeGatePrefab;
    }
    [SerializeField] public Gates gates;

    private GameObject currentStage;
    private GameObject nextStage;
    public float startSpeed = 4f;
    [System.NonSerialized]
    public float speed = 4f;
    [System.NonSerialized]
    public bool pause = true;
    public float gateChance = 0.1f;

    public void restart() {
        //Destroy(currentStage.gameObject);
        //Destroy(currentStage);
        //Destroy(nextStage.gameObject);
        //currentStage = Instantiate(startStagePrefab, new Vector3(0, 0, 10), startStagePrefab.transform.rotation);
        speed = startSpeed;
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
        if (Random.Range(0f, 1f) < gateChance)
        {
            int ind = (((int)gameMode) - 1 + Random.Range(1, 3)) % 3 + 1;
            switch (ind)
            {
                case 1:
                    nextStage = Instantiate(gates.ballGatePrefab, currentStage.transform.position, currentStage.transform.rotation);
                    break;
                case 2:
                    nextStage = Instantiate(gates.carGatePrefab, currentStage.transform.position, currentStage.transform.rotation);
                    break;
                case 3:
                    nextStage = Instantiate(gates.planeGatePrefab, currentStage.transform.position, currentStage.transform.rotation);
                    break;
            }
            nextStage.transform.position += new Vector3(0, 2f, 0);
        }
        else if (gameMode == gameModesEnum.Car)
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

    
    public void changeGameMode(gameModesEnum mode)
    {
        this.gameMode = mode;
        initializeGeneration();
    }

    public void initializeGeneration() {
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
