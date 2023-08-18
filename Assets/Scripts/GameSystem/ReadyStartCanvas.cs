using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyStartCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator CountDown()
    {
        gameObject.SetActive(true);
        numberText.text = 1.ToString();
        yield return new WaitForSeconds(0.3f);
        numberText.text = 2.ToString();
        yield return new WaitForSeconds(0.3f);
        numberText.text = 3.ToString();
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
