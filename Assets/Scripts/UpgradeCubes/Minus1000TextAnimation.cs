using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Minus1000TextAnimation : MonoBehaviour
{

    public void StartAnimation()
    {
        StartCoroutine(DecreaseTransparency());
    }

    IEnumerator DecreaseTransparency()
    {
        Color origColor = GetComponent<TextMeshProUGUI>().color;
        origColor.a = 1f;
        GetComponent<TextMeshProUGUI>().color = origColor;
        for (int i = 0; i < 100; i++)
        {
            origColor.a -= 0.01f;
            GetComponent<TextMeshProUGUI>().color = origColor;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
