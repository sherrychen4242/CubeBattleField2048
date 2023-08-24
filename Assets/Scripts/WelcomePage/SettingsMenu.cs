using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropDown;


    [SerializeField] private GameObject qualityDropDownObject;
    [SerializeField] private GameObject resolutionDropDownObject;
    [SerializeField] private GameObject fullScreenToggle;
    [SerializeField] private GameObject volumeSlider;
    [SerializeField] private GameObject settingBackButton;

    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject optionArrowPanel;

    Resolution[] resolutions;
    private void Start()
    {
        qualityDropDownObject.SetActive(false);
        resolutionDropDownObject.SetActive(false);
        fullScreenToggle.SetActive(false);
        volumeSlider.SetActive(false);
        settingBackButton.SetActive(false);

        qualityDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " * " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {

    }

    public void SetQuality(int qualityIndex)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void GoBackToMainMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        qualityDropDownObject.SetActive(false);
        resolutionDropDownObject.SetActive(false);
        fullScreenToggle.SetActive(false);
        volumeSlider.SetActive(false);
        settingBackButton.SetActive(false);

        optionPanel.SetActive(true);
        optionArrowPanel.SetActive(true);
    }

    public void GoToSettingsMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        qualityDropDownObject.SetActive(true);
        resolutionDropDownObject.SetActive(true);
        fullScreenToggle.SetActive(true);
        volumeSlider.SetActive(true);
        settingBackButton.SetActive(true);

        optionPanel.SetActive(false);
        optionArrowPanel.SetActive(false);
    }
}
