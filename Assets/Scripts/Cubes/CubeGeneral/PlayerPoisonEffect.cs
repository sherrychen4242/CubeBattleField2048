using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoisonEffect : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] int poisonEffectDamagePerSecond;
    [SerializeField] Material poisonMaterial;

    public bool poisonEffectFlag;
    public bool timerSetUp;
    public Material origMaterial;
    public bool spiderEffect;
    public float origSpeed;
    [Range(0.1f, 0.9f)]
    [SerializeField] float slowDownPercentage;

    // Start is called before the first frame update
    void Start()
    {
        poisonEffectFlag = false;
        timerSetUp = false;
        origMaterial = GetComponent<Renderer>().material;

        origSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<CubeMovement>().moveSpeed;
        spiderEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerSetUp)
        {
            timer.AddTimer("PoisonTimer", 1f, true);
            timer.StartTimer("PoisonTimer");
            timerSetUp = true;
        }

        if (poisonEffectFlag)
        {
            GetComponent<Renderer>().material = poisonMaterial;
            if (timer.CanStartMethodForTimer("PoisonTimer"))
            {
                PoisonDamage();
                if (spiderEffect &&
                    !GameObject.FindGameObjectWithTag("Player").GetComponent<CubeMovement>().speedChanged)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CubeMovement>().moveSpeed = origSpeed * slowDownPercentage;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CubeMovement>().speedChanged = true;
                }
            }
        }
        else
        {
            GetComponent<Renderer>().material = origMaterial;
            
        }

        CheckWhetherPoisoned();
    }

    private void PoisonDamage()
    {
        gameObject.GetComponentInParent<Player>()?.TakeDamage(poisonEffectDamagePerSecond);
    }

    private void CheckWhetherPoisoned()
    {
        bool poisonNearby = false;
        ArrayList poisonEffectColliders = new ArrayList();
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x + 1);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<EnemyPoisonEffect>() != null ||
                collider.gameObject.GetComponent<SpidernetEffect>() != null)
            {
                poisonEffectColliders.Add(collider);
                poisonNearby = true;
                break;
            }
        }

        if (!poisonNearby && poisonEffectFlag)
        {
            poisonEffectFlag = false;
            GetComponent<Renderer>().material = origMaterial;
            

        }

        if (poisonNearby)
        {
            bool poisonTakingEffect = false;
            foreach (Collider collider in poisonEffectColliders)
            {
                if (collider.gameObject.GetComponent<EnemyPoisonEffect>() != null)
                {
                    if (!collider.gameObject.GetComponent<EnemyPoisonEffect>().timer.CanStartMethod)
                    {
                        poisonTakingEffect = true;
                        break;
                    }
                }
                else if (collider.gameObject.GetComponent<SpidernetEffect>() != null)
                {
                    if (!collider.gameObject.GetComponent<SpidernetEffect>().timer.CanStartMethod)
                    {
                        poisonTakingEffect = true;
                        break;
                    }
                }
                
            }
            if (!poisonTakingEffect)
            {
                poisonEffectFlag = false;
                GetComponent<Renderer>().material = origMaterial;
                
            }
        }
    }
}
