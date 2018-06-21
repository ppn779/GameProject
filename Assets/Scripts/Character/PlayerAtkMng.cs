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
        debugWeaponMesh = GameObject.Find("DebugWeaponMesh").GetComponent<DebugWeaponMeshCtrl>();//디버그용 지워야 하는 코드.
        equipment = GetComponent<Equipment>();
        if (equipment == null) { gameObject.AddComponent<Equipment>(); }
    }

    private void Update()
    {
        if (isAtkTimerOn && waitingTimeForAtk > time)
        {
            time += Time.deltaTime;
        }

        else
        {
            isAtkTimerOn = false;
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
            
          
            if (equippedWeapon.isWeaponTypeMelee)
            {
                // 현재 낀 무기가 근접 무기일경우
                SetForMeleeAtk();
            }
            isAtkTimerOn = true;
            waitingTimeForAtk = 3.0f - atkSpeed;
            if (waitingTimeForAtk <= 0.1f) { waitingTimeForAtk = 0.1f; }
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
        debugWeaponMesh.makeFanShape(tmpAngle, objTr, equippedWeapon.AtkRangeDist, equippedWeapon.AtkStartDist);//디버그용
        debugWeaponMesh.gameObject.SetActive(false);
        debugWeaponMesh.gameObject.SetActive(true);//디버그용
    }

    private void WeaponAttack()
    {
        Debug.Log("웨폰어택 실행");
        time = 0.0f;
        attackSwitchOn = true;
        if (equippedWeapon.isWeaponTypeMelee)
        {
            StartCoroutine(MeshActivation());
        }
        else
        {
            equippedWeapon.Attack(this.transform,atkPower);
        }
    }

    private void SetForMeleeAtk()
    {
        //while (waitingTimeForAtk > time)
        //{
        //    time += Time.deltaTime;
        //    //콜리전에 사용할 Mesh를 만든다.
        //if (weaponMeshCtrl != null)
        //{
        if (!isReady)
        {
            Debug.Log("메쉬 만듬");
            weaponMeshCtrl.AtkPow = this.atkPower;
            //Debug.Log("Start Pos   : " +atkStartPos);
            if (weaponMeshCtrl == null) { Debug.LogError("웨폰메쉬컨트롤 Null"); }
            float[] tmpAngle = new float[] { objTr.rotation.y - (equippedWeapon.WeaponAngle / 2), objTr.rotation.y + (equippedWeapon.WeaponAngle / 2) };
            weaponMeshCtrl.gameObject.SetActive(false);
            weaponMeshCtrl.makeFanShape(tmpAngle, objTr, equippedWeapon.AtkRangeDist, equippedWeapon.AtkStartDist);
           
            isReady = true;
        }
        //else
        //{
        //    weaponMeshCtrl.clearShape();
        //}
        //}
        //}
    }

    private IEnumerator MeshActivation()
    {
        while (waitingTimeForAtk > time)
        {
            time += Time.deltaTime;
            {
                if (attackSwitchOn)
                {
                    weaponMeshCtrl.transform.position = this.transform.position;
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
