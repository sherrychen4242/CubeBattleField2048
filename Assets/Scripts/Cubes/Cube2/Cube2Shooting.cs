using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube2Shooting : MonoBehaviour
{
    #region PUBLIC VARIABLES
    [SerializeField] GameObject arrowImage;
    public Vector3 mousePosition;
    public LayerMask whatIsGround;
    #endregion

    #region PRIVATE VARIABLES

    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();

        ChangeArrowOrientation();

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
        Vector3 currentPosition = transform.TransformPoint(transform.position);
        Vector3 dir = new Vector3(mousePosition.x - currentPosition.x, currentPosition.y, mousePosition.z - currentPosition.z);
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            arrowImage.transform.rotation = Quaternion.Euler(-90f, -90f, lookRotation.eulerAngles.y);
        }

    }
}
