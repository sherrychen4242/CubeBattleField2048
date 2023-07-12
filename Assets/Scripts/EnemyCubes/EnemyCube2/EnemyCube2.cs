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
    [SerializeField] int damageAmount;
    #endregion

    #region PRIVATE VARIABLES

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = moveSpeed;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag(playerTag).transform.position);
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
                healthScripts[targetPlayerIndex].TakeDamage(damageAmount);
            }
        }
    }
}
