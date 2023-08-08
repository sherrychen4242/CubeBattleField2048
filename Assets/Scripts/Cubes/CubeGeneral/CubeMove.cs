using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] NavMeshAgent agent;
    public Vector3 destinationVector;
    

    // Start is called before the first frame update
    void Start()
    {
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationVector == null) return;

        /*if (agent.remainingDistance > 0.01f)
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position + destinationVector);
        }
        else
        {
            agent.ResetPath();
            agent.isStopped = true;
        }*/

        Vector3 destination = GameObject.FindGameObjectWithTag("Player").transform.position + destinationVector;
        if ((transform.position - destination).magnitude < 0.01f)
        {
            return;
        }
        else
        {
            transform.position += (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
    }
}
