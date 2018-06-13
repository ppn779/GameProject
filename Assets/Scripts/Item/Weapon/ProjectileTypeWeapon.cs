using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeWeapon : Weapon
{
    [SerializeField] private GameObject projectile = null;
    private Vector3 direction;


    private float atkTimer;
    private bool isAtkSwitchOn = false;
    private bool isAtkTimerOn = false;

    private Transform playerTr;

    private void Update()
    {
        UpdateTransformMesh();
    }

    public override void Attack(bool atkSwitch, Transform playerTr)
    {
        if (!this.isAtkTimerOn)
        {
            //Debug.Log("atkMngOn");
            this.isAtkTimerOn = atkSwitch;
            this.isAtkSwitchOn = atkSwitch;
            atkTimer = 2.0f;
            this.playerTr = playerTr;
        }

        //if (usableCount > 0 && atkSwitch)
        //{
        //    if (isOverAtkWaitingTime)
        //    {
        //        tr = this.transform;
        //        Instantiate<GameObject>(projectile, tr.position, tr.rotation);
        //        SubtractUsableCount(1);
        //        isOverAtkWaitingTime = false;
        //    }
        //    atkTimer += Time.deltaTime;
        //    if (atkTimer > waitingTime)
        //    {
        //        isOverAtkWaitingTime = true;
        //    }
        //}
    }

    private void UpdateTransformMesh()
    {
        if (isAtkTimerOn && atkTimer > 0.0f)
        {
            if (usableCount>0 && isAtkSwitchOn)
            {
                Instantiate<GameObject>(projectile, this.transform.position, playerTr.rotation);
                SubtractUsableCount(1);
                isAtkSwitchOn = false;
            }
            atkTimer -= Time.deltaTime + (attackSpeed / 50);
        }
        else
        {
            isAtkTimerOn = false;
        }
    }
}
