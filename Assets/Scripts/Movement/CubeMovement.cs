using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float groundDrag;

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
    public Rigidbody rb;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        SpeedControl();
        if (GroundCheck())
            grounded = true;
        else
            grounded = false;

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;

        if (jumpKeyPressed && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void FixedUpdate()
    {
        Move();

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
    }

    private void Move()
    {
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void SpeedControl()
    {
        Vector3 currentVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (currentVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = currentVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
