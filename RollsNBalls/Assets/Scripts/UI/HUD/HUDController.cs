using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text HPText;
    public Text[] MoneyText;
    public Text ScoreText;
    public ProgressBar BossHPbar;

    public void updateHealthPoints(int healthPoints) {
        HPText.text = healthPoints.ToString();
    }

    public void updateMoneyPoints(int money) {
        for (int i = 0; i < MoneyText.Length; i++)
        {
            MoneyText[i].text = money.ToString();
        }
    }

    public void updateScore(int score) {
        ScoreText.text = "x" + score.ToString();
    }

    public void updateBossHealthPoints(int healthPoints) {
        BossHPbar.setValue(healthPoints);
    }

    public void updateBossMaxHealthPoints(int healthPoints) {
        BossHPbar.setMaxValue(healthPoints);
    }
}
