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
    private float waitingTimeForAtk;
    private float time;
    private bool isAtkTimerOn = false;
    //private bool isAtkSwitchOn = false;
    private bool isEquippedWeapon = false;

    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAtkTimerOn && waitingTimeForAtk > time)
        {
            time += Time.deltaTime;
        }

        else
        {
            isAtkTimerOn = false;
        }

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
        if (!isAtkTimerOn && isEquippedWeapon)
        {
            Debug.Log("공격");
            animator.SetTrigger("Attack");
            isAtkTimerOn = true;
            waitingTimeForAtk = 3.0f - atkSpeed;
            time = 0.0f;
        }
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

    private void WeaponAttack()
    {
        weapon.Attack(obj.transform, waitingTimeForAtk);
    }
}
