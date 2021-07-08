using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour
{
    public GameObject modelPrefab;
    private GameObject model;
    public Shop shop;
    public bool purchased = false;
    public bool chosen = false;

    public void refresh()
    {
        if (model)
            Destroy(model);
        model = Instantiate(modelPrefab, transform);
        model.transform.localScale *= 50;
        if (purchased)
            gameObject.GetComponent<Image>().color = Color.green;
        if (chosen)
            gameObject.GetComponent<Image>().color = Color.yellow;
    }

    public void putModel()
    {
        shop.putModel(modelPrefab, purchased, chosen);
    }
}
