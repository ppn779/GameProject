﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public float remainTime = 5f;
    public float damage = 0;
    public float attackSpeed = 0.0f;
    public int durability = 100;
    public int usableCount = 5;
    private bool isPlayerEquipped = false;
    protected float calculatedAtkPow;

    public virtual void Attack(Transform objTr, float atkPow, float waitingTimeForAtk) { }

    public bool IsPlayerEquipped
    {
        get { return isPlayerEquipped; }
        set { isPlayerEquipped = value; }
    }

    public void SubtractDurability(int amount)
    {
        durability -= amount;
        if (durability <= 0) { DestroyWeapon(); }
    }

    public void SubtractUsableCount(int count)
    {
        usableCount -= count;
        if (usableCount <= 0) { DestroyWeapon(); }
    }

    public void OnStartRemainTime(float time)
    {
        StartCoroutine(ExpiredRemainTime());
    }

    public float CalculatedAtkPow
    {
        get
        {
            return calculatedAtkPow;
        }
    }

    private IEnumerator ExpiredRemainTime()
    {
        DestroyWeapon();
        yield return null;
    }
    private void DestroyWeapon()
    {
        IsDestroyed = true;
        //DestroyItem();
    }
}
