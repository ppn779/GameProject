using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : CharacterStat
{
    [SerializeField]
    private string opponentObjAtkTagName = null;
    private static bool isAttackedByWeapon = false;
    private Transform tr = null;
    private Animator animator = null;
    private AtkMng atkMng = null;
    private Weapon weapon = null;
    private NavMeshAgent nav = null;
    private EnemyAIScript01 ai = null;

    private void Start()
    {
        tr = this.transform;
        animator = this.gameObject.GetComponentInChildren<Animator>();
        atkMng = this.gameObject.GetComponent<AtkMng>();
        weapon = this.gameObject.GetComponentInChildren<Weapon>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        ai = this.gameObject.GetComponent<EnemyAIScript01>();

        Equipment();
    }

    void Equipment()
    {
        atkMng.IsEquippedWeapon = true;
        atkMng.Weapon = weapon;

        atkMng.AtkPower += weapon.damage;
        atkMng.AtkSpeed += weapon.attackSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (opponentObjAtkTagName == null) { Debug.LogError("WeaponTag Name is null"); }
        if (other.tag == opponentObjAtkTagName)
        {

        
            CharacterStat objStat = this.gameObject.GetComponent<CharacterStat>();
            WeaponMeshCtrl meshCtrl = other.GetComponent<WeaponMeshCtrl>();
            Weapon weapon = null;
            if (meshCtrl)
                weapon = meshCtrl.WeaponGameObject;
            else
                weapon = other.GetComponent<Weapon>();
            
            //Debug.Log("데미지 : " + weapon.damage);
            objStat.TakeDamage(weapon.damage);

            Vector3 newPos = tr.position;
            newPos.y += 1f;
            Instantiate(ParticleMng.GetInstance().EffectBulletImpactWood(), newPos , tr.rotation);
            Instantiate(ParticleMng.GetInstance().EffectBulletImpactMetal(), newPos, tr.rotation);
            if (!isAttackedByWeapon)
            {
                isAttackedByWeapon = true;
                weapon.SubtractDurability(weapon.durabilityReduce);
            }
            ComboSystemMng.GetInstance().AddCombo(1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isAttackedByWeapon)
            isAttackedByWeapon = false;
    }

    public override void Die()
    {
        base.Die();
        // effect
        IsDead = true;
        nav.isStopped = true;
        animator.SetTrigger("Death");

        this.GetComponent<DropTable>().GetRandomItem();

        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 1);
    }
}
