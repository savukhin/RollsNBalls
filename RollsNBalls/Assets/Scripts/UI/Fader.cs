using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public float duration = 1f;

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float counter = 0f;
        var canv = gameObject.GetComponent<CanvasGroup>();
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canv.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }
    }
}
