using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text[] HPText;
    public Text[] MoneyText;
    public Text[] ScoreText;
    public Text[] MaxScoreText;
    public GameObject TutorialPanel;
    public ProgressBar BossHPbar;
    public GameObject SongPanel;
    public Text SongTitleText;
    public Text SongAuthorText;
    public GameObject PauseMenu;

    public void pause()
    {
        PauseMenu.SetActive(true);
    }

    public void resume()
    {
        PauseMenu.SetActive(false);
    }

    public void updateHealthPoints(int healthPoints) {
        for (int i = 0; i < HPText.Length; i++)
            HPText[i].text = healthPoints.ToString();
    }

    public void updateMoneyPoints(int money) {
        for (int i = 0; i < MoneyText.Length; i++)
            MoneyText[i].text = money.ToString();
    }

    public void updateScore(int score) {
        for (int i = 0; i < ScoreText.Length; i++)
            ScoreText[i].text = "x" + score.ToString();
    }

    public void updateMaxScore(int score) {
        for (int i = 0; i < MaxScoreText.Length; i++)
            MaxScoreText[i].text = "Max Score: " + score.ToString();
    }

    public void updateBossHealthPoints(int healthPoints) {
        BossHPbar.setValue(healthPoints);
    }

    public void updateBossMaxHealthPoints(int healthPoints) {
        BossHPbar.setMaxValue(healthPoints);
    }

    private void HideTutorialPanel()
    {
        TutorialPanel.SetActive(false);
    }

    public void TutorialWrite(string value, float duration)
    {
        TutorialPanel.SetActive(true);
        TutorialPanel.GetComponentInChildren<Text>().text = value;
        Invoke("HideTutorialPanel", duration);
    }

    private void HideSongPanel()
    {
        SongPanel.GetComponent<Fader>().FadeOut();
//        SongPanel.SetActive(false);
    }

    public void updateSong(string title, string author)
    {
        SongPanel.SetActive(true);
        SongPanel.GetComponent<Fader>().FadeIn();
        SongTitleText.text = title;
        SongAuthorText.text = author;
        Invoke("HideSongPanel", 2f);
    }
}
