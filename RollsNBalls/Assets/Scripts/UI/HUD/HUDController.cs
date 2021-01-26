using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text HPText;
    public Text MoneyText;

    public void updateHealthPoints(int healthPoints) {
        HPText.text = healthPoints.ToString();
    }

    public void updateMoneyPoints(int money) {
        MoneyText.text = money.ToString();
    }
}
