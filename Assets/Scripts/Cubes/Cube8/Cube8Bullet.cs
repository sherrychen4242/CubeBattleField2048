using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube8Bullet : MonoBehaviour, IDataPersistence
{
    [SerializeField] float bulletSurvivalTime;
    [SerializeField] Timer timer;
    [SerializeField] int bulletDamage;

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
                enemy.GetComponent<Health>().TakeDamage(bulletDamage);
            }
        }
    }

    public void LoadData(GameData data)
    {
        bulletSurvivalTime = data.cube8_BulletSurvivalTime;
        transform.localScale = new Vector3(data.cube8_BulletSize, data.cube8_BulletSize, data.cube8_BulletSize);
    }

    public void SaveData(ref GameData data)
    {
        data.cube8_BulletSurvivalTime = bulletSurvivalTime;
        data.cube8_BulletSize = transform.localScale.x;
    }
}
