using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum spotTypeEnum
{
    Point = 1,
    Line = 2,
}

public class Spot : MonoBehaviour
{
    public spotTypeEnum type = spotTypeEnum.Point;
}
