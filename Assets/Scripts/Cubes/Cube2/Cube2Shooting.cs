using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube2Shooting : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [SerializeField] GameObject arrowImage;
    public LayerMask whatIsGround;

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootingCoolDownTime;
    [SerializeField] Transform bulletPos;
    [SerializeField] float bulletSpeed;
    [SerializeField] Timer timer;
    #endregion

    #region PRIVATE VARIABLES
    public Vector3 mousePosition;
    public bool isLeftMouseButtonDown;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // Init timer;
        timer.maxTime = shootingCoolDownTime;
        timer.startMethodRightAway = true;

        isLeftMouseButtonDown = false;
    }

    // Update is called once per frame
    void Update()
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

    private void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);

        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, 0f, mousePosition.z - transform.position.z);
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
    }

    private void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) isLeftMouseButtonDown = true;
        if (Input.GetMouseButtonUp(0)) isLeftMouseButtonDown = false;
    }
}
