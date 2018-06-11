using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private GameObject weaponMesh = null;
    public float atkStartDist = 0.0f;
    public float attackRange = 0.0f;
    public float attackSpeed = 0.0f;
    public float weaponAngle = 0.0f;
    public float remainTime = 5f;
    public int damage = 0;
    public int usableCount = 5;
    public int durability = 100;
    public bool hasProjectile;

    private Transform tr = null;
    private bool isPlayerEquipped = false;

    public bool IsPlayerEquipped
    {
        get { return isPlayerEquipped; }
        set { isPlayerEquipped = value; }
    }

    private void Start()
    {
        tr = this.transform;
        Instantiate<GameObject>(weaponMesh, tr);
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
    private IEnumerator ExpiredRemainTime()
    {
        DestroyWeapon();
        yield return null;
    }
    private void DestroyWeapon()
    {
        IsDestroyed = true;
    }
}
