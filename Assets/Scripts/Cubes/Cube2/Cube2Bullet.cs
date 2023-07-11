using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2Bullet : MonoBehaviour
{
    [SerializeField] float bulletSurvivalTime;
    [SerializeField] Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer.maxTime = bulletSurvivalTime;
        timer.startMethodRightAway = false;
        timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer.StartTimer();
        if (timer.CanStartMethod)
        {
            Destroy(gameObject);
        }
    }
}
