using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidernetEffect : MonoBehaviour
{
    [SerializeField] float timeTillDie;
    [SerializeField] float poisonEffectiveTime;
    public Timer timer;
    [Range(0.02f, 0.1f)]
    [SerializeField] float scaleUpFactor;
    [SerializeField] float speed;

    public bool poisonEffectTimerSetup;
    public bool canPoison;
    public Vector3 targetPos;
    public Color spidernetColor;
    public bool startChangingTransparency;


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

        spidernetColor = gameObject.GetComponent<MeshRenderer>().material.color;
        startChangingTransparency = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != null)
        {
            Vector3 dir = targetPos - transform.position;
            if (dir.magnitude > 0.5f)
            {
                transform.position += dir.normalized * speed * Time.deltaTime;
                if (transform.localScale.x < 1f)
                {
                    transform.localScale += new Vector3((1 + scaleUpFactor) * Time.deltaTime,
                                        (1 + scaleUpFactor) * Time.deltaTime, (1 + scaleUpFactor) * Time.deltaTime);
                }
            }
        }

        if (timer.CanStartMethod)
        {
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
            // Make the spidernet transparent over time
            
            if (!startChangingTransparency)
            {
                StartCoroutine(ChangeSpidernetTransparency());
                startChangingTransparency = true;
            }

            Collider[] playerCubes = Physics.OverlapSphere(gameObject.transform.position, 4f, LayerMask.NameToLayer("PlayerCube"));
            foreach (Collider collider in playerCubes)
            {
                if (collider.gameObject.GetComponent<PlayerPoisonEffect>() is not null)
                {
                    collider.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = false;
                    collider.gameObject.GetComponent<PlayerPoisonEffect>().spiderEffect = false;
                }
            }
            if (playerCubes.Length == 0)
            {
                if (spidernetColor.a <= 0f)
                {
                    Destroy(gameObject);
                }
                
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
                    if (spidernetColor.a <= 0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCube"))
        {
            //Debug.Log("Entered!");
            if (other.gameObject.GetComponent<PlayerPoisonEffect>() != null)
            {
                //Debug.Log("Entered2!");
                if (!timer.CanStartMethodForTimer("PoisonEffectTimer"))
                {
                    if (canPoison)
                    {
                        other.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = true;
                        other.gameObject.GetComponent<PlayerPoisonEffect>().spiderEffect = true;
                    }
                }
                else
                {
                    canPoison = false;
                    other.gameObject.GetComponent<PlayerPoisonEffect>().poisonEffectFlag = false;
                    other.gameObject.GetComponent<PlayerPoisonEffect>().spiderEffect = false;
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
                other.gameObject.GetComponent<PlayerPoisonEffect>().spiderEffect = false;
            }
        }
    }

    IEnumerator ChangeSpidernetTransparency()
    {
        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

        spidernetColor.a -= 0.1f;
        gameObject.GetComponent<MeshRenderer>().material.color = spidernetColor;
        yield return new WaitForSeconds(0.1f);

    }
}
