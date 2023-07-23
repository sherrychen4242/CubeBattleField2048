using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Default Timer")]
    public float maxTime;
    public float currentTime;
    public bool canStartMethod;
    public bool startMethodRightAway;
    public bool startTimer;

    [Header("Multiple Timer")]
    public bool multipleTimer;
    public int timerCount;
    public Dictionary<string, int> timerCatalog;
    public List<float> maxTimeList;
    public List<float> currentTimeList;
    public List<bool> canStartMethodList;
    public List<bool> startMethodRightAwayList;
    public List<bool> startTimerList;

    public Timer(float _maxTime, bool _startMethodRightAway)
    {
        maxTime = _maxTime;
        startMethodRightAway = _startMethodRightAway;
    }

    public bool CanStartMethod
    {
        get { return canStartMethod; }
    }

    public bool CanStartMethodForTimer(string timerName)
    {
        if (!timerCatalog.ContainsKey(timerName)) Debug.LogError("timer name not found!");
        int timerIndex = timerCatalog[timerName];

        return canStartMethodList[timerIndex];
    }

    public void Init()
    {
        startTimer = false;
        startTimerList[0] = false;
        currentTime = 0f;
        currentTimeList[0] = 0f;
        if (startMethodRightAway)
        {
            canStartMethod = true;
            canStartMethodList[0] = true;
        }
        else
        {
            canStartMethod = false;
            canStartMethodList[0] = false;
        }
            
    }

    public void InitTimer(string timerName)
    {
        if (!timerCatalog.ContainsKey(timerName)) Debug.LogError("timer name not found!");
        int timerIndex = timerCatalog[timerName];

        startTimerList[timerIndex] = false;
        currentTimeList[timerIndex] = 0f;
        if (startMethodRightAwayList[timerIndex])
        {
            canStartMethodList[timerIndex] = true;
        }
        else
        {
            canStartMethodList[timerIndex] = false;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        multipleTimer = false;
        timerCount = 1;
        
        // Default
        currentTime = 0f;
        startTimer = false;

        if (startMethodRightAway)
            canStartMethod = true;
        else
            canStartMethod = false;

        // Multiple Timer
        timerCatalog = new Dictionary<string, int>();
        maxTimeList = new List<float>();
        currentTimeList = new List<float>();
        canStartMethodList = new List<bool>();
        startMethodRightAwayList = new List<bool>();
        startTimerList = new List<bool>();

        // Add default timer
        timerCatalog.Add("Default", 0);
        maxTimeList.Add(maxTime);
        currentTimeList.Add(currentTime);
        canStartMethodList.Add(canStartMethod);
        startMethodRightAwayList.Add(startMethodRightAway);
        startTimerList.Add(startTimer);
}

    // Update is called once per frame
    void Update()
    {
        // Default Timer
        if (!startTimer && timerCount == 1)
        {
            return;
        }

        if (startTimer)
        {
            if (currentTime < maxTime)
            {
                canStartMethod = false;
                canStartMethodList[0] = false;
                currentTime += Time.deltaTime;
                currentTimeList[0] += Time.deltaTime;
            }
            else
            {
                currentTime = 0f;
                currentTimeList[0] = 0f;
                canStartMethod = true;
                canStartMethodList[0] = true;
            }
        }
        
        // Multiple Timer
        if (multipleTimer)
        {
            for (int i = 1; i < timerCount; i++)
            {
                if (startTimerList[i] == false)
                {
                    continue;
                }

                if (currentTimeList[i] < maxTimeList[i])
                {
                    canStartMethodList[i] = false;
                    currentTimeList[i] += Time.deltaTime;
                }
                else
                {
                    currentTimeList[i] = 0f;
                    canStartMethodList[i] = true;
                }
            }
        }
    }

    public void AddTimer(string timerName, float maxTime, bool startMethodRightAway)
    {
        multipleTimer = true;
        timerCount += 1;
        int timerIndex = timerCount - 1;
        timerCatalog.Add(timerName, timerIndex);

        currentTimeList.Add(0f);
        maxTimeList.Add(maxTime);
        startTimerList.Add(false);
        startMethodRightAwayList.Add(startMethodRightAway);
        if (startMethodRightAway)
        {
            canStartMethodList.Add(true);
        }
        else
        {
            canStartMethodList.Add(false);
        }

    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StartTimer(string timerName)
    {
        if (!timerCatalog.ContainsKey(timerName)) Debug.LogError("timer name not found!");
        int timerIndex = timerCatalog[timerName];
        startTimerList[timerIndex] = true;
    }

    public void StopTimer()
    {
        startTimer = false;
        this.Init();
    }

    public void StopTimer(string timerName)
    {
        if (!timerCatalog.ContainsKey(timerName)) Debug.LogError("timer name not found!");
        int timerIndex = timerCatalog[timerName];
        startTimerList[timerIndex] = false;
        this.InitTimer(timerName);
    }

    public void PauseTimer()
    {
        startTimer = false;
    }

    public void PauseTimer(string timerName)
    {
        if (!timerCatalog.ContainsKey(timerName)) Debug.LogError("timer name not found!");
        int timerIndex = timerCatalog[timerName];
        startTimerList[timerIndex] = false;
    }
}
