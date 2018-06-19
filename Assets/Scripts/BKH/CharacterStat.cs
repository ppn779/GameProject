using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 0;// { get; private set; }
    public float damage = 10f;
    public float armor = 0f;

    public event System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor;

        currentHealth -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
