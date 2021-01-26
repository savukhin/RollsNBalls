using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalWorld : MonoBehaviour
{
    [SerializeField]
    private mapGeneration generator = null;
    public GameObject HUD;
    public GameObject mainMenu;
    public GameObject gameOverBanner;
    public int maxHealthPoints = 3;
    private int healthPoints;
    public int moneyPoints = 3;
    public CarController player;

    public void startGame() {
        healthPoints = maxHealthPoints;
        HUD.GetComponent<HUDController>().updateHealthPoints(healthPoints);
        HUD.GetComponent<HUDController>().updateMoneyPoints(moneyPoints);
        generator.pause = false;
        HUD.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void pause()
    {
        generator.stop();
    }

    public void resume()
    {
        generator.resume();
    }

    public void gameOver()
    {
        generator.stop();
        HUD.SetActive(false);
        gameOverBanner.SetActive(true);
    }

    public void takeDamage(int damage=1)
    {
        healthPoints -= damage;
        HUD.GetComponent<HUDController>().updateHealthPoints(healthPoints);
        if (healthPoints <= 0)
            gameOver();
    }

    public void restart() 
    {
        HUD.SetActive(false);
        mainMenu.SetActive(true);
        gameOverBanner.SetActive(false);
        generator.restart();
        player.restart();
    }
}
