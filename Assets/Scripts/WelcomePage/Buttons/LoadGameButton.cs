using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour
{
    public bool onMouseHover;

    public void MouseHoverTrue()
    {
        onMouseHover = true;
    }

    public void MouseHoverFalse()
    {
        onMouseHover = false;
    }

    public void LoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene(1);
    }
}
