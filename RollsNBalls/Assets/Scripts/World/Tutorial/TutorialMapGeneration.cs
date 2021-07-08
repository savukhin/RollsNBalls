using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMapGeneration : BaseMapGenerator
{
    public GameObject[] StageSeuence;
    private int index;

    public override void initializeGeneration() {
        Destroy(currentStage);
        Destroy(nextStage);
        index = 0;
        currentStage = Instantiate(StageSeuence[index], new Vector3(0, 0, 10), Quaternion.identity);
        generateNextStage();
    }

    protected override void generateNextStage()
    {
        index++;
        nextStage = Instantiate(StageSeuence[index], currentStage.transform.position, currentStage.transform.rotation);
        nextStage.transform.position += new Vector3(0, 0, getStageSize(currentStage) / 2 + getStageSize(nextStage) / 2);
    }
}
