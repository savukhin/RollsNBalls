using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStrike : MonoBehaviour
{
    public GameObject modelPrefab;
    public gameModesEnum gameMode;
    public GameObject target;

    public virtual void Launch(Transform from, gameModesEnum gameMode, GameObject target)
    {
        this.gameMode = gameMode;
        this.target = target;
    }

    public virtual void Pause(){}

    public virtual void Resume(){}
    public virtual void Destroy(){}
}
