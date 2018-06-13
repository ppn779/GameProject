using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTypeWeapon : Weapon {
    public float atkStartDist = 0.0f;
    public float attackRange = 0.0f;
    public float weaponAngle = 0.0f;

    [SerializeField] private GameObject weaponMesh = null;
    private WeaponMeshCtrl weaponMeshCtrl;

    private float atkTimer;
    private bool isAtkSwitchOn = false;
    private bool isAtkTimerOn = false;

    private Transform tr;
    private Transform playerTr;

    private void Start()
    {
        tr = this.transform;
        Instantiate<GameObject>(weaponMesh, tr);
        weaponMeshCtrl = GetComponentInChildren<WeaponMeshCtrl>();
    }

    private void Update()
    {
        UpdateTransformMesh();
    }

    public override void Attack(bool atkSwitch,Transform playerTr)
    {
        if (!this.isAtkTimerOn)
        {
            //Debug.Log("atkMngOn");
            this.isAtkTimerOn = atkSwitch;
            this.isAtkSwitchOn = atkSwitch;
            atkTimer = 2.0f;
            this.playerTr = playerTr;
        }
    }

    private void UpdateTransformMesh()
    {
        //콜리전에 사용할 Mesh를 만든다.
        if (isAtkTimerOn && atkTimer > 0.0f)
        {
            if (isAtkSwitchOn)
            {
                if (weaponMeshCtrl != null)
                {
                    Vector3 atkStartPos = this.playerTr.position + (this.playerTr.forward * (atkStartDist));
                    float[] tmpAngle = new float[] { this.playerTr.rotation.y - (weaponAngle / 2), this.playerTr.rotation.y + (weaponAngle / 2) };
                    weaponMeshCtrl.makeFanShape(tmpAngle, atkStartPos, attackRange, this.playerTr.rotation);
                    isAtkSwitchOn = false;

                }
            }
            else
            {

                weaponMeshCtrl.clearShape();
            }
            atkTimer -= Time.deltaTime + (attackSpeed / 50);
        }
        else
        {
            isAtkTimerOn = false;
        }
    }
}
