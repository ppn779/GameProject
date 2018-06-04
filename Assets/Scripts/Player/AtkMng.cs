using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMng : MonoBehaviour
{
    [SerializeField]
    private string objTagName;
    private GameObject obj;
    private WeaponMeshCtrl weaponMesh;

    private int atkPower;
    private float atkAngle;
    private float atkStartDist;
    private float atkRangeDist;
    private float atkSpeed;
    private float atkTimer;
    private bool isAtkSwitchOn = false;
    private bool isAtkTimerOn = false;
    private bool isEquippedWeapon = false;
    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag(objTagName);
        weaponMesh = gameObject.GetComponentInChildren<WeaponMeshCtrl>();
    }

    private void FixedUpdate()
    {
        UpdateTransformMesh();
    }

    public float AtkStartDist
    {
        get
        {
            return atkStartDist;
        }
        set
        {
            atkStartDist = value;
        }
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
        return atkPower;
    }

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
                    Vector3 atkStartPos = this.transform.position + (this.transform.forward * (atkStartDist));
                    float[] tmpAngle = new float[] { obj.transform.rotation.y - (atkAngle / 2), obj.transform.rotation.y + (atkAngle / 2) };
                    weaponMesh.makeFanShape(tmpAngle, atkStartPos, atkRangeDist);
                    isAtkSwitchOn = false;
                }
            }
            else
            {

                weaponMesh.clearShape();
            }
            this.atkTimer -= Time.deltaTime + (atkSpeed / 50);
        }
        else
        {
            isAtkTimerOn = false;
        }
    }
}
