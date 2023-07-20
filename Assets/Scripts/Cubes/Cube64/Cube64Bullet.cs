using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube64Bullet : MonoBehaviour
{
    [SerializeField] float bulletSurvivalTime;
    [SerializeField] Timer timer;
    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject electricHitEffect;

    public GameObject[] enemyList;
    public int currentNumHits;
    public bool initialEnemyHit;
    [SerializeField] int numberOfHits;
    [SerializeField] float findEnemyRadius;
    [SerializeField] LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentNumHits = 0;
        initialEnemyHit = false;

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

    private void FixedUpdate()
    {
        if (initialEnemyHit && currentNumHits < numberOfHits)
        {
            if (enemyList.Length > currentNumHits)
            {
                if (enemyList[currentNumHits] != null)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Vector3 dir = enemyList[currentNumHits].transform.position - transform.position;
                    GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
                }
                else
                {
                    int index = currentNumHits;
                    while (enemyList[index] == null && enemyList.Length - 1 > index)
                    {
                        index += 1;
                    }
                    
                    if (enemyList[index] != null)
                    {
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        Vector3 dir = enemyList[index].transform.position - transform.position;
                        GetComponent<Rigidbody>().AddForce(dir.normalized * bulletSpeed, ForceMode.VelocityChange);
                    }
                    
                }
                
            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            if (enemy.GetComponent<Health>() != null)
            {
                if (initialEnemyHit is false)
                {
                    initialEnemyHit = true;
                    currentNumHits += 1;
                    Instantiate(electricHitEffect, collision.transform.position, Quaternion.identity);
                    enemy.GetComponent<Health>().TakeDamage(bulletDamage);

                    enemyList = FindEnemiesInNeighbor(collision.transform.position);
                }
                else
                {
                    currentNumHits += 1;
                    Instantiate(electricHitEffect, collision.transform.position, Quaternion.identity);
                    enemy.GetComponent<Health>().TakeDamage(bulletDamage);
                }
                
            }
        }
    }

    private GameObject[] FindEnemiesInNeighbor(Vector3 origin)
    {
        Collider[] enemyColliders = Physics.OverlapSphere(origin, findEnemyRadius, whatIsEnemy);
        GameObject[] enemies = new GameObject[enemyColliders.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = enemyColliders[i].gameObject;
        }
        return enemies;
    }
}
