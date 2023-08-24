using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube32Shooting : CubeShooting, IDataPersistence
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

    public override void Shooting()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.Cube32ShootingSound);
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.Euler(-90f, 0f, 0f));

        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, 0f, mousePosition.z - transform.position.z);
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
    }

    public void LoadData(GameData data)
    {
        shootingCoolDownTime = data.cube32_CoolDownTime;
        bulletSpeed = data.cube32_BulletSpeed;
    }

    public void SaveData(ref GameData data)
    {
        data.cube32_CoolDownTime = shootingCoolDownTime;
        data.cube32_BulletSpeed = bulletSpeed;
    }
}
