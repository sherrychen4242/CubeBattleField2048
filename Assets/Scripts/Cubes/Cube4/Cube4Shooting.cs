using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube4Shooting : CubeShooting
{
    #region PUBLIC VARIABLES
    
    #endregion

    #region PRIVATE VARIABLES

    #endregion

    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        
        base.Update();

    }

    public override void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);

        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, 0f, mousePosition.z - transform.position.z);
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
    }

}
