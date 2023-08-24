using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    public void GoToTutorialLevel()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        SceneManager.LoadScene(2);
    }
}
