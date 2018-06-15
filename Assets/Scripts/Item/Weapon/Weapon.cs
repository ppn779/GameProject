using System.Collections;
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
    
    public virtual void Attack(bool atkSwitch,Transform playerTr) { }

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

    private IEnumerator ExpiredRemainTime()
    {
        DestroyWeapon();
        yield return null;
    }
    private void DestroyWeapon()
    {
        IsDestroyed = true;
        DestroyItem();
    }

    //private void UpdateTransformMesh()
    //{
    //    //콜리전에 사용할 Mesh를 만든다.
    //    if (isAtkTimerOn && atkTimer > 0.0f)
    //    {
    //        if (isAtkSwitchOn)
    //        {
    //            if (weaponMesh != null)
    //            {
    //                Vector3 atkStartPos = this.transform.position + (this.transform.forward * (atkStartDist));
    //                float[] tmpAngle = new float[] { this.transform.rotation.y - (weaponAngle / 2), this.transform.rotation.y + (weaponAngle / 2) };
    //                weaponMesh.makeFanShape(tmpAngle, atkStartPos, atkRangeDist, this.transform);
    //                isAtkSwitchOn = false;

    //            }
    //        }
    //        else
    //        {

    //            weaponMesh.clearShape();
    //        }
    //        this.atkTimer -= Time.deltaTime + (atkSpeed / 50);
    //    }
    //    else
    //    {
    //        isAtkTimerOn = false;
    //    }
    //}
}
