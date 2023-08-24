using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject poisonEffect;
    [SerializeField] int damage;

    public Rigidbody rb;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] playerCubes = GameObject.FindGameObjectsWithTag("PlayerCube");
        float minDistance = Mathf.Infinity;
        foreach(GameObject cube in playerCubes)
        {
            float distance = Vector3.Distance(transform.position, cube.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = cube;
            }
        }

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            rb.AddForce(dir.normalized * speed, ForceMode.VelocityChange);
            
            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = dir * speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCube"))
        {
            if (other.gameObject.GetComponentInParent<Player>() != null)
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundEffects.EnemyCube32BubbleExplosionSound);
                other.gameObject.GetComponentInParent<Player>().TakeDamage(damage);
                Instantiate(poisonEffect, transform.position, Quaternion.identity);
                //SoundManager.Instance.PlaySound(SoundManager.SoundEffects.EnemyCube32PoisonSound);
                Destroy(gameObject);
            }
            
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundEffects.EnemyCube32BubbleExplosionSound);
            //SoundManager.Instance.PlaySound(SoundManager.SoundEffects.EnemyCube32PoisonSound);
            Instantiate(poisonEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
