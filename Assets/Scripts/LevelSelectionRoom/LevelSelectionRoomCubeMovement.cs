using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionRoomCubeMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    // Used to keep track whether any player cubes have made player change its speed so the other cubes can't make it change speed again

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    #endregion

    #region PRIVATE VARIABLES
    [HideInInspector]
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;

    /* public float initialYPosition;*/
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (GroundCheck())
            grounded = true;
        else
            grounded = false;

        if (grounded)
        {
            GetComponent<Rigidbody>().drag = groundDrag;
        }
        else
        {
            GetComponent<Rigidbody>().drag = 0f;
        }


    }

    private void FixedUpdate()
    {
        SpeedControl();
        Move();

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed, ForceMode.VelocityChange);

    }

    private void SpeedControl()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > moveSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * moveSpeed;
        }
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

}
