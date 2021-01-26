using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalWorld : MonoBehaviour
{
    [SerializeField]
    private mapGeneration generator = null;

    public void gameOver()
    {
        generator.stop();
        print("Game Over");
    }
}
