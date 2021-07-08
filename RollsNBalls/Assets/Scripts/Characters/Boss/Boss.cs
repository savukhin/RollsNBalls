using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseCharacter
{
    public float strikeRate;
    public float strikeChance; // value between 0 and 1
    private float lastStrikeTime = -100;

    IEnumerator Strike()
    {
        yield return new WaitForSeconds(lastStrikeTime + strikeRate - Time.time);
        for (;;) {
            float dice = Random.Range(0, 1f);
            if (dice < strikeChance) {
                var strike = Instantiate(strikePrefab);
                strike.GetComponent<BaseStrike>().Launch(transform, world.gameMode, null);
                world.events.pauseEvent.AddListener(strike.GetComponent<BaseStrike>().Pause);
                world.events.resumeEvent.AddListener(strike.GetComponent<BaseStrike>().Resume);
                world.events.gameOverEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
                world.events.winEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
                world.events.changeGameModeEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
                world.events.exitEvent.AddListener(strike.GetComponent<BaseStrike>().Destroy);
            }
            lastStrikeTime = Time.time;
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

    public override void takeDamage(int damage = 1)
    {
        base.takeDamage(damage);
        if (healthPoints <= 0)
            world.win();
    }

    public void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Player Missle":
                takeDamage(1);
                break;
      }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = maxHealthPoints;
        world.updateHUD();
    }

    public void restart() {
        Start();
    }
}
