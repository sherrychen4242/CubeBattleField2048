using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WastedCanvas : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image redImage1;
    [SerializeField] Image redImage2;
    [SerializeField] Image redImage3;
    [SerializeField] TextMeshProUGUI wastedText;
    [SerializeField] TextMeshProUGUI roomText;
    [SerializeField] TextMeshProUGUI coinText;

    public bool appearFinished = false;
    public bool startingAppearing = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
        if (!startingAppearing)
        {
            StartCoroutine(IncreaseTransparency());
            startingAppearing = true;
        }
        
    }

    IEnumerator IncreaseTransparency()
    {
        roomText.text = "Died in Room " + FindObjectOfType<LevelManager>().currentLevel.ToString();
        coinText.text = "Collected " + FindObjectOfType<Player>().highestNumber.ToString() + " coins";

        Color backgroundImageColor = backgroundImage.color;
        Color redImageColor = redImage1.color;
        Color wastedTextColor = wastedText.color;
        Color roomTextColor = roomText.color;
        Color coinTextColor = coinText.color;
        for (int i = 0; i < 100; i++)
        {
            backgroundImageColor.a += 0.01f;
            redImageColor.a += 0.01f;
            wastedTextColor.a += 0.01f;
            roomTextColor.a += 0.01f;
            coinTextColor.a += 0.01f;

            backgroundImage.color = backgroundImageColor;
            redImage1.color = redImageColor;
            redImage2.color = redImageColor;
            redImage3.color = redImageColor;
            wastedText.color = wastedTextColor;
            roomText.color = roomTextColor;
            coinText.color = coinTextColor;
            yield return new WaitForSeconds(0.03f);

        }
        yield return new WaitForSeconds(1.5f);
        appearFinished = true;
    }
}
