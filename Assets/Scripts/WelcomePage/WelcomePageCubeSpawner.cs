using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePageCubeSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] cubePrefabs;
    [SerializeField] Timer timer;
    [Range(0.1f, 1f)]
    [SerializeField] float minSpawnTime;
    [Range(0.1f, 3f)]
    [SerializeField] float maxSpawnTime;

    private bool timerSetUp;

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
            SetUpTimer();
            timerSetUp = true;
        }

        if (timer.CanStartMethod)
        {
            int ranNum = Random.Range(0, cubePrefabs.Length);
            GameObject cube = Instantiate(cubePrefabs[ranNum], transform.position, Quaternion.identity);
            timerSetUp = false;
        }
        
    }

    private void SetUpTimer()
    {
        timer.StopTimer();
        timer.maxTime = Random.Range(minSpawnTime, maxSpawnTime);
        timer.startMethodRightAway = false;
        timer.StartTimer();
    }
}
