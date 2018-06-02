using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMng : MonoBehaviour
{
    [SerializeField]
    private string objTagName;
    private GameObject obj;
    private WeaponMeshCtrl weaponMesh;
    private CharacterStat stats;

    private int atkPower = 10;
    private float atkAngle = 50.0f;//0.0f로 바꿔야 함.
    private float atkRangeDist = 2.0f;//마찬가지
    private float atkSpeed = 0.0f;//- (stats.AtkSpeed / 140); 공격속도 숫자가 커질수록 타이머 시간은 줄어듬.
    private float atkTimer = 0.0f;
    private float atkStartDist = 0.0f;
    private bool isAtkSwitchOn = false;
    private bool isAtkTimerOn = false;

    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag(objTagName);
        weaponMesh = gameObject.GetComponentInChildren<WeaponMeshCtrl>();
    }

    private void FixedUpdate()
    {
        UpdateTransformMesh();
    }

    public float AtkAngle
    {
        get
        {
            return atkAngle;
        }
        set
        {
            atkAngle = value;
        }
    }

    public int AtkPower
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

    public float AtkRangeDist
    {
        get
        {
            return atkRangeDist;
        }
        set
        {
            atkRangeDist = value;
        }
    }

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

    public void AtkMngOn(bool isAtkTimerOn)
    {
        if (!this.isAtkTimerOn)
        {
            //Debug.Log("atkMngOn");
            this.isAtkTimerOn = isAtkTimerOn;
            this.isAtkSwitchOn = isAtkTimerOn;
            atkTimer = 2.0f;
        }
    }


    public int Attack()
    {
        Debug.Log("공격력 : " + atkPower);
        return atkPower;
    }

    private void UpdateTransformMesh()
    {
        //콜리전에 사용할 Mesh를 만든다.
        if (isAtkTimerOn && atkTimer > 0.0f)
        {
            if (isAtkSwitchOn)
            {
                if (weaponMesh == null) { Debug.LogError(weaponMesh); }
                else
                {
                    Vector3 atkStartPos = this.transform.position+(this.transform.forward * atkStartDist);
                    float[] tmpAngle = new float[] { obj.transform.rotation.y - (atkAngle / 2), obj.transform.rotation.y + (atkAngle / 2) };
                    weaponMesh.makeFanShape(tmpAngle, atkStartPos,atkRangeDist);
                    isAtkSwitchOn = false;
                }
            }
            else
            {

                weaponMesh.clearShape();
            }
            this.atkTimer -= Time.deltaTime+(atkSpeed/50);
        }
        else
        {
            isAtkTimerOn = false;
        }
    }
}
