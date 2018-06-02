using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStat
{

    //private bool canBeAttacked;
    public Animator animator;

    private void Start()
    {
        animator = animator = this.gameObject.GetComponentInChildren<Animator>();
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
