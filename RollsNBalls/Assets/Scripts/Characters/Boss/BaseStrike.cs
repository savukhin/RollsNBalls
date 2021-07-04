using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStrike : MonoBehaviour
{
    public GameObject modelPrefab;
    public virtual void Launch(Transform from, gameModesEnum gameMode) {}
}
