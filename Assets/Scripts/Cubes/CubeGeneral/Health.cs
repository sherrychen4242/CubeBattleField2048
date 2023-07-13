using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    [SerializeField] GameObject bloodSplashEffect;
    [SerializeField] GameObject coinPrefab;

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
            GameObject bloodSplash = Instantiate(bloodSplashEffect, transform.position, Quaternion.identity);
            float scale = gameObject.transform.localScale.x;
            bloodSplash.transform.localScale = new Vector3(scale, scale, scale);
            if (gameObject.CompareTag("Enemy") && maxHealth == 2)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
