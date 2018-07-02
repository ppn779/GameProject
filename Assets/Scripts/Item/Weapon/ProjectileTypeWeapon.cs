﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeWeapon : Weapon
{
    [SerializeField] private GameObject projectile = null;

    private Transform tr;

    
    public override void Attack(Transform _objTr, float _objAtkPow)
    {
        this.tr = _objTr;

        Fire(_objAtkPow);
    }
    
    private void Fire(float _objAtkPow)
    {
        if (usableCount > 0)
        {
            //GameObject go = Instantiate<GameObject>(projectile, this.transform.position, this.transform.rotation);
            //go.transform.parent = this.transform;
            Vector3 newPos = this.transform.position;
            newPos += (tr.forward * 1.2f);
            newPos.y = 1.0f;

            GameObject objProjectile=Instantiate<GameObject>(projectile, newPos, tr.transform.rotation);
            //Debug.Log("objProjectile = " + objProjectile);
            objProjectile.GetComponent<ProjectileCtrl>().damage = _objAtkPow;
            SubtractUsableCount(1);
            
        }
    }
}
