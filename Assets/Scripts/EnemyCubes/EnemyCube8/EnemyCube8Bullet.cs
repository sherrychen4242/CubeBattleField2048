using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube8Bullet : MonoBehaviour
{
    [SerializeField] float bulletSurvivalTime;
    [SerializeField] Timer timer;
    [SerializeField] int bulletDamage;
    [SerializeField] GameObject bulletHitBloodEffect;

    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.CompareTag("PlayerCube"))
        {
            GameObject player = collision.gameObject;
            if (player.GetComponent<Health>() != null)
            {

                
                Vector3 dir = gameObject.transform.position - collision.gameObject.transform.position;
                GameObject blood = Instantiate(bulletHitBloodEffect, collision.transform.position + dir.normalized * collision.gameObject.transform.localScale.x/2, Quaternion.EulerAngles(0, -90, 0));
                blood.transform.forward = dir.normalized;
                blood.transform.localScale *= collision.gameObject.transform.localScale.x / 2;
                player.GetComponent<Health>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
