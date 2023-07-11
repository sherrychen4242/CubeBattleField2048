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
}
