using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube16Shooting : CubeShooting, IDataPersistence
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
    public void LoadData(GameData data)
    {
        shootingCoolDownTime = data.cube16_CoolDownTime;
        bulletSpeed = data.cube16_BulletSpeed;
    }

    public void SaveData(ref GameData data)
    {
        data.cube16_CoolDownTime = shootingCoolDownTime;
        data.cube16_BulletSpeed = bulletSpeed;
    }

    public override void Shooting()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.Cube16ShootingSound);
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);

        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, 0f, mousePosition.z - transform.position.z);
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
    }
}
