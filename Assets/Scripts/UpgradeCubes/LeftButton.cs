using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButton : MonoBehaviour
{
    public void GoToPreviousCube()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        FindObjectOfType<UpgradeCubePageCamera>().DecreaseIndex();
    }

    public void GoToNextCube()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.UIClickSound);
        FindObjectOfType<UpgradeCubePageCamera>().IncreaseIndex();
    }
}
