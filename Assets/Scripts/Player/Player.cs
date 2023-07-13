using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public int currentNumber;
    #endregion

    #region PRIVATE VARIABLES

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentNumber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpCoin()
    {
        currentNumber += 2;
    }
}
