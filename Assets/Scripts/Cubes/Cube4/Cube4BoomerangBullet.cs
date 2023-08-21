using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube4BoomerangBullet : MonoBehaviour, IDataPersistence
{
    [SerializeField] float bulletSurvivalTime;
    [SerializeField] Timer timer;
    [SerializeField] int bulletDamage;

    [SerializeField] float spinSpeed;
    [SerializeField] float flyBackTime;
    public bool setUpFlyBackTimer;

    // Start is called before the first frame update
    void Start()
    {
        LoadData(DataPersistenceManager.instance.gameData);

        setUpFlyBackTimer = false;

        timer.maxTime = bulletSurvivalTime;
        timer.startMethodRightAway = false;
        timer.StartTimer();

    }

    // Update is called once per frame
    void Update()
    {
        Spin();

        timer.StartTimer();
        if (timer.CanStartMethod)
        {
            Destroy(gameObject);
        }

        if (!setUpFlyBackTimer)
        {
            timer.AddTimer("BoomerangTimer", flyBackTime, false);
            timer.StartTimer("BoomerangTimer");
            setUpFlyBackTimer = true;
        }

        if (timer.CanStartMethodForTimer("BoomerangTimer"))
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(-vel.x, 0f, -vel.z);
            GetComponent<Rigidbody>().velocity = newVel;
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

    public void Spin()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    public void LoadData(GameData data)
    {
        bulletSurvivalTime = data.cube4_BulletSurvivalTime;
        flyBackTime = data.cube4_BulletFlyBackTime;
        transform.localScale = new Vector3(data.cube4_BulletSize, data.cube4_BulletSize, data.cube4_BulletSize);
    }

    public void SaveData(ref GameData data)
    {
        data.cube4_BulletSurvivalTime = bulletSurvivalTime;
        data.cube4_BulletFlyBackTime = flyBackTime;
        data.cube4_BulletSize = transform.localScale.x;
    }
}
