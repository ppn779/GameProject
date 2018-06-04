using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStat
{

    //private bool canBeAttacked;
    public Animator animator;

    private AtkMng atkMng;
    private Equipment equipment = null;
    private Weapon weapon = null;

    private void Start()
    {
        animator = animator = this.gameObject.GetComponentInChildren<Animator>();
        atkMng = this.gameObject.GetComponent<AtkMng>();
        equipment = this.gameObject.GetComponent<Equipment>();
        weapon = this.gameObject.GetComponentInChildren<Weapon>();


        Equipment();
    }
    void Equipment()
    {
        atkMng.AtkPower += weapon.damage;
        atkMng.AtkSpeed += weapon.attackSpeed;
        atkMng.AtkAngle += weapon.weaponAngle;
        atkMng.AtkRangeDist += weapon.attackRange;
        atkMng.AtkStartDist += weapon.atkStartDist;
    }

    public override void Die()
    {
        base.Die();

        // effect
        // death animation
        animator.SetBool("isDeath", true);
        Destroy(gameObject);

    }

}
