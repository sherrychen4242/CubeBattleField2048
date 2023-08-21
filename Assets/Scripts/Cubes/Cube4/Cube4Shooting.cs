using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube4Shooting : CubeShooting, IDataPersistence
{
    #region PUBLIC VARIABLES
    
    #endregion

    #region PRIVATE VARIABLES

    #endregion

    public override void Start()
    {
        LoadData(DataPersistenceManager.instance.gameData);
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

    public void LoadData(GameData data)
    {
        shootingCoolDownTime = data.cube4_CoolDownTime;
        bulletSpeed = data.cube4_BulletSpeed;
    }

    public void SaveData(ref GameData data)
    {
        data.cube4_CoolDownTime = shootingCoolDownTime;
        data.cube4_BulletSpeed = bulletSpeed;
    }
}
