using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMng : MonoBehaviour
{
    private Weapon weapon = null;
    private float atkPower;
    private float atkSpeed;
    private bool isEquippedWeapon = false;

    public Weapon Weapon
    {
        set
        {
            weapon = value;
        }
    }

    public void Attack()
    {
        if (isEquippedWeapon)
        {
            Debug.Log("보스 공격");
            weapon.Attack(this.transform, atkPower);
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
}
