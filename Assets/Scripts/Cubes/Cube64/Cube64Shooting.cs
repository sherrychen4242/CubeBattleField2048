using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube64Shooting : CubeShooting
{
    #region PUBLIC VARIABLES

    #endregion

    #region PRIVATE VARIABLES

    #endregion

    public override void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);

        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, bulletPos.position.y, mousePosition.z - transform.position.z);
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
    }
}
