using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    [SerializeField] GameObject bloodSplashEffect;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject oneLevelLowerCube;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    void Die()
    {
        if (currentHealth <= 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundEffects.EnemyDieSound);
            GameObject bloodSplash = Instantiate(bloodSplashEffect, transform.position, Quaternion.identity);
            float scale = gameObject.transform.localScale.x;
            bloodSplash.transform.localScale = new Vector3(scale, scale, scale);
            if (gameObject.CompareTag("Enemy") && maxHealth == 2)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                
                Vector3 newPos1 = new Vector3(transform.position.x + Random.Range(2f * gameObject.transform.localScale.x, 4f * gameObject.transform.localScale.x),
                    transform.position.y,
                    transform.position.z + Random.Range(2f * gameObject.transform.localScale.x, 4f * gameObject.transform.localScale.x));
                Vector3 newPos2 = new Vector3(transform.position.x + Random.Range(2f * gameObject.transform.localScale.x, 4f * gameObject.transform.localScale.x),
                    transform.position.y,
                    transform.position.z + Random.Range(2f * gameObject.transform.localScale.x, 4f * gameObject.transform.localScale.x));
                int count = 0;
                while (Vector3.Distance(newPos1, newPos2) < 2f * gameObject.transform.localScale.x && count < 10)
                {
                    newPos1 = new Vector3(transform.position.x + Random.Range(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.x),
                                            transform.position.y,
                                            transform.position.z + Random.Range(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.x));
                    newPos2 = new Vector3(transform.position.x + Random.Range(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.x),
                                            transform.position.y,
                                            transform.position.z + Random.Range(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.x));
                    count += 1;
                }

                Instantiate(oneLevelLowerCube, newPos1, Quaternion.identity);
                Instantiate(oneLevelLowerCube, newPos2, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
