using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public float remainTime = 5f;
    public float damage = 0;
    public float attackSpeed = 0.0f;
    public int durabilityMax = 100;
    public int durabilityCur = 100;
    public int usableCount = 5;
    public int durabilityReduce = 50;
    public bool isWeaponTypeMelee = true;

    private bool isPlayerEquipped = false;
    public Color SlotColor = Color.green;

    public virtual void Attack(Transform _objTr, float _atkPow) { }
    public virtual float WeaponAngle { get; set; }
    public virtual float AtkRangeDist { get; set; }
    public virtual float AtkStartDist { get; set; }
    
    public bool IsPlayerEquipped { get; set; }

    public void SubtractDurability(int amount)
    {
        durabilityCur -= amount;
        if (durabilityCur > 66)
            SlotColor = Color.green;
        else if (durabilityCur > 33)
            SlotColor = Color.yellow;
        else
            SlotColor = Color.red;
        if (durabilityCur <= 0) {
            SlotColor = Color.white;
            DestroyWeapon();
        }
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

    //public float CalculatedAtkPow
    //{
    //    get
    //    {
    //        return calculatedAtkPow;
    //    }
    //}

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
