using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePageCube : MonoBehaviour
{
    [SerializeField] Timer timer;
    //[SerializeField] float disappearTime;
    public bool timerSetUp;
    // Start is called before the first frame update
    void Start()
    {
        timerSetUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerSetUp)
        {
            timer.maxTime = 8f;
            timer.startMethodRightAway = false;
            timer.StartTimer();
            timerSetUp = true;
        }

        if (timer.CanStartMethod)
        {
            Destroy(gameObject);
        }
    }
}
