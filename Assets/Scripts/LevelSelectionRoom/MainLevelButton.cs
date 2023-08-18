using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelButton : MonoBehaviour
{
    public void GoToMainLevel()
    {
        SceneManager.LoadScene(3);
    }
}
