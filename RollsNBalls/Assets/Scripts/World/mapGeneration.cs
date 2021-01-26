using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : MonoBehaviour
{
    public GameObject[] carStagesPrefabs;
    public GameObject currentStage;
    private GameObject nextStage;
    public float speed = 4f;
    public bool pause = false;

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
		nextStage = Instantiate(carStagesPrefabs[Random.Range(0, 2)], currentStage.transform.position, currentStage.transform.rotation);
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

    // Start is called before the first frame update
    void Start()
    {
        generateRandomStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
            moveForward();        
    }
}
