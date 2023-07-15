using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    [SerializeField] float timeTillDie;
    [SerializeField] float fireEffectiveTime;
    [SerializeField] Timer timer;

    public bool fireEffectTimerSetup;
    public bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        
        fireEffectTimerSetup = false;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!fireEffectTimerSetup)
        {
            timer.maxTime = timeTillDie;
            timer.startMethodRightAway = false;
            timer.StartTimer();

            timer.AddTimer("FireEffectTimer", fireEffectiveTime, false);
            timer.StartTimer("FireEffectTimer");
            fireEffectTimerSetup = true;
        }

        if (timer.CanStartMethod)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
            Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, 1f);
            foreach (Collider collider in enemies)
            {
                if (collider.gameObject.GetComponent<EnemyBurningEffect>() is not null)
                {
                    collider.gameObject.GetComponent<EnemyBurningEffect>().fireEffectFlag = false;
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
                    if (collider.gameObject.GetComponent<EnemyBurningEffect>() is not null)
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
            if (other.gameObject.GetComponent<EnemyBurningEffect>() != null)
            {

                if (!timer.CanStartMethodForTimer("FireEffectTimer"))
                {
                    if (canFire)
                    {
                        other.gameObject.GetComponent<EnemyBurningEffect>().fireEffectFlag = true;
                    }
                }
                else
                {
                    canFire = false;
                    other.gameObject.GetComponent<EnemyBurningEffect>().fireEffectFlag = false;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyBurningEffect>() != null)
            {
                other.gameObject.GetComponent<EnemyBurningEffect>().fireEffectFlag = false;
            }
        }
    }
}
