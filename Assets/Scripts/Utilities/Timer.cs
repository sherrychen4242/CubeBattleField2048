using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float maxTime;
    public float currentTime;
    public bool canStartMethod;
    public bool startMethodRightAway;
    public bool startTimer;

    public Timer(float _maxTime, bool _startMethodRightAway)
    {
        maxTime = _maxTime;
        startMethodRightAway = _startMethodRightAway;
    }

    public bool CanStartMethod
    {
        get { return canStartMethod; }
    }

    public void Init()
    {
        startTimer = false;
        currentTime = 0f;
        if (startMethodRightAway)
            canStartMethod = true;
        else
            canStartMethod = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        startTimer = false;

        if (startMethodRightAway)
            canStartMethod = true;
        else
            canStartMethod = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startTimer)
        {
            return;
        }

        if (currentTime < maxTime)
        {
            canStartMethod = false;
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            canStartMethod = true;
        }
            
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
        this.Init();
    }

    public void PauseTimer()
    {
        startTimer = false;
    }
}
