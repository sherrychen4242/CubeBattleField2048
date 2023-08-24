using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCubePageCamera : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    public int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = positions[currentIndex].position - transform.position;
        transform.position += dir * 5f * Time.deltaTime;
    }

    public void IncreaseIndex()
    {
        if (currentIndex < positions.Length - 1)
        {
            currentIndex++;
        }
        else if (currentIndex == positions.Length - 1)
        {
            currentIndex = 0;
        }
    }

    public void DecreaseIndex()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else if (currentIndex == 0)
        {
            currentIndex = positions.Length - 1;
        }
    }
}
