using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkMng : MonoBehaviour
{
    private Transform objTr=null;
    private Animator animator = null;
    private WeaponMeshCtrl weaponMeshCtrl = null;
    private DebugWeaponMeshCtrl debugWeaponMesh = null;//디버그용
    private Weapon equippedWeapon = null;
    private Equipment equipment = null;
    private float atkPower = 0.0f;
    private float atkSpeed = 0.0f;
    //private float atkStartDist = 0.0f;
    //private float attackRange = 0.0f;
    //private float weaponAngle = 0.0f;
    private float waitingTimeForAtk = 0.0f;
    private float time = 0.0f;
    private bool isAtkTimerOn = false;
    private bool isEquippedWeapon = false;
    private bool attackSwitchOn = false;
    private bool isReady = false;

    private void Start()
    {
        objTr = this.transform;
        animator = GetComponent<Animator>();
        weaponMeshCtrl = GameObject.FindGameObjectWithTag("PlayerWeaponMesh").GetComponent<WeaponMeshCtrl>();
        if (weaponMeshCtrl == null) { Debug.LogError("어택매니져 웨폰메쉬 Null"); }
        //debugWeaponMesh = GameObject.Find("DebugWeaponMesh").GetComponent<DebugWeaponMeshCtrl>();//디버그용 지워야 하는 코드.
        equipment = GetComponent<Equipment>();
        if (equipment == null) { gameObject.AddComponent<Equipment>(); }

        weaponMeshCtrl.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isAtkTimerOn && waitingTimeForAtk > time)
        {
            //Debug.Log("웨이팅타임" + waitingTimeForAtk);
            //Debug.Log("시간" + time);
            time += Time.deltaTime;
        }
        else
        {
            isAtkTimerOn = false;
        }

        if (equippedWeapon !=  null)//디버그용 조건문. 다 지워야 함.
        {
            //debugWeaponMesh.transform.position = this.transform.position + (this.transform.forward * equippedWeapon.AtkStartDist);
            //debugWeaponMesh.transform.rotation = this.transform.rotation;
        }
    }

    public bool IsReady
    {
        set
        {
            isReady = value;
        }
    }

    public Weapon EquippedWeapon
    {
        set
        {
            equippedWeapon = value;
        }
    }

    public void Attack()
    {
        if (!isAtkTimerOn && isEquippedWeapon)
        {
                   
            if (!isReady)
            {
                if (equippedWeapon.isWeaponTypeMelee)
                {
                    // 현재 낀 무기가 근접 무기일경우
                    SetForMeleeAtk();
                }

                waitingTimeForAtk = 3.0f - atkSpeed;
                if (waitingTimeForAtk <= 0.8f) { waitingTimeForAtk = 0.8f; }
                isReady = true;
            }
            isAtkTimerOn = true;

            switch (Random.Range(0, 2))
            {
                case 0:
                    AudioMng.GetInstance().PlaySound("AttackSound_1", objTr.position, 100f);
                    break;
                case 1:
                    AudioMng.GetInstance().PlaySound("AttackSound_2", objTr.position, 100f);
                    break;
                case 2:
                    AudioMng.GetInstance().PlaySound("AttackSound_3", objTr.position, 100f);
                    break;
            }
            time = 0.0f;
            animator.SetTrigger("Attack");
        }
    }

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

    public void MakeDebugWeaponMesh()//삭제해야 함.
    {
        float[] tmpAngle = new float[] { objTr.rotation.y - (equippedWeapon.WeaponAngle / 2), objTr.rotation.y + (equippedWeapon.WeaponAngle / 2) };
        //debugWeaponMesh.makeFanShape(tmpAngle, objTr, equippedWeapon.AtkRangeDist);//디버그용
        //debugWeaponMesh.gameObject.SetActive(false);
        //debugWeaponMesh.gameObject.SetActive(true);//디버그용
    }

    private void WeaponAttack()
    {
        attackSwitchOn = true;
        if (equippedWeapon != null)
        {
            if (equippedWeapon.isWeaponTypeMelee)
            {
                StartCoroutine(MeshActivation());
            }
            else
            {
                equippedWeapon.Attack(this.transform, atkPower);
            }
        }
    }

    private void SetForMeleeAtk()
    {
        weaponMeshCtrl.damage = this.atkPower;
        //Debug.Log("Start Pos   : " +atkStartPos);
        if (weaponMeshCtrl == null) { Debug.LogError("웨폰메쉬컨트롤 Null"); }
        float[] tmpAngle = new float[] { objTr.rotation.y - (equippedWeapon.WeaponAngle / 2), objTr.rotation.y + (equippedWeapon.WeaponAngle / 2) };

        weaponMeshCtrl.makeFanShape(tmpAngle, objTr, equippedWeapon.AtkRangeDist);
        if (equippedWeapon != null)
            weaponMeshCtrl.WeaponGameObject = equippedWeapon;
    }

    private IEnumerator MeshActivation()
    {
        while (waitingTimeForAtk > time)
        {
            {
                if (attackSwitchOn)
                {
                    weaponMeshCtrl.transform.position = this.transform.position+ (this.transform.forward * equippedWeapon.AtkStartDist);
                    weaponMeshCtrl.transform.rotation = this.transform.rotation;
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
