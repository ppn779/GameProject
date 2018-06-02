using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public int damage;
    public int armor;

    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
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

        Debug.Log(this.gameObject.tag + "은(는) 데미지 " + damage + "을 입었다.");
    }

    public virtual void Die()
    {
        // some way
        Debug.Log(transform.name + " died.");
    }
}
