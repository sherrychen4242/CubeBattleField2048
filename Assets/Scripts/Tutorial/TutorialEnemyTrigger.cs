using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemyCube2;
    [SerializeField] GameObject enemyCube4;

    // Start is called before the first frame update
    void Start()
    {
        enemyCube2.SetActive(false);
        enemyCube4.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCube"))
        {
            print("Player");
            if (enemyCube2 == null || enemyCube4 == null) return;
            enemyCube2.SetActive(true);
            enemyCube4.SetActive(true);
        }
    }
}
