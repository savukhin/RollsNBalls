using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum gameModesEnum {
    Ball = 1,
    Car = 2,
    Plane = 3
}

public class generalWorld : MonoBehaviour
{
    [Serializable]
    public struct Events {
        public UnityEngine.Events.UnityEvent pauseEvent;
        public UnityEngine.Events.UnityEvent resumeEvent;
        public UnityEngine.Events.UnityEvent gameOverEvent;
        public UnityEngine.Events.UnityEvent winEvent;
        public UnityEngine.Events.UnityEvent restartEvent;
    }
    public Events events;
    public ControllerRouter player;
    public Boss boss;
    //[System.NonSerialized]
    public gameModesEnum gameMode;
    [SerializeField]
    private mapGeneration generator = null;
    public GameObject HUD;
    public GameObject mainMenu;
    public GameObject gameOverBanner;
    public int maxPlayerHealthPoints = 3;
    private int playerHealthPoints; 
    public int moneyPoints = 0;
    public int score = 0;

    public void startGame() {
        player.startMoving();
        playerHealthPoints = maxPlayerHealthPoints;
        updateHUD();
        generator.pause = false;
        HUD.SetActive(true);
        mainMenu.SetActive(false);
        boss.Activate();
        StartCoroutine("ScoreUpdate");
    }

    public void pause()
    {
        events.pauseEvent.Invoke();
        StopCoroutine("ScoreUpdate");
        Time.timeScale = 0;
    }

    public void resume()
    {
        events.resumeEvent.Invoke();
        StartCoroutine("ScoreUpdate");
        Time.timeScale = 1;
    }

    public void gameOver()
    {
        events.gameOverEvent.Invoke();
        StopCoroutine("ScoreUpdate");
    }

    public void win()
    {
        events.gameOverEvent.Invoke();
        StopCoroutine("ScoreUpdate");
    }

    public void updateHUD()
    {
        var controller = HUD.GetComponent<HUDController>();
        controller.updateHealthPoints(player.healthPoints);
        controller.updateMoneyPoints(player.moneyPoints);
        controller.updateBossHealthPoints(boss.healthPoints);
        controller.updateBossMaxHealthPoints(boss.maxHealthPoints);
    }

    IEnumerator ScoreUpdate() {
        for (;;) {
            score++;
            generator.speed += 0.1f;
            HUD.GetComponent<HUDController>().updateScore(score);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        generator.gameMode = gameMode;
        player.changeGameMode(gameMode);
        generator.initializeGeneration();
        score = 0;
    }

    public void restart() 
    {
        Start();
        events.restartEvent.Invoke();
    }
}
