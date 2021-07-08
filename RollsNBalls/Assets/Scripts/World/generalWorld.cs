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
        public UnityEngine.Events.UnityEvent changeGameModeEvent;
        public UnityEngine.Events.UnityEvent exitEvent;
    }
    [Serializable]
    public struct AllModels {
        public Model[] balls;
        public Model[] cars;
        public Model[] planes;
    }
    public Events events;
    public AllModels allModels;

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
    //public int moneyPoints = 0;
    public int score = 0;
    public SaveManager saveManager;

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
        saveManager.SaveMoney(player.moneyPoints);
        StopCoroutine("ScoreUpdate");
    }

    public void win()
    {
        events.winEvent.Invoke();
        saveManager.SaveMoney(player.moneyPoints);
        StopCoroutine("ScoreUpdate");
    }

    public void exit()
    {
        events.exitEvent.Invoke();
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
            generator.speed = Mathf.Min(generator.maxSpeed, generator.speed + generator.deltaSpeed);
            HUD.GetComponent<HUDController>().updateScore(score);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void changeGameMode(gameModesEnum mode)
    {
        events.changeGameModeEvent.Invoke();
        this.gameMode = mode;
        generator.changeGameMode(mode);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject find(Model[] from, string name)
        {
            foreach (var item in from)
            {
                if (item.modelName == name)
                    return item.gameObject;
            }
            return null;
        }

        Shop.Chosen chosen = saveManager.LoadChosen();
        GameObject ballPrefab = find(allModels.balls, chosen.ball), 
                    carPrefab = find(allModels.cars, chosen.car), 
                    planePrefab = find(allModels.planes, chosen.plane);
        player.setModels(ballPrefab, carPrefab, planePrefab);
        
        generator.gameMode = gameMode;
        player.changeGameMode(gameMode);
        generator.initializeGeneration();
        player.moneyPoints = saveManager.LoadMoney();
        updateHUD();
        score = 0;
    }

    public void restart() 
    {
        int ind = (((int)gameMode) - 1 + UnityEngine.Random.Range(1, 3)) % 3 + 1;
        switch (ind)
        {
            case 1:
                gameMode = gameModesEnum.Ball;
                break;
            case 2:
                gameMode = gameModesEnum.Car;
                break;
            case 3:
                gameMode = gameModesEnum.Plane;
                break;
        }
        Start();
        events.restartEvent.Invoke();
    }
}
