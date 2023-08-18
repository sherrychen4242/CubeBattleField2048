using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool triggeredFlag = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredFlag) return;

        if (other.gameObject.CompareTag("PlayerCube"))
        {
            FindObjectOfType<LevelManager>().GenerateNextLevel();
            triggeredFlag = true;
        }
    }
}
