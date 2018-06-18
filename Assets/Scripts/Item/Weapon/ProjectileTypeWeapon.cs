using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeWeapon : Weapon
{
    [SerializeField] private GameObject projectile = null;

    private Quaternion objRot;

    public override void Attack(Transform objTr, float objAtkPow, float waitingTimeForAtk)
    {
        this.objRot = objTr.rotation;
        calculatedAtkPow = damage + objAtkPow;
        Fire();
    }

    private void Fire()
    {
        if (usableCount > 0)
        {
            //GameObject go = Instantiate<GameObject>(projectile, this.transform.position, objRot);
            //go.transform.parent = this.transform;
            GameObject objProjectile=Instantiate<GameObject>(projectile, this.transform.position, objRot);
            objProjectile.GetComponent<ProjectileCtrl>().CalculatedAtkPow = calculatedAtkPow;
            SubtractUsableCount(1);
        }
    }
}
