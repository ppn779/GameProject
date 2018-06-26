﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 0;// { get; private set; }
    public float damage = 10f;
    public float armor = 0f;

    private bool isDead = false;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }
    public event System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damage)
    {
        damage -= armor;

        currentHealth -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            return false;
        }

        return true;
    }

    public virtual void Die()
    {
        //Debug.Log(transform.name + " died.");
    }
}
