using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public int currentNumber;
    public int highestNumber;
    #endregion

    #region PRIVATE VARIABLES

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentNumber = 2;
        highestNumber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNumber > highestNumber)
        {
            highestNumber = currentNumber;
        }
    }

    public void PickUpCoin()
    {
        currentNumber += 2;
    }

    public void TakeDamage(int damageAmount)
    {
        currentNumber -= damageAmount;
    }
}
