using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelButton : MonoBehaviour
{
    public void GoToMainLevel()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        SceneManager.LoadScene(3);
    }
}
