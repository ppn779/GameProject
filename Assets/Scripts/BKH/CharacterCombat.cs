using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterStat))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    private float attackCooldowm = 0f;

    const float combatCooldown = 0.0f;

   
    public float lastAttackTime = 0.0f;

    public bool inCombat { get; private set; }
    public event System.Action OnAttack;
    

    CharacterStat myStats;
    CharacterStat otherStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStat>();
    }

    private void Update()
    {
        attackCooldowm -= Time.deltaTime;

        if(Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }

    public void Attack(CharacterStat targetStats)
    {
        if(attackCooldowm <= 0f)
        {
            otherStats = targetStats;

            if(OnAttack != null)
            {
                //OnAttack();
            }

            attackCooldowm = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }
}
