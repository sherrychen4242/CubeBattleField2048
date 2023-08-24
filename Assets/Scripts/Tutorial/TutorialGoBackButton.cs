using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGoBackButton : MonoBehaviour
{
    public void GoBack()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        SceneManager.LoadScene(1);
    }
}
