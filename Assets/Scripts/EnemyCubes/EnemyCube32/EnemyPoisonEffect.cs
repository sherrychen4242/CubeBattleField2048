using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoisonEffect : MonoBehaviour
{
    [SerializeField] float timeTillDie;
    [SerializeField] float poisonEffectiveTime;
    public Timer timer;

    public bool poisonEffectTimerSetup;
    public bool canPoison;

    // Start is called before the first frame update
    void Start()
    {

        poisonEffectTimerSetup = false;
        canPoison = true;

        timer.maxTime = timeTillDie;
        timer.startMethodRightAway = false;
        timer.StartTimer();

        timer.AddTimer("PoisonEffectTimer", poisonEffectiveTime, false);
        timer.StartTimer("PoisonEffectTimer");
        poisonEffectTimerSetup = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer.CanStartMethod)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            Collider[] playerCubes = Physics.OverlapSphere(gameObject.transform.position, 2f, LayerMask.NameToLayer("PlayerCube"));
            foreach (Collider collider in playerCubes)
            {
                if (collider.gameObject.GetComponent<PlayerPoisonEffect>() is not null)
                {
                    collider.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = false;
                }
            }
            if (playerCubes.Length == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bool stillEnemyInside = false;
                foreach (Collider collider in playerCubes)
                {
                    if (collider.gameObject.GetComponent<PlayerPoisonEffect>() is not null)
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
        if (other.gameObject.CompareTag("PlayerCube"))
        {
            if (other.gameObject.GetComponent<PlayerPoisonEffect>() != null)
            {

                if (!timer.CanStartMethodForTimer("PoisonEffectTimer"))
                {
                    if (canPoison)
                    {
                        other.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = true;
                    }
                }
                else
                {
                    canPoison = false;
                    other.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = false;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCube"))
        {
            if (other.gameObject.GetComponent<PlayerPoisonEffect>() != null)
            {
                other.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = false;
            }
        }
    }
}
