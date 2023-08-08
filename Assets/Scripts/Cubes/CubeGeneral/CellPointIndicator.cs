using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPointIndicator : MonoBehaviour
{
    [SerializeField] float existingTime;
    public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < existingTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            Destroy(gameObject);
        }
    }
}
