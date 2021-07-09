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
    public Radio radio;

    public void startGame() {
        player.startMoving();
        playerHealthPoints = maxPlayerHealthPoints;
        updateHUD();
        //generator.pause = false;
        generator.resume();
        HUD.SetActive(true);
        mainMenu.SetActive(false);
        boss.Activate();
        radio.Play();
        StartCoroutine("ScoreUpdate");
        if (!saveManager.LoadPassedTutorial())
        {
            StartCoroutine("Tutorial");
        }
    }

    public void pause()
    {
        radio.ChangeVolume(0.3f);
        events.pauseEvent.Invoke();
        StopCoroutine("ScoreUpdate");
        Time.timeScale = 0;
    }
    public void resume()
    {
        radio.ChangeVolume(1);
        if (!generator.tutorial)
            boss.Activate();
        events.resumeEvent.Invoke();
        StartCoroutine("ScoreUpdate");
        Time.timeScale = 1;
    }

    public void gameOver()
    {
        radio.ChangeVolume(0.3f);
        if (generator.tutorial)
        {
            player.Revive();
            updateHUD();
            return;
        }
        events.gameOverEvent.Invoke();
        saveManager.SaveMoney(player.moneyPoints);
        if (score > saveManager.LoadRecordScore())
            saveManager.SaveRecordScore(score);
        StopCoroutine("ScoreUpdate");
    }

    public void win()
    {
        radio.ChangeVolume(0.3f);
        events.winEvent.Invoke();
        saveManager.SaveMoney(player.moneyPoints);
        if (score > saveManager.LoadRecordScore())
            saveManager.SaveRecordScore(score);
        StopCoroutine("ScoreUpdate");
    }

    public void exit()
    {
        events.exitEvent.Invoke();
        StopCoroutine("ScoreUpdate");
        StopCoroutine("Tutorial");
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
            HUD.GetComponent<HUDController>().updateScore(score);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void changeGameMode(gameModesEnum mode)
    {
        if (generator.tutorial)
            return;
        events.changeGameModeEvent.Invoke();
        this.gameMode = mode;
        generator.changeGameMode(mode);
    }

    // Start is called before the first frame update
    void Start()
    {
        generator.maxSpeed = 40f;
        player.maxHealthPoints = 3;
        player.maxHealthPoints = 3;
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
        //generator.initializeGeneration();
        player.moneyPoints = saveManager.LoadMoney();
        HUD.GetComponent<HUDController>().updateMaxScore(saveManager.LoadRecordScore());
        if (!saveManager.LoadPassedTutorial())
            generator.generateTutorial();
        else
            generator.cancelTutorial();
        updateHUD();
        score = 0;
    }

    IEnumerator Tutorial()
    {
        player.healthPoints = 10;
        player.maxHealthPoints = 10;
        updateHUD();
        generator.maxSpeed = 20f;
        boss.Stop();
        yield return new WaitForSeconds(0.5f);
        HUD.GetComponent<HUDController>().TutorialWrite("It is ball\nYou can move to left and right", 3f);
        yield return new WaitForSeconds(4.5f);
        HUD.GetComponent<HUDController>().TutorialWrite("Try not to hit to obstacles", 3f);
        yield return new WaitForSeconds(5f);
        HUD.GetComponent<HUDController>().TutorialWrite("It is gate!", 1f);
        yield return new WaitForSeconds(3f);
        HUD.GetComponent<HUDController>().TutorialWrite("Now it is a car\nYou can change lines\nJust swipe!", 3f);
        yield return new WaitForSeconds(6f);
        HUD.GetComponent<HUDController>().TutorialWrite("If you take damage, you can heal", 3f);
        yield return new WaitForSeconds(4f);
        HUD.GetComponent<HUDController>().TutorialWrite("Another gate!", 2f);
        yield return new WaitForSeconds(3f);
        HUD.GetComponent<HUDController>().TutorialWrite("And the last, it is a plane\nYou can move\nup, right, left and botom", 4f);
        yield return new WaitForSeconds(5f);
        boss.Activate();
        HUD.GetComponent<HUDController>().TutorialWrite("And this guy can strike to you. Avoid this things!", 3f);
        yield return new WaitForSeconds(5f);
        boss.Stop();
        HUD.GetComponent<HUDController>().TutorialWrite("You can strike back, take this!", 3f);
        yield return new WaitForSeconds(5f);
        HUD.GetComponent<HUDController>().TutorialWrite("That's all. Have fun!", 3f);
        yield return new WaitForSeconds(3f);
        generator.tutorial = false;
        generator.maxSpeed = 40f;
        player.maxHealthPoints = 3;
        player.maxHealthPoints = 3;
        updateHUD();
        saveManager.SavePassedTutorial(true);
        startGame();
    }

    public void restart() 
    {
        radio.Stop();
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                HUD.GetComponent<HUDController>().pause();
                pause();
            }
            else
            {
                HUD.GetComponent<HUDController>().resume();
                resume();
            }
        }
    }
}
