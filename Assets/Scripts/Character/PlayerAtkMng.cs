using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkMng : MonoBehaviour
{

    private Transform objTr=null;
    private Animator animator = null;
    private WeaponMeshCtrl weaponMeshCtrl = null;
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
        equipment = GetComponent<Equipment>();
        if (equipment == null) { gameObject.AddComponent<Equipment>(); }
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

    private void SetForMeleeAtk()
    {
        weaponMeshCtrl.damage = this.atkPower;
        //Debug.Log("Start Pos   : " +atkStartPos);
        if (weaponMeshCtrl == null) { Debug.LogError("웨폰메쉬컨트롤 Null"); }
        float[] tmpAngle = new float[] { objTr.rotation.y - (equippedWeapon.WeaponAngle / 2), objTr.rotation.y + (equippedWeapon.WeaponAngle / 2) };

        weaponMeshCtrl.makeFanShape(tmpAngle, objTr, equippedWeapon.AtkRangeDist);
        weaponMeshCtrl.gameObject.SetActive(false);

        if (equippedWeapon != null)
            weaponMeshCtrl.WeaponGameObject = equippedWeapon;
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
