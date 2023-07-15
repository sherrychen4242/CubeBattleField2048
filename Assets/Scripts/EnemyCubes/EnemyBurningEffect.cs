using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBurningEffect : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] int fireEffectDamagePerSecond;

    public bool fireEffectFlag;
    public bool timerSetUp;

    // Start is called before the first frame update
    void Start()
    {
        fireEffectFlag = false;
        timerSetUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerSetUp)
        {
            timer.maxTime = 1f;
            timer.startMethodRightAway = true;
            timer.StartTimer();
            timerSetUp = true;
        }

        if (fireEffectFlag)
        {
            if (timer.CanStartMethod)
            {
                FireDamage();
            }
        }
    }

    private void FireDamage()
    {
        gameObject.GetComponent<Health>()?.TakeDamage(fireEffectDamagePerSecond);
    }

}
