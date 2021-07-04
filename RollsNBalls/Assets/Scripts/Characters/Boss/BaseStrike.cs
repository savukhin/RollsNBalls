using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStrike : MonoBehaviour
{
    public GameObject modelPrefab;
    public gameModesEnum gameMode;

    public virtual void Launch(Transform from, gameModesEnum gameMode)
    {
        this.gameMode = gameMode;
    }

    public virtual void Pause(){}

    public virtual void Resume(){}
    public virtual void Destroy(){}
}
