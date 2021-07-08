using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {get; set;}
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void SavePurchased(List<string> purchased)
    {
        PlayerPrefs.SetString("SavePurchased", SaveHelper.Serialize<List<string>>(purchased));
    }

    public List<string> LoadPurchased()
    {
        if (!PlayerPrefs.HasKey("SavePurchased"))
        {
            List<string> purchased = new List<string>();
            purchased.Add("Green Ball");
            purchased.Add("Green Car");
            purchased.Add("Green Plane");
            SavePurchased(purchased);
            return LoadPurchased();
        }
        return SaveHelper.Deserialize<List<string>>(PlayerPrefs.GetString("SavePurchased"));
    }

    public void SaveChosen(Shop.Chosen chosen)
    {
        PlayerPrefs.SetString("SaveChosen", SaveHelper.Serialize<Shop.Chosen>(chosen));
    }

    public Shop.Chosen LoadChosen()
    {
        if (!PlayerPrefs.HasKey("SaveChosen"))
        {
            Shop.Chosen chosen = new Shop.Chosen();
            chosen.ball = "Green Ball";
            chosen.car = "Green Car";
            chosen.plane = "Green Plane";
            SaveChosen(chosen);
            return LoadChosen();
        }
        return SaveHelper.Deserialize<Shop.Chosen>(PlayerPrefs.GetString("SaveChosen"));
    }

    public void SaveMoney(int money)
    {
        PlayerPrefs.SetString("SaveMoney", SaveHelper.Serialize<int>(money));
    }

    public int LoadMoney()
    {
        if (!PlayerPrefs.HasKey("SaveMoney"))
        {
            SaveMoney(0);
            return LoadMoney();
        }
        return SaveHelper.Deserialize<int>(PlayerPrefs.GetString("SaveMoney"));
    }

    public void SaveRecordScore(int recordScore)
    {
        PlayerPrefs.SetString("SaveRecordScore", SaveHelper.Serialize<int>(recordScore));
    }

    public int LoadRecordScore()
    {
        if (!PlayerPrefs.HasKey("SaveRecordScore"))
        {
            SaveRecordScore(0);
            return LoadRecordScore();
        }
        return SaveHelper.Deserialize<int>(PlayerPrefs.GetString("SaveRecordScore"));
    }
}
