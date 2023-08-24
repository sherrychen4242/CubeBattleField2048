using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube8Shooting : CubeShooting, IDataPersistence
{
    #region PUBLIC VARIABLES
    [SerializeField] float bulletRotationAngle;
    [SerializeField] int numberOfBullets;
    #endregion

    #region PRIVATE VARIABLES
    public float currentRadian;
    public float initialRadian;
    public float _midRadian;
    #endregion

    public override void Start()
    {
        LoadData(DataPersistenceManager.instance.gameData);

        base.Start();

        if (numberOfBullets % 2 == 1)
        {
            /* initialRadian = (int)(-(numberOfBullets - 1) / 2);*/
            initialRadian = ((int)(-((numberOfBullets - 1)/2))) * bulletRotationAngle;
        }
        else
        {
            initialRadian = (-(numberOfBullets / 2 - 0.5f)) * bulletRotationAngle;
        }
    }
    public override void Shooting()
    {
/*        Vector3 dir = new Vector3(mousePosition.x - transform.position.x, 0f, mousePosition.z - transform.position.z);
*/        
        List<Vector3> directions = CalculateBulletDirections();
        SoundManager.Instance.PlaySound(SoundManager.SoundEffects.Cube8ShootingSound);
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
            bullet.transform.forward = directions[i];
            bullet.GetComponent<Rigidbody>().AddForce(directions[i] * bulletSpeed, ForceMode.VelocityChange);
        }
    }

    private List<Vector3> CalculateBulletDirections()
    {
        List<Vector3> directions = new List<Vector3>();
        float radian = (bulletRotationAngle / 360) * (2f * Mathf.PI);
        float midRadian = Mathf.Atan2(mousePosition.z - bulletPos.transform.position.z, mousePosition.x - bulletPos.transform.position.x);
        _midRadian = midRadian;
        float x = bulletPos.transform.position.x;
        float z = bulletPos.transform.position.z;
        if (numberOfBullets % 2 == 1)
        {
            initialRadian = midRadian - ((numberOfBullets - 1) / 2) * radian;
            
        }
        else if (numberOfBullets % 2 == 0)
        {
            initialRadian = midRadian - ((numberOfBullets / 2) - 0.5f) * radian;
            
        }

        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentRadian = initialRadian + i * radian;
            float newX = x + Mathf.Cos(currentRadian);
            float newZ = z + Mathf.Sin(currentRadian);
            Vector3 newDir = new Vector3(newX - x, 0f, newZ - z);
            newDir = newDir.normalized;
            directions.Add(newDir);
        }

        return directions;
    }

    public void LoadData(GameData data)
    {
        shootingCoolDownTime = data.cube8_CoolDownTime;
        bulletSpeed = data.cube8_BulletSpeed;
        numberOfBullets = data.cube8_NumBullets;
        bulletRotationAngle = data.cube8_BulletRotationAngle;

    }

    public void SaveData(ref GameData data)
    {
        data.cube8_CoolDownTime = shootingCoolDownTime;
        data.cube8_BulletSpeed = bulletSpeed;
        data.cube8_NumBullets = numberOfBullets;
        data.cube8_BulletRotationAngle = bulletRotationAngle;
    }
}
