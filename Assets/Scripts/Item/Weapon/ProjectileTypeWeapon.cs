using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeWeapon : Weapon
{
    [SerializeField] private GameObject projectile = null;

    private Quaternion objRot;

    
    public override void Attack(Transform _objTr, float _objAtkPow)
    {
        this.objRot = _objTr.rotation;

        Fire();
    }
    
    private void Fire()
    {
        if (usableCount > 0)
        {
            //GameObject go = Instantiate<GameObject>(projectile, this.transform.position, objRot);
            //go.transform.parent = this.transform;
            Vector3 newPos = this.transform.position;
            newPos.y = 1f;
            GameObject objProjectile=Instantiate<GameObject>(projectile, newPos, objRot);
            SubtractUsableCount(1);
        }
    }
}
