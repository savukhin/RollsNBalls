using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [System.Serializable]
    public struct Events {
        public UnityEngine.Events.UnityEvent takeDamageEvent;
        public UnityEngine.Events.UnityEvent healEvent;
    }
    public Events events;
    public GameObject strikePrefab;
    public int maxHealthPoints;
    [System.NonSerialized]
    public int healthPoints;
    public GameObject model;
    public generalWorld world;
    
    public virtual void takeDamage(int damage = 1)
    {
        healthPoints -= damage;
        world.updateHUD();
    }
}
