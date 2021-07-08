using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Review : MonoBehaviour
{
    private GameObject prefab;
    private GameObject model;
    public GameObject rotator;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public Button buyButton;
    public Button useButton;

    public void setModel(GameObject prefab, bool purchased, bool chosen)
    {
        this.prefab = prefab;
        gameObject.SetActive(true);
        if (model)
            Destroy(model);
        model = Instantiate(prefab, rotator.transform);
        nameText.text = model.GetComponent<Model>().modelName;
        descriptionText.text = model.GetComponent<Model>().description;
        priceText.text = "Price: " + model.GetComponent<Model>().price;
        buyButton.gameObject.SetActive(!purchased);
        useButton.gameObject.SetActive(purchased);
        useButton.enabled = !chosen;
        if (chosen)
            useButton.GetComponentInChildren<Text>().text = "Chosen";
        else
            useButton.GetComponentInChildren<Text>().text = "Use";
    }

    public bool hasModel()
    {
        return model != null;
    }

    public Model getModelPrefab()
    {
        return prefab.GetComponent<Model>();
    }
}
