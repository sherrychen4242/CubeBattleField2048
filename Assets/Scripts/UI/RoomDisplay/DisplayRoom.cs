using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRoom : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = FindObjectOfType<LevelManager>().currentLevel.ToString();
    }
}
