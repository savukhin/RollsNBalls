using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomEffect : MonoBehaviour
{
    public GameObject[] EffectPrefabs;
    [NonSerialized]
    public GameObject currentEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentEffect = Instantiate(EffectPrefabs[UnityEngine.Random.Range(0, EffectPrefabs.Length)], transform);
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player")
            Destroy(gameObject);
    }
}
