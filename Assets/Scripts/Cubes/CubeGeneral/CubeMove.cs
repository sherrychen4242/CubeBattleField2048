using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    //[SerializeField] NavMeshAgent agent;
    public Vector3 destinationVector;
    public Vector3 destination;

    public bool obstaclesNear;
    public float avoidObstaclesTime;
    public bool obstacleEncountered;

    public bool onNavMesh = true;
    public Vector3 outOfNavMeshPos;
    public bool onGround = true;
    public float groundCheckTimer = 0f;

    /*public bool isAvoidingCube;
    public float avoidingCubeCurrentTime;

    private bool startCollidedWithCubeTimer;
    private bool collidedWithCube;
    private float collidedWithCubeCurrentTime;*/

    private float staySamePlaceTime;
    private bool needNudge;
    private Vector3 previousPosition;
    private float nudgeTime;
    private Vector3 randomDir;
    

    // Start is called before the first frame update
    void Start()
    {
        //agent.speed = moveSpeed;
        /*isAvoidingCube = false;
        collidedWithCube = false;
        startCollidedWithCubeTimer = false;*/

        previousPosition = transform.position;
        staySamePlaceTime = 0f;
        needNudge = false;
        nudgeTime = 0f;

        obstaclesNear = false;
        avoidObstaclesTime = 0f;
        obstacleEncountered = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (startCollidedWithCubeTimer)
        {
            if (collidedWithCubeCurrentTime < 1.5f)
            {
                collidedWithCubeCurrentTime += Time.deltaTime;
            }
            else
            {
                collidedWithCubeCurrentTime = 0f;
                collidedWithCube = false;
                startCollidedWithCubeTimer = false;
            }
*/
        /*}*/
        

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
        if (!GroundCheck())
        {
            if (groundCheckTimer < 2f)
            {
                onGround = true;
                groundCheckTimer += Time.deltaTime;
            }
            else
            {
                groundCheckTimer = 0f;
                onGround = false;
            }
        }
        else
        {
            onGround = true;
            groundCheckTimer = 0f;
        }

        if (IsPositionOnNavMesh())
        {
            onNavMesh = true;
        }
        else
        {
            onNavMesh = false;
            return;
        }

        destination = GameObject.FindGameObjectWithTag("Player").transform.position + destinationVector;
        if ((transform.position - destination).magnitude < 0.01f)
        {
            return;
        }
        else
        {

            //Collider[] obstacles = Physics.OverlapSphere(transform.position, transform.localScale.x + 3f, LayerMask.NameToLayer("Obstacle"));
            //List<GameObject> obstacleList = new List<GameObject>();

            GameObject[] obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");

            List<GameObject> closeObstacles = new List<GameObject>();
            foreach (GameObject obstacle in obstacleList)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - obstacle.transform.position.x, 2) + 
                    Mathf.Pow(transform.position.z - obstacle.transform.position.z, 2));
                if (distance < transform.localScale.x + 3f)
                {
                    closeObstacles.Add(obstacle);
                }
            }
            
            if (closeObstacles.Count > 0)
            {
                //print("obstacles!");
                GetComponentInParent<Grid>().obstacleEncountered = true;
                obstacleEncountered = true;
                Vector3 sumVector = Vector3.zero;
                foreach (GameObject obstacle in closeObstacles)
                {
                    Vector3 dir = new Vector3(transform.position.x - obstacle.gameObject.transform.position.x,
                        0f,
                        transform.position.z - obstacle.gameObject.transform.position.z);
                    sumVector += dir;
                }
                Vector3 newPosition = transform.position + sumVector.normalized * moveSpeed * Time.deltaTime;
                //Vector3 newDir = transform.position + sumVector.normalized;
                transform.position = Vector3.Lerp(transform.position, newPosition, 0.5f);
                //GetComponent<Rigidbody>().AddForce(newDir * moveSpeed, ForceMode.Impulse);
                //transform.position += sumVector.normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                obstacleEncountered = false;

                if (GetComponentInParent<Grid>().obstacleEncountered)
                {
                    return;
                }

                if ((transform.position - previousPosition).magnitude < 0.1f)
                {
                    staySamePlaceTime += Time.deltaTime;
                    if (staySamePlaceTime >= 0.5f)
                    {
                        needNudge = true;
                        randomDir = new Vector3(Random.value, 0f, Random.value);
                        //print("nudge!");
                    }
                }

                if (needNudge)
                {
                    nudgeTime += Time.deltaTime;
                    if (nudgeTime < 0.5f)
                    {
                        //GetComponent<Rigidbody>().AddForce(randomDir.normalized * moveSpeed, ForceMode.Impulse);
                        transform.position += randomDir.normalized * moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        needNudge = false;
                        staySamePlaceTime = 0f;
                    }

                }
                else
                {
                    Vector3 newPosition = transform.position + (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
                    newPosition.y = transform.localScale.x / 2f;
                    //GetComponent<Rigidbody>().AddForce((destination - transform.position).normalized * moveSpeed, ForceMode.VelocityChange);
                    //transform.position += (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.localScale.x / 2f, transform.position.z), newPosition, 0.5f);
                }
            }

            

            /*if (GetComponent<LineRenderer>() != null)
            {
                LineRenderer line = GetComponent<LineRenderer>();
                line.SetPosition(0, transform.position);
                line.SetPosition(1, destination);
            }*/


            /*RaycastHit hit;

            if (Physics.Raycast(transform.position, (destination - transform.position).normalized, out hit, Mathf.Infinity, LayerMask.NameToLayer("PlayerCube")))
            {
                print("ray!");
                Vector3 origDir = (destination - transform.position).normalized;
                Vector3 rotatedDir = new Vector3(origDir.z, origDir.y, -origDir.x);
                transform.position += rotatedDir.normalized * moveSpeed * 2f * Time.deltaTime;
            }
            else
            {
                transform.position += (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
            }*/

            /*GameObject[] cubes = GameObject.FindGameObjectsWithTag("PlayerCube");
            List<GameObject> cubesThatAreTooClose = new List<GameObject>();
            foreach (GameObject cube in cubes)
            {
                if (cube != gameObject)
                {
                    float distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - cube.transform.position.x), 2) +
                        Mathf.Pow((transform.position.z - cube.transform.position.z), 2));
                    float normalDistance = gameObject.transform.localScale.x / 2f + cube.transform.localScale.x / 2f + GetComponentInParent<Grid>().cubeGapWidth;
                    if (distance <= normalDistance)
                    {
                        cubesThatAreTooClose.Add(cube);
                    }
                }
            }

            if (cubesThatAreTooClose.Count == 0)
            {
                transform.position += (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                Vector3 maxVector = Vector3.zero;
                foreach (GameObject cube in cubesThatAreTooClose)
                {
                    Vector3 dir = new Vector3(transform.position.x - cube.transform.position.x,
                        0f,
                        transform.position.z - cube.transform.position.z);
                    if (dir.magnitude > maxVector.magnitude)
                    {
                        maxVector = dir;
                    }
                }
                transform.position += maxVector.normalized * moveSpeed * Time.deltaTime;
            }*/


            /*if (!isAvoidingCube)
            {
                transform.position += (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                if (avoidingCubeCurrentTime < 0.5f)
                {
                    avoidingCubeCurrentTime += Time.deltaTime;
                    Vector3 origDir = (destination - transform.position).normalized;
                    Vector3 rotatedDir = new Vector3(origDir.z, origDir.y, -origDir.x);
                    transform.position += rotatedDir.normalized * moveSpeed * Time.deltaTime;
                }
                else
                {
                    avoidingCubeCurrentTime = 0f;
                    isAvoidingCube = false;
                }
            }*/
        }
    }

    private bool IsPositionOnNavMesh()
    {
        NavMeshHit hit;
        float halfSize = transform.localScale.x / 2f + 3f;
        
        float smallestX = transform.position.x - halfSize;
        float largestX = transform.position.x + halfSize;
        float smallestZ = transform.position.z - halfSize;
        float largestZ = transform.position.z + halfSize;
        
        Vector3 position1 = new Vector3(smallestX, 0f, largestZ);
        Vector3 position2 = new Vector3(largestX, 0f, largestZ);
        Vector3 position3 = new Vector3(smallestX, 0f, smallestZ);
        Vector3 position4 = new Vector3(largestX, 0f, smallestZ);

        if (!(NavMesh.SamplePosition(position1, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position1, out hit, 4f, NavMesh.AllAreas)))
        {
            outOfNavMeshPos = position1;
            return false;
        }
        if (!(NavMesh.SamplePosition(position2, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position2, out hit, 4f, NavMesh.AllAreas)))
        {
            outOfNavMeshPos = position2;
            return false;
        }
        if (!(NavMesh.SamplePosition(position3, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position3, out hit, 4f, NavMesh.AllAreas)))
        {
            outOfNavMeshPos = position3;
            return false;
        }
        if (!(NavMesh.SamplePosition(position4, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position4, out hit, 4f, NavMesh.AllAreas)))
        {
            outOfNavMeshPos = position4;
            return false;
        }
        return true;
    }

    /*private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerCube"))
        {
            startCollidedWithCubeTimer = true;
            if (!collidedWithCube)
            {
                print("Collided!");
                isAvoidingCube = true;
                collidedWithCube = true;
            }
            
        }
    }*/

    private bool GroundCheck()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position + GameObject.FindGameObjectWithTag("Player").transform.position, transform.localScale.x / 2f, LayerMask.NameToLayer("Ground"));
        if (objects.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
