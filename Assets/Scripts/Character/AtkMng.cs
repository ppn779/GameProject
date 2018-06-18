using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMng : MonoBehaviour
{
    private GameObject obj;
    private Animator animator;
    private Weapon weapon;
    private float atkPower;
    private float atkSpeed;
    private float atkTimer;
    private bool isAtkTimerOn = false;
    private bool isAtkSwitchOn = false;
    private bool isEquippedWeapon = false;

    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isEquippedWeapon)
        {
            if (isAtkTimerOn && atkTimer > 0.0f)
            {
                Debug.Log("공격");
                weapon.Attack(isAtkSwitchOn, obj.transform);
                if (isAtkSwitchOn)
                {   //animator.SetTrigger("attack");
                    isAtkSwitchOn = false;
                }
                this.atkTimer -= Time.deltaTime + (atkSpeed / 50);
            }

            else
            {
                isAtkTimerOn = false;
            }
        }
    }

    public Weapon Weapon
    {
        set
        {
            weapon = value;
        }
    }

    public void Attack()
    {
        if (!isAtkTimerOn)
        {
            isAtkSwitchOn = true;
            isAtkTimerOn = true;
            atkTimer = 2.0f;
        }
    }

    //public WeaponMeshCtrl WeaponMeshCtrl
    //{
    //    get
    //    {
    //        return weaponMesh;
    //    }
    //    set
    //    {
    //        weaponMesh = value;
    //    }
    //}

    //public ProjectileCtrl ProjectileCtrl
    //{
    //    get
    //    {
    //        return projectileCtrl;
    //    }
    //    set
    //    {
    //        projectileCtrl = value;
    //    }
    //}

    //public float AtkStartDist
    //{
    //    get
    //    {
    //        return atkStartDist;
    //    }
    //    set
    //    {
    //        atkStartDist = value;
    //    }
    //}
    //public float AtkAngle
    //{
    //    get
    //    {
    //        return atkAngle;
    //    }
    //    set
    //    {
    //        atkAngle = value;
    //    }
    //}

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

    //public float AtkRangeDist
    //{
    //    get
    //    {
    //        return atkRangeDist;
    //    }
    //    set
    //    {
    //        atkRangeDist = value;
    //    }
    //}

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

    //public bool HasProjectile
    //{
    //    get
    //    {
    //        return hasProjectile;
    //    }
    //    set
    //    {
    //        hasProjectile = value;
    //    }
    //}

    //public void AtkMngOn(bool isAtkTimerOn)
    //{
    //    if (!this.isAtkTimerOn)
    //    {
    //        //Debug.Log("atkMngOn");
    //        this.isAtkTimerOn = isAtkTimerOn;
    //        this.isAtkSwitchOn = isAtkTimerOn;
    //        atkTimer = 2.0f;
    //    }
    //}


    //public int Attack()
    //{
    //    return atkPower;
    //}

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

    //private void UpdateTransformMesh()
    //{
    //    콜리전에 사용할 Mesh를 만든다.
    //    if (isAtkTimerOn && atkTimer > 0.0f)
    //    {
    //        if (isAtkSwitchOn)
    //        {
    //            if (weaponMesh != null && !hasProjectile)
    //            {

    //                Vector3 atkStartPos = this.transform.position + (this.transform.forward * (atkStartDist));
    //                float[] tmpAngle = new float[] { this.transform.rotation.y - (atkAngle / 2), this.transform.rotation.y + (atkAngle / 2) };
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
