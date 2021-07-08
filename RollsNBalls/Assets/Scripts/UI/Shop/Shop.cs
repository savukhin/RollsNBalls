using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public GameObject[] carPrefabs;
    public GameObject[] planePrefabs;
    public Review modelReview;
    public GameObject modelScroll;
    public GameObject placePrefab;
    public List<string> purchased = new List<string>();
    public class Chosen
    {
        public string ball = "";
        public string car = "";
        public string plane = "";
    }
    public Chosen chosenModelNames;
    private GameObject[] places;
    public GameObject modelScrollContent;
    public SaveManager saveManager;
    public gameModesEnum gameMode = gameModesEnum.Ball;
    public Text MoneyText;
    public int moneyPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        purchased = saveManager.LoadPurchased();
        chosenModelNames = saveManager.LoadChosen();
        moneyPoints = saveManager.LoadMoney();
        MoneyText.text = moneyPoints.ToString();
        loadGoods();
    }

    public void changeGameMode(string mode)
    {
        gameMode = (gameModesEnum)System.Enum.Parse(typeof(gameModesEnum), mode);
        loadGoods();
    }

    public void loadGoods()
    {
        if (places != null && places.Length > 0)
        {
            for (int i = 0; i < places.Length; i++)
                Destroy(places[i]);
        }
        
        GameObject[] prefabs;
        string chosenName;
        switch (gameMode)
        {
            case gameModesEnum.Ball:
                prefabs = ballPrefabs;
                chosenName = chosenModelNames.ball;
                break;
            case gameModesEnum.Car:
                prefabs = carPrefabs;
                chosenName = chosenModelNames.car;
                break;
            default:
                prefabs = planePrefabs;
                chosenName = chosenModelNames.plane;
                break;
        }

        places = new GameObject[prefabs.Length];

        int n = (prefabs.Length + 1) / 2;
        float delta = 1.0f / n;
        // Left Column
        for (int i = 0; i < n; i++)
        {
            places[i] = Instantiate(placePrefab, modelScrollContent.transform);
            places[i].GetComponent<RectTransform>().anchorMin = new Vector2(0f, delta * i);
            places[i].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, delta * (i + 1));
        }
        // Right Column
        for (int i = n; i < prefabs.Length; i++)
        {
            
            places[i] = Instantiate(placePrefab, modelScrollContent.transform);
            places[i].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, delta * (i - n));
            places[i].GetComponent<RectTransform>().anchorMax = new Vector2(1f, delta * (i + 1 - n));
        }

        modelScrollContent.GetComponent<RectTransform>().localScale = new Vector3(1, n, 1);

        for (int i = 0; i < places.Length; i++)
        {
            Place place = places[i].GetComponent<Place>();
            if (purchased.Contains(prefabs[i].GetComponent<Model>().modelName))
                place.purchased = true;
            if (prefabs[i].GetComponent<Model>().modelName == chosenName)
                place.chosen = true;
            
            place.shop = this;
            place.modelPrefab = prefabs[i];
            place.refresh();
            places[i].GetComponent<Button>().onClick.AddListener(place.putModel);
        }
    }

    public void buyChosenGood()
    {
        if (!modelReview.hasModel())
            return;
        
        Model model =  modelReview.getModelPrefab();

        if (purchased.Contains(model.modelName))
            return;

        if (moneyPoints > model.price)
        {
            purchased.Add(model.modelName);
            moneyPoints -= model.price;
            MoneyText.text = moneyPoints.ToString();
            modelReview.setModel(modelReview.getModelPrefab().gameObject, true, false);

            saveManager.SavePurchased(purchased);
            saveManager.SaveMoney(moneyPoints);
            loadGoods();
        }
    }

    public void putModel(GameObject prefab, bool purchased, bool chosen)
    {
        modelReview.setModel(prefab, purchased, chosen);
    }

    public void useModel()
    {
        string modelName = modelReview.getModelPrefab().modelName;
        switch (gameMode)
        {
            case gameModesEnum.Ball:
                chosenModelNames.ball = modelName;
                break;
            case gameModesEnum.Car:
                chosenModelNames.car = modelName;
                break;
            default:
                chosenModelNames.plane = modelName;
                break;
        }
        putModel(modelReview.getModelPrefab().gameObject, true, true);
        saveManager.SaveChosen(chosenModelNames);
        loadGoods();
    }
}
