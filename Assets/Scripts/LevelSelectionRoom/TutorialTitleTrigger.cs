using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialTitleTrigger : MonoBehaviour
{
    [SerializeField] GameObject tutorialButton;
    [SerializeField] TMP_Text buttonText;
    // Start is called before the first frame update
    void Start()
    {
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 0f);
        tutorialButton.SetActive(false);
        //tutorialButton.GetComponent<Button>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LevelSelectionRoomPlayer"))
        {
            StartCoroutine(TextAppear());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LevelSelectionRoomPlayer"))
        {
            StartCoroutine(TextDisappear());
        }
    }

    IEnumerator TextAppear()
    {
        tutorialButton.SetActive(true);
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 0f);
        Color textColor = buttonText.color;
        for (int i = 0; i < 100; i++)
        {
            textColor.a += 0.01f;
            buttonText.color = textColor;
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    IEnumerator TextDisappear()
    {
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1f);
        Color textColor = buttonText.color;
        for (int i = 0; i < 100; i++)
        {
            textColor.a -= 0.01f;
            buttonText.color = textColor;
            yield return new WaitForSeconds(0.01f);
        }
        tutorialButton.SetActive(false);
    }
}
