using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStat {

    [SerializeField]
    private float atkSpeed = 0f;
    [SerializeField]
    private float movementSpeed = 0f;
    [SerializeField]
    private float resistance = 0f;//저항력

    private void Update()
    {
        if (currentHealth > 0)
        {
            currentHealth -= (Time.deltaTime + 1.0f);
        }
        else
        {
            Die();
        }
    }

    public void HealthUp(float HP)
    {
        currentHealth += HP;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float AtkSpeed
    {
        get
        {
            return atkSpeed;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
    }

    public float Resistance
    {
        get
        {
            return resistance;
        }
    }

    public override void Die()
    {
        base.Die();

        // effect / death animation

        //Canvas canvas = ;


        Destroy(gameObject);
    }
}
