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

    private void Start()
    {
        //currentHealth = maxHealth;
    }

    //public void HealthUp(float HP)
    //{
    //    currentHealth += HP;
    //    if (currentHealth > maxHealth)
    //    {
    //        currentHealth = maxHealth;
    //    }
    //}

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
        Vector3 newPos = this.transform.position;
        Quaternion quater = this.transform.rotation;
        newPos.y += 1f;

        Instantiate(ParticleMng.GetInstance().EffectSmallExp(), newPos, quater);
        AudioMng.GetInstance().PlaySound("PlayerDie", newPos, 100f);


        Destroy(gameObject);
        GameMng.Instance.GameOver();
    }
}
