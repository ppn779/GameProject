using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 0;// { get; private set; }

    public float damage;
    public float armor;

    public event System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        damage -= armor;

        currentHealth -= damage;

        //Debug.Log(transform.name + " takes " + damage + " damage ");

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        //Debug.Log(transform.name + "-> " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // some way
        Debug.Log(transform.name + " died.");
    }
}
