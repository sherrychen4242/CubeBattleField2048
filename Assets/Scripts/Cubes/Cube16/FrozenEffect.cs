using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrozenEffect : MonoBehaviour, IDataPersistence
{
    [SerializeField] float timeTillDie;
    [SerializeField] float frozenEffectiveTime;
    /*[Range(0f, 1f)]
    [SerializeField] float speedSlowPercentage;*/
    [SerializeField] Timer timer;

    /*public float originalSpeed;
    public float slowedSpeed;
    public bool speedRecorded;*/
    public bool frozenEffectTimerSetup;
    public bool canFreeze;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadData(DataPersistenceManager.instance.gameData);

        timer.maxTime = timeTillDie;
        timer.startMethodRightAway = false;
        timer.StartTimer();

/*        speedRecorded = false;
*/        
        frozenEffectTimerSetup = false;
        canFreeze = true;

        //SoundManager.Instance.PlaySound(SoundManager.SoundEffects.Cube16FrozenEffectSound);
    }

    // Update is called once per frame
    void Update()
    {
        timer.StartTimer();

        if (!frozenEffectTimerSetup)
        {
            timer.AddTimer("FrozenEffectTimer", frozenEffectiveTime, false);
            timer.StartTimer("FrozenEffectTimer");
            frozenEffectTimerSetup = true;
        }

        if (timer.CanStartMethod)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
            Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, 1f);
            foreach (Collider collider in enemies)
            {
                if (collider.gameObject.GetComponent<EnemySlowDownEffect>() is not null)
                {
                    collider.gameObject.GetComponent<EnemySlowDownEffect>().frozenSlowedDownFlag = false;
                }
            }
            if (enemies.Length == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bool stillEnemyInside = false;
                foreach (Collider collider in enemies)
                {
                    if (collider.gameObject.GetComponent<EnemySlowDownEffect>() is not null)
                    {
                        stillEnemyInside = true;
                        break;
                    }
                }
                if (!stillEnemyInside)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemySlowDownEffect>() != null)
            {
                /*if (!speedRecorded)
                {
                    originalSpeed = other.gameObject.GetComponent<NavMeshAgent>().speed;
                    slowedSpeed = originalSpeed * (1 - speedSlowPercentage);
                    speedRecorded = true;
                }*/
                
                if (!timer.CanStartMethodForTimer("FrozenEffectTimer"))
                {
                    if (canFreeze)
                    {
                        other.gameObject.GetComponent<EnemySlowDownEffect>().frozenSlowedDownFlag = true;
                    }
                }
                else
                {
                    canFreeze = false;
                    other.gameObject.GetComponent<EnemySlowDownEffect>().frozenSlowedDownFlag = false;
                }
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemySlowDownEffect>() != null)
            {
                other.gameObject.GetComponent<EnemySlowDownEffect>().frozenSlowedDownFlag = false;
            }
        }
    }

    public void LoadData(GameData data)
    {
        frozenEffectiveTime = data.cube16_FrozenEffectiveTime;
        timeTillDie = data.cube16_FrozenEffectSurvivalTime;
    }

    public void SaveData(ref GameData data)
    {
        data.cube16_FrozenEffectiveTime = frozenEffectiveTime;
        data.cube16_FrozenEffectSurvivalTime = timeTillDie;
    }
}
