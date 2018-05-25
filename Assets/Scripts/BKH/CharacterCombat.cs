using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterStat))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    private float attackCooldowm = 0f;

    CharacterStat myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStat>();
    }

    private void Update()
    {
        attackCooldowm -= Time.deltaTime;
    }

    public void Attack(CharacterStat targetStats)
    {
        if(attackCooldowm <= 0f)
        {
            targetStats.TakeDamage(myStats.damage.GetValue());
            attackCooldowm = 1f / attackSpeed;
        }
    }
}
