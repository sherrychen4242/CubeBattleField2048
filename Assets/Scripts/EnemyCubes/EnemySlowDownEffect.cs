using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlowDownEffect : MonoBehaviour
{
    
    [Range(0f, 1f)]
    [SerializeField] float fronzenEffectSlowDownPercentage;
    [SerializeField] NavMeshAgent agent;
    public float originalSpeed;


    public float frozenSlowedDownSpeed;
    public bool frozenSlowedDownFlag;
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = agent.speed;
        frozenSlowedDownSpeed = originalSpeed * (1f - fronzenEffectSlowDownPercentage);
        frozenSlowedDownFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (frozenSlowedDownFlag)
        {
            FrozenSlowDown();
        }
        else
        {
            agent.speed = originalSpeed;
        }
    }

    private void FrozenSlowDown()
    {
        agent.speed = frozenSlowedDownSpeed;
    }


}
