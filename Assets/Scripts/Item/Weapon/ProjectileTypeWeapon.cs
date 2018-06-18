using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeWeapon : Weapon
{
    [SerializeField] private GameObject projectile = null;

    private Transform objTr;

    public override void Attack(Transform objTr, float waitingTimeForAtk)
    {
        this.objTr = objTr;
        Fire();
    }

    private void Fire()
    {
        if (usableCount > 0)
        {
            GameObject go = Instantiate<GameObject>(projectile, (this.transform.position + (this.transform.forward * 10)), objTr.rotation);
            go.transform.parent = this.transform;
            SubtractUsableCount(1);
        }
    }
}
