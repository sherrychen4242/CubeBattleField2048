using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube32Bullet : MonoBehaviour, IDataPersistence
{
    [SerializeField] float bulletSurvivalTime;
    [SerializeField] Timer timer;
    [SerializeField] int bulletDamage;
    [SerializeField] GameObject fireEffect;

    // Start is called before the first frame update
    void Start()
    {
        LoadData(DataPersistenceManager.instance.gameData);

        timer.maxTime = bulletSurvivalTime;
        timer.startMethodRightAway = false;
        timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer.StartTimer();
        if (timer.CanStartMethod)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            if (enemy.GetComponent<Health>() != null)
            {
                Instantiate(fireEffect, collision.transform.position, Quaternion.identity);
                enemy.GetComponent<Health>().TakeDamage(bulletDamage);
            }
        }
    }

    public void LoadData(GameData data)
    {
        bulletSurvivalTime = data.cube32_BulletSurvivalTime;
        transform.localScale = new Vector3(data.cube32_BulletSize, data.cube32_BulletSize, data.cube32_BulletSize);
    }

    public void SaveData(ref GameData data)
    {
        data.cube32_BulletSurvivalTime = bulletSurvivalTime;
        data.cube32_BulletSize = transform.localScale.x;
    }
}
