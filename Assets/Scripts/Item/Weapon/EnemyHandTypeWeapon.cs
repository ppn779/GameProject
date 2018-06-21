using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandTypeWeapon : Weapon
{
    public float atkStartDist = 0.0f;
    public float attackRange = 0.0f;
    public float weaponAngle = 0.0f;

    private WeaponMeshCtrl weaponMeshCtrl;

    private Transform objTr;

    private float waitingTimeForAtk;
    private float time;
    private bool attackSwitchOn;
    private bool isReady = false;

    private void Start()
    {
        weaponMeshCtrl = GetComponentInChildren<WeaponMeshCtrl>();
        waitingTimeForAtk=3.0f-attackSpeed;
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

    public override void Attack(Transform objTr, float objAtkPow)
    {
        this.objTr = objTr;
        time = 0.0f;
        attackSwitchOn = true;
        if (!isReady)
        {
            weaponMeshCtrl.gameObject.SetActive(false);
            float[] tmpAngle = new float[] { objTr.rotation.y - (weaponAngle / 2), objTr.rotation.y + (weaponAngle / 2) };
            weaponMeshCtrl.makeFanShape(tmpAngle, objTr, attackRange, atkStartDist);

            isReady = true;
        }
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
                    weaponMeshCtrl.gameObject.SetActive(true);
                    attackSwitchOn = false;
                }
                else
                {
                    weaponMeshCtrl.gameObject.SetActive(false);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
