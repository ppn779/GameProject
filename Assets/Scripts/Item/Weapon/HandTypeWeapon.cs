using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTypeWeapon : Weapon
{
    public float atkStartDist = 0.0f;
    public float attackRange = 0.0f;
    public float weaponAngle = 0.0f;

    [SerializeField] private GameObject weaponMesh = null;
    private WeaponMeshCtrl weaponMeshCtrl = null;

    private Transform tr = null;
    private Transform objTr = null;

    private float waitingTimeForAtk;
    private float time;
    private bool attackSwitchOn;

    private bool isReady = false;

    private void Start()
    {
        tr = this.transform;
        if (weaponMesh == null) Debug.LogError("WeaponMesh is missing!!");
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

    public override void Attack(Transform objTr, float waitingTimeForAtk)
    {
        if (!isReady)
        {
            this.objTr = objTr;
            this.waitingTimeForAtk = waitingTimeForAtk;

            float[] tmpAngle = new float[] { this.objTr.rotation.y - (weaponAngle / 2), this.objTr.rotation.y + (weaponAngle / 2) };
            weaponMeshCtrl.makeFanShape(tmpAngle, objTr, attackRange, atkStartDist);

            isReady = true;

            //StartCoroutine(MakeTransformMesh());
        }

        time = 0.0f;
        attackSwitchOn = true;
        StartCoroutine(MakeTransformMesh());
    }

    private IEnumerator MakeTransformMesh()
    {
        while (waitingTimeForAtk > time)
        {
            time += Time.deltaTime;
            //콜리전에 사용할 Mesh를 만든다.
            if (weaponMeshCtrl != null)
            {
                if (attackSwitchOn)
                {
                    //Debug.Log("Start Pos   : " +atkStartPos);
                    //float[] tmpAngle = new float[] { this.objTr.rotation.y - (weaponAngle / 2), this.objTr.rotation.y + (weaponAngle / 2) };
                    //weaponMeshCtrl.makeFanShape(tmpAngle, objTr, attackRange, atkStartDist);
                    attackSwitchOn = false;

                    weaponMeshCtrl.gameObject.SetActive(true);
                }
                else
                {
                    //weaponMeshCtrl.clearShape();
                    weaponMeshCtrl.gameObject.SetActive(false);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
