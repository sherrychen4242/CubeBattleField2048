using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCube2 : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float moveSpeed;
    [SerializeField] string playerTag;
    [SerializeField] string playerCubeTag;
    [SerializeField] int damageAmount;
    [SerializeField] GameObject bulletHitBloodEffect;
    #endregion

    #region PRIVATE VARIABLES
    public bool startApproachTarget;
    public bool startSteppingBack;
    public GameObject target;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = moveSpeed;
        agent.updateRotation = false;
        startSteppingBack = false;
        startApproachTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = FindClosestCube();
        }

        MoveTowardsPlayer();

        
    }

    void MoveTowardsPlayer()
    {
        
        if (!startSteppingBack && startApproachTarget)
        {
            target = FindClosestCube();
            if (target is not null)
            {
                agent.SetDestination(target.transform.position);
                startApproachTarget = false;
            }
        }
        
        // Step Back
        if (!startApproachTarget && !startSteppingBack)
        {
            if (target is null)
            {
                target = FindClosestCube();
            }
            if (target is null)
            {
                return;
            }
            else
            {
                try
                {
                    Vector3 newRandomPos = Vector3.zero;
                    float playerCubeScale = target.transform.localScale.x;
                    float enemyCubeScale = gameObject.transform.localScale.x;

                    if (agent.remainingDistance < ((playerCubeScale + enemyCubeScale) / 2f) * 1.2f)
                    {
                        newRandomPos = StepBack(target);
                        startSteppingBack = true;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    target = FindClosestCube();
                }
                
            }
            
        }

        // Get Back
        if (startSteppingBack && !startApproachTarget)
        {
            if (target is null)
            {
                target = FindClosestCube();
            }
            if (target is null)
            {
                return;
            }
            else
            {
                try
                {
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if (agent.remainingDistance < 0.1f || distance > 1f)
                    {
                        startSteppingBack = false;
                        startApproachTarget = true;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    target = FindClosestCube();
                }
                
            }
        }
    }

    private Vector3 StepBack(GameObject target)
    {
        Vector2 newRandomPosVec2 = Random.insideUnitCircle * 2f;
        Vector3 newRandomPos = new Vector3(target.transform.position.x + newRandomPosVec2.x,
            target.transform.position.y, target.transform.position.z + newRandomPosVec2.y);
        agent.SetDestination(newRandomPos);
        return newRandomPos;
    }

    private GameObject FindClosestCube()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag(playerCubeTag);
        if (cubes.Length == 0) return null;
        float minDistance = Mathf.Infinity;
        GameObject targetCube = null;
        foreach (GameObject cube in cubes)
        {
            float distance = Vector3.Distance(transform.position, cube.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetCube = cube;
            }
        }
        return targetCube;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            if (collision.gameObject.GetComponent<Health>() != null)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
            }
            else if (collision.gameObject.GetComponentsInChildren<Health>().Length > 0)
            {
                Health[] healthScripts = collision.gameObject.GetComponentsInChildren<Health>();
                float minDistance = Mathf.Infinity;
                int targetPlayerIndex = 0;
                for (int i = 0; i < healthScripts.Length; i++)
                {
                    Health healthScript = healthScripts[i];
                    float distance = Vector3.Distance(transform.position, healthScript.gameObject.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetPlayerIndex = i;
                    }
                }
                float playerCubeScale = healthScripts[targetPlayerIndex].gameObject.transform.localScale.x;
                float enemyScale = gameObject.transform.localScale.x;
                if (minDistance < ((playerCubeScale + enemyScale)/2f) * Mathf.Sqrt(2f) * 1.3f)
                {
                    GameObject cube = healthScripts[targetPlayerIndex].gameObject;
                    Vector3 dir = cube.transform.position - gameObject.transform.position;
                    dir = (dir.normalized) * 5f;
                    cube.GetComponentInParent<CubeMovement>().KickBack(dir);
                    healthScripts[targetPlayerIndex].TakeDamage(damageAmount);
                    // Blood Effect
                    GameObject blood = Instantiate(bulletHitBloodEffect, cube.transform.position - dir.normalized * cube.transform.localScale.x / 2, Quaternion.EulerAngles(0, -90, 0));
                    blood.transform.forward = -dir.normalized;
                    blood.transform.localScale *= cube.transform.localScale.x;
                }
                
            }
        }
    }

}
