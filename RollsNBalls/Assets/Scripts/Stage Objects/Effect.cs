using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTypesEnum
{
    Heal = 1,
    Attack = 2,
}

public class Effect : MonoBehaviour
{
    public EffectTypesEnum type;
    public int multiper = 1;
    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
