using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStat
{

    //private bool canBeAttacked;
    private Animator animator;

    private AtkMng atkMng;
    private Weapon weapon = null;

    private void Start()
    {
        animator =  this.gameObject.GetComponentInChildren<Animator>();
        atkMng = this.gameObject.GetComponent<AtkMng>();
        weapon = this.gameObject.GetComponentInChildren<Weapon>();


        Equipment();
    }
    void Equipment()
    {
        atkMng.IsEquippedWeapon = true;
        atkMng.Weapon = weapon;

        atkMng.AtkPower += weapon.damage;
        atkMng.AtkSpeed += weapon.attackSpeed;
        
    }

    private void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();

        // effect
        // death animation
        this.GetComponent<DropTable>().GetRandomItem();

        animator.SetBool("isDeath", true);
        Destroy(gameObject);

    }

}
