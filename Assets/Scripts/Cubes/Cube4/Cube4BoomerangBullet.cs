using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube4BoomerangBullet : MonoBehaviour
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
}
