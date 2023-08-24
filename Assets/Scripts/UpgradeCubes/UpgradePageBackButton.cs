using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradePageBackButton : MonoBehaviour
{
    public void GoBackToLevelSelectionRoom()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        SceneManager.LoadScene(1);
    }
}
