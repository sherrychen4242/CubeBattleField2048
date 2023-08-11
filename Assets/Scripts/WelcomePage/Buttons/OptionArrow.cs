using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionArrow : MonoBehaviour
{
    [SerializeField] ArrowName arrowName;
    // Start is called before the first frame update

    public enum ArrowName { StartNewGame, LoadGame, Settings, QuitGame}

    void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (arrowName)
        {
            case ArrowName.StartNewGame:
                if (FindObjectOfType<StartNewGameButton>().onMouseHover) GetComponent<Image>().enabled = true;
                else GetComponent<Image>().enabled = false;
                break;
            case ArrowName.LoadGame:
                if (FindObjectOfType<LoadGameButton>().onMouseHover) GetComponent<Image>().enabled = true;
                else GetComponent<Image>().enabled = false;
                break;
            case ArrowName.Settings:
                if (FindObjectOfType<SettingsButton>().onMouseHover) GetComponent<Image>().enabled = true;
                else GetComponent<Image>().enabled = false;
                break;
            case ArrowName.QuitGame:
                if (FindObjectOfType<QuitGameButton>().onMouseHover) GetComponent<Image>().enabled = true;
                else GetComponent<Image>().enabled = false;
                break;
        }
    }

}
