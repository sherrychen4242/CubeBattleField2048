using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube64Shooting : CubeShooting, IDataPersistence
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
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.Cube64ShootingSound);
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);

        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, bulletPos.position.y, mousePosition.z - transform.position.z);
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
    }

    public void LoadData(GameData data)
    {
        shootingCoolDownTime = data.cube64_CoolDownTime;
        bulletSpeed = data.cube64_BulletSpeed;
    }

    public void SaveData(ref GameData data)
    {
        data.cube64_CoolDownTime = shootingCoolDownTime;
        data.cube64_BulletSpeed = bulletSpeed;
    }
}
