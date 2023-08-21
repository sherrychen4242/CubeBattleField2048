using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCoinsDisplay : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Total Coins: " + FindObjectOfType<DataPersistenceManager>().gameData.totalCoins;
    }
}
