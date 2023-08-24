using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeCubeButton : MonoBehaviour
{
    public void GoToUpgradeCubePage()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        SceneManager.LoadScene(4);
    }
}
