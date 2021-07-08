using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float min;
    public float max;
    public float current;
    public GameObject bar;
    public GameObject progress;

    private void refresh()
    {
        var width = bar.GetComponent<RectTransform>().rect.width * bar.GetComponent<RectTransform>().localScale.x;
        progress.GetComponent<RectTransform>().sizeDelta = new Vector2(
                                bar.GetComponent<RectTransform>().rect.width,
                                bar.GetComponent<RectTransform>().rect.height);
        progress.transform.localScale = new Vector3(current / max, 1f, 1f);
        progress.transform.localPosition = new Vector3(-width / 2f + current / max / 2f * width, 0f, 0f);
    }

    public void setValue(float value)
    {
        current = Mathf.Min(max, value);
        current = Mathf.Max(min, current);
        refresh();
    }

    public void setMaxValue(float value)
    {
        max = value;
        refresh();
    }

    void Start()
    {
        refresh();
    }
}
