using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : BaseMapGenerator
{
    [System.NonSerialized]
    public gameModesEnum gameMode = gameModesEnum.Ball;
    public GameObject[] carStagesPrefabs;
    public GameObject[] ballStagesPrefabs;
    public GameObject[] planeStagesPrefabs;
    public GameObject[] tutorialSequence;
    private int tutorialSequenceIndex = 0;
    public bool tutorial = false;
    
    [System.Serializable]
    public class StartStagePrefabs
    {
        public GameObject car;
        public GameObject ball;
        public GameObject plane;
    }
    public StartStagePrefabs startStagePrefabs;

    [System.Serializable] public class Gates {
        public GameObject carGatePrefab;
        public GameObject ballGatePrefab;
        public GameObject planeGatePrefab;
    }
    [SerializeField] public Gates gates;

    public float gateChance = 0.1f;

    public void generateTutorial()
    {
        tutorial = true;
        initializeGeneration();
    }

    public void cancelTutorial()
    {
        tutorial = false;
        initializeGeneration();
    }

    protected override void generateNextStage() {
        if (tutorial)
        {
            tutorialSequenceIndex++;
            nextStage = Instantiate(tutorialSequence[tutorialSequenceIndex], currentStage.transform.position, currentStage.transform.rotation);
            if (nextStage.GetComponent<Gate>() != null)
                nextStage.transform.position += new Vector3(0, 2f, 0);
            else if (currentStage.GetComponent<Gate>() != null)
                nextStage.transform.position -= new Vector3(0, 2f, 0);
        }
        else if (Random.Range(0f, 1f) < gateChance)
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
    
    public void changeGameMode(gameModesEnum mode)
    {
        this.gameMode = mode;
        initializeGeneration();
    }

    public override void initializeGeneration() {
        Destroy(currentStage);
        Destroy(nextStage);
        if (tutorial)
        {
            tutorialSequenceIndex = 0;
            currentStage = Instantiate(tutorialSequence[tutorialSequenceIndex], new Vector3(0, 0, 10), 
                                tutorialSequence[tutorialSequenceIndex].transform.rotation);
        }
        else if (gameMode == gameModesEnum.Car)
            currentStage = Instantiate(startStagePrefabs.car, new Vector3(0, 0, 10), startStagePrefabs.car.transform.rotation);
        else if (gameMode == gameModesEnum.Ball)
            currentStage = Instantiate(startStagePrefabs.ball, new Vector3(0, 0, 10), startStagePrefabs.car.transform.rotation);
        else if (gameMode == gameModesEnum.Plane)
            currentStage = Instantiate(startStagePrefabs.plane, new Vector3(0, 0, 10), startStagePrefabs.car.transform.rotation);
        generateNextStage();
    }
}
