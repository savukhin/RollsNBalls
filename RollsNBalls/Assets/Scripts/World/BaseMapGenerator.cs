using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMapGenerator : MonoBehaviour
{
    protected GameObject currentStage;
    protected GameObject nextStage;
    public float startSpeed = 5f;
    [System.NonSerialized]
    public float speed = 4f;
    public float deltaSpeed = 0.1f;
    public float maxSpeed = 40f;
    [System.NonSerialized]
    public bool pause = true;

    protected float getStageSize(GameObject stage) {
        return stage.transform.localScale.z * stage.GetComponent<Collider>().bounds.size.z;
    }

    protected virtual void generateNextStage() {}
    public virtual void initializeGeneration() {}


    public void restart() {
        //Destroy(currentStage.gameObject);
        //Destroy(currentStage);
        //Destroy(nextStage.gameObject);
        //currentStage = Instantiate(startStagePrefab, new Vector3(0, 0, 10), startStagePrefab.transform.rotation);
        speed = startSpeed;
        initializeGeneration();
    }
    
    protected virtual void Start()
    {
        speed = startSpeed;
    }

    protected virtual IEnumerator SpeedUpdate() {
        for (;;) {
            speed = Mathf.Min(maxSpeed, speed + deltaSpeed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void stop()
    {
        pause = true;
        StopCoroutine("SpeedUpdate");
    }

    public void resume() {
        pause = false;
        StartCoroutine("SpeedUpdate");
    }

    protected virtual void moveForward() {
        if (currentStage.transform.position.z < -getStageSize(currentStage) / 2 - 5) {
            if (currentStage)
                Destroy(currentStage);
            currentStage = nextStage;
            generateNextStage();
        }
        Vector3 direction = new Vector3(0, 0, -1);
        currentStage.transform.position += direction * speed * Time.deltaTime;
        nextStage.transform.position += direction * speed * Time.deltaTime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!pause)
            moveForward();
    }
}
