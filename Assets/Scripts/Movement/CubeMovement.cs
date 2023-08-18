using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    // Used to keep track whether any player cubes have made player change its speed so the other cubes can't make it change speed again
    public bool speedChanged;
    [SerializeField] Vector3 acceleration;

    public bool jumpKeyPressed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCoolDown;
    [SerializeField] float airMultiplier;
    public bool readyToJump;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    #endregion

    #region PRIVATE VARIABLES
    [HideInInspector]
    public float origSpeed;
    public Rigidbody[] cubeRBs;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;
    public Vector3 kickBackDir;
    public bool isKickBack;

   /* public float initialYPosition;*/
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        cubeRBs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in cubeRBs)
        {
            rb.freezeRotation = true;
        }
        
        readyToJump = true;

        speedChanged = false;
        origSpeed = moveSpeed;

        isKickBack = false;

        acceleration = Vector3.zero;

        /*initialYPosition = transform.position.y;*/
    }

    // Update is called once per frame
    void Update()
    {
        cubeRBs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in cubeRBs)
        {
            rb.freezeRotation = true;
        }

        CheckWhetherSlowDown();

        GetInput();
        SpeedControl();
        if (GroundCheck())
            grounded = true;
        else
            grounded = false;

        if (grounded)
        {
            foreach (Rigidbody rb in cubeRBs)
            {
                rb.drag = groundDrag;
            }
        }
            
        else
        {
            foreach (Rigidbody rb in cubeRBs)
            {
                rb.drag = 0f;
            }
        }

        /*if (!jumpKeyPressed)
        {
            if (Mathf.Abs(transform.position.y - initialYPosition) > 0.5f)
            {
                rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
            }
        }*/

        if (jumpKeyPressed && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }

    }

    private void FixedUpdate()
    {
        ChangeAcceleration();
        Move();

        if (isKickBack)
        {
            transform.position += kickBackDir * moveSpeed * 2f * Time.deltaTime;
        }

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
    }

    private void ChangeAcceleration()
    {
        Vector3 moveDir = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        if (moveDir.magnitude > 0)
        {
            /*if (acceleration.magnitude < 20f)
            {
                acceleration += moveDir * Time.deltaTime * 20f;
            }
            else
            {
                acceleration = acceleration.normalized * 20f;
            }*/
            if (Vector3.Dot(moveDir, acceleration) >= 0)
            {
                acceleration += moveDir * Time.deltaTime * 20f;
            }
            else
            {
                acceleration = Vector3.zero;
            }
            if (acceleration.magnitude > 20f)
            {
                acceleration = acceleration.normalized * 20f;
            }

        }
        else
        {
            if (acceleration.magnitude > 0f)
            {
                acceleration -= Time.deltaTime * acceleration;
            }
            else
            {
                acceleration = Vector3.zero;
            }
            
        }
        
    }

    private void Move()
    {
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        Vector3 newPos = transform.position + moveDirection * moveSpeed * Time.deltaTime + acceleration * moveSpeed * Mathf.Pow(Time.deltaTime, 2);
        CubeMove[] cubeMoves = FindObjectsOfType<CubeMove>();
        foreach (CubeMove cm in cubeMoves)
        {
            if (!cm.onNavMesh)
            {
                Vector3 dir = transform.position - cm.outOfNavMeshPos;
                transform.position += dir.normalized * moveSpeed * Time.deltaTime;
                return;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, 1f);        
        
    }

    private bool IsPositionOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;

        float smallestX = Mathf.Infinity;
        float largestX = -Mathf.Infinity;
        float smallestZ = Mathf.Infinity;
        float largestZ = -Mathf.Infinity;
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("PlayerCube");
        foreach(GameObject cube in cubes)
        {
            float halfSize = cube.transform.localScale.x / 2f;
            Vector3 cubePosition = cube.transform.position + transform.position;
            if (cubePosition.x - halfSize < smallestX)
            {
                smallestX = cubePosition.x - halfSize;
            }

            if (cubePosition.x + halfSize > largestX)
            {
                largestX = cubePosition.x + halfSize;
            }

            if (cubePosition.z - halfSize < smallestZ)
            {
                smallestZ = cubePosition.z - halfSize;
            }

            if (cubePosition.z + halfSize > largestZ)
            {
                largestZ = cubePosition.z + halfSize;
            }
        }
        Vector3 position1 = new Vector3(smallestX, 0f, largestZ);
        Vector3 position2 = new Vector3(largestX, 0f, largestZ);
        Vector3 position3 = new Vector3(smallestX, 0f, smallestZ);
        Vector3 position4 = new Vector3(largestX, 0f, smallestZ);

        if (!(NavMesh.SamplePosition(position1, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position1, out hit, 4f, NavMesh.AllAreas))) return false;
        if (!(NavMesh.SamplePosition(position2, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position2, out hit, 4f, NavMesh.AllAreas))) return false;
        if (!(NavMesh.SamplePosition(position3, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position3, out hit, 4f, NavMesh.AllAreas))) return false;
        if (!(NavMesh.SamplePosition(position4, out hit, 0.1f, NavMesh.AllAreas) || NavMesh.SamplePosition(position4, out hit, 4f, NavMesh.AllAreas))) return false;

        return true;
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void SpeedControl()
    {

        foreach (Rigidbody rb in cubeRBs)
        {
            Vector3 currentVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (currentVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = currentVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        
    }

    private void Jump()
    {
        foreach (Rigidbody rb in cubeRBs)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void KickBack(Vector3 dir)
    {
        /*foreach (Rigidbody rb in cubeRBs)
        {
            rb.AddForce(dir, ForceMode.Impulse);
        }*/
        kickBackDir = dir;
        isKickBack = true;

        Invoke("StopKickBack", 0.5f);
        
    }

    public void StopKickBack()
    {
        isKickBack = false;
    }

    private void CheckWhetherSlowDown()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("PlayerCube");
        bool slowDown = false;
        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<PlayerPoisonEffect>().spiderEffect)
            {
                slowDown = true;
                break;
            }
        }
        if (!slowDown)
        {
            speedChanged = false;
            moveSpeed = origSpeed;
        }
    }
}
