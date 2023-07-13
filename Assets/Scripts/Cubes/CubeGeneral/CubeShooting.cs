using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShooting : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public GameObject arrowImage;
    public LayerMask whatIsGround;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float shootingCoolDownTime;
    public Transform bulletPos;
    public float bulletSpeed;
    public Timer timer;
    #endregion

    #region PRIVATE VARIABLES
    public Vector3 mousePosition;
    public bool isLeftMouseButtonDown;
    #endregion
    // Start is called before the first frame update
    public virtual void Start()
    {
        // Init timer;
        timer.maxTime = shootingCoolDownTime;
        timer.startMethodRightAway = true;

        isLeftMouseButtonDown = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GetMousePosition();

        ChangeArrowOrientation();

        GetMouseInput();
        timer.StartTimer();
        if (isLeftMouseButtonDown)
        {
            if (timer.CanStartMethod)
            {
                Shooting();
            }
        }


    }

    private void GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, whatIsGround))
        {
            Vector3 hitPos = hit.point;
            mousePosition = new Vector3(hitPos.x, transform.position.y, hitPos.z);
        }
    }

    private void ChangeArrowOrientation()
    {
        Vector3 currentPosition = transform.position;
        Vector3 dir = new Vector3(mousePosition.x - currentPosition.x, currentPosition.y, mousePosition.z - currentPosition.z);
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            arrowImage.transform.rotation = Quaternion.Euler(-90f, -90f, lookRotation.eulerAngles.y);
        }

    }

    public virtual void Shooting()
    {
        
    }

    private void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) isLeftMouseButtonDown = true;
        if (Input.GetMouseButtonUp(0)) isLeftMouseButtonDown = false;
    }
}
