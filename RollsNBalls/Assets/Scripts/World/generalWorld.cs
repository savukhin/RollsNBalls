﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameModesEnum {
    Ball = 1,
    Car = 2,
    Plane = 3
}

public class generalWorld : MonoBehaviour
{
    public ControllerRouter player;
    //[System.NonSerialized]
    public gameModesEnum gameMode;
    [SerializeField]
    private mapGeneration generator = null;
    public GameObject HUD;
    public GameObject mainMenu;
    public GameObject gameOverBanner;
    public int maxHealthPoints = 3;
    private int healthPoints;
    public int moneyPoints = 3;
    public int score = 0;

    public void startGame() {
        player.startMoving();
        healthPoints = maxHealthPoints;
        HUD.GetComponent<HUDController>().updateHealthPoints(healthPoints);
        HUD.GetComponent<HUDController>().updateMoneyPoints(moneyPoints);
        generator.pause = false;
        HUD.SetActive(true);
        mainMenu.SetActive(false);
        StartCoroutine("ScoreUpdate");
    }

    public void pause()
    {
        player.stopMoving();
        generator.stop();
    }

    public void resume()
    {
        player.startMoving();
        generator.resume();
    }

    public void gameOver()
    {
        player.stopMoving();
        generator.stop();
        HUD.SetActive(false);
        gameOverBanner.SetActive(true);
        StopCoroutine("ScoreUpdate");
    }

    public void takeDamage(int damage=1)
    {
        healthPoints -= damage;
        HUD.GetComponent<HUDController>().updateHealthPoints(healthPoints);
        if (healthPoints <= 0)
            gameOver();
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
        //gameMode = gameModesEnum.Ball;
        generator.gameMode = gameMode;
        player.changeGameMode(gameMode);
        generator.initializeGeneration();
        score = 0;
    }

    public void restart() 
    {
        Start();
        HUD.SetActive(false);
        mainMenu.SetActive(true);
        gameOverBanner.SetActive(false);
        generator.restart();
        player.restart();
    }
}
