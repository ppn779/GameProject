using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMng : MonoBehaviour
{
    private GameObject obj;
    private Animator animator;
    private Weapon weapon;
    private float atkPower;
    private float atkSpeed;
    private float atkTimer;
    private bool isAtkSwitchOn = false;
    private bool isEquippedWeapon = false;

    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    public Weapon Weapon
    {
        set
        {
            weapon = value;
        }
    }

    public void Attack()
    {
        if (IsEquippedWeapon && atkTimer <= 0)
        {
            atkTimer = 2.0f;
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        while (atkTimer > 0.0f)
        {
            if (!isAtkSwitchOn)
            {
                //animator.SetTrigger("Attack");
            }
            weapon.Attack();
            this.atkTimer -= Time.deltaTime + (atkSpeed / 50);
            yield return null;
        }
    }

    private void WeaponAttack()
    {
        isAtkSwitchOn = true;
        Debug.Log("공격");
    }

    public float AtkPower
    {
        get
        {
            return atkPower;
        }
        set
        {
            atkPower = value;
        }
    }

    public float AtkSpeed
    {
        get
        {
            return atkSpeed;
        }
        set
        {
            atkSpeed = value;
        }
    }

    public bool IsEquippedWeapon
    {
        get
        {
            return isEquippedWeapon;
        }
        set
        {
            isEquippedWeapon = value;
        }
    }
}
