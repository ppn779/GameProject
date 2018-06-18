using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTypeWeapon : Weapon {
    public float atkStartDist = 0.0f;
    public float attackRange = 0.0f;
    public float weaponAngle = 0.0f;

    [SerializeField] private GameObject weaponMesh = null;
    private WeaponMeshCtrl weaponMeshCtrl;
    
    private bool isAtkSwitchOn = false;

    private Transform tr;
    private Transform objTr;

    private void Start()
    {
        tr = this.transform;
        Instantiate<GameObject>(weaponMesh, tr);
        weaponMeshCtrl = GetComponentInChildren<WeaponMeshCtrl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (IsPlayerEquipped)
            {
                SubtractDurability(25);
                Debug.Log(durability);
            }
        }
    }

    public override void Attack(bool atkSwitch,Transform objTr)
    {
            this.isAtkSwitchOn = atkSwitch;
            this.objTr = objTr;
        MakeTransformMesh();
    }

    public void MakeTransformMesh()
    {
        //콜리전에 사용할 Mesh를 만든다.
       
            if (isAtkSwitchOn)
            {
                if (weaponMeshCtrl != null)
                {
                    //Debug.Log("Start Pos   : " +atkStartPos);
                    float[] tmpAngle = new float[] { this.objTr.rotation.y - (weaponAngle / 2), this.objTr.rotation.y + (weaponAngle / 2) };
                    weaponMeshCtrl.makeFanShape(tmpAngle, objTr, attackRange,atkStartDist);
                    isAtkSwitchOn = false;

                }
            }
            else
            {

                weaponMeshCtrl.clearShape();
            }      
    }
}
