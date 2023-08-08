using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBomb : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] float explosionTime;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float explosionRadius;
    [SerializeField] int explosionDamage;
    [SerializeField] float selfDestroyTime;

    public bool startCountDown;
    public bool selfDestroyTimerSetUp;

    // Start is called before the first frame update
    void Start()
    {
        startCountDown = false;
        selfDestroyTimerSetUp = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!selfDestroyTimerSetUp)
        {
            timer.AddTimer("SelfDestroy", selfDestroyTime, false);
            timer.StartTimer("SelfDestroy");
            selfDestroyTimerSetUp = true;
        }

        if ((startCountDown && timer.CanStartMethod) || timer.CanStartMethodForTimer("SelfDestroy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        CauseDamage();
        Destroy(gameObject);
    }

    void CauseDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("PlayerCube") && collider.gameObject.GetComponentInParent<Player>() != null)
            {
                collider.gameObject.GetComponentInParent<Player>().TakeDamage(explosionDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCube") || collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            startCountDown = true;
            timer.maxTime = explosionTime;
            timer.startMethodRightAway = false;
            timer.StartTimer();
        }
    }
}
