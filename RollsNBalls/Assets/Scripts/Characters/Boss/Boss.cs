using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseCharacter
{
    public GameObject strikePrefab;
    public float strikeRate;
    public float strikeChance; // value between 0 and 1

    IEnumerator Strike()
    {
        for (;;) {
            float dice = Random.Range(0, 1f);
            if (dice < strikeChance) {
                var strike = Instantiate(strikePrefab);
                strike.GetComponent<BaseStrike>().Launch(transform, world.gameMode);
                world.events.pauseEvent.AddListener(strike.GetComponent<BaseStrike>().Pause);
                world.events.resumeEvent.AddListener(strike.GetComponent<BaseStrike>().Resume);
                world.events.gameOverEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
            }
            yield return new WaitForSeconds(strikeRate);
        }
    }

    public void Activate()
    {
        StartCoroutine("Strike");
    }

    public void Stop()
    {
        StopCoroutine("Strike");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
