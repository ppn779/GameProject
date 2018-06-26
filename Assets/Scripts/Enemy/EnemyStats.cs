using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : CharacterStat
{
    [SerializeField]
    private string opponentObjAtkTagName = null;
    private Animator animator = null;
    private AtkMng atkMng = null;
    private Weapon weapon = null;
    private NavMeshAgent nav = null;
    private EnemyAIScript01 ai = null;

    private void Start()
    {
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
        if (!IsDead)
        {
            if (opponentObjAtkTagName == null) { Debug.LogError("WeaponTag Name is null"); }

            if (other.tag == opponentObjAtkTagName)
            {
                CharacterStat objStat = this.gameObject.GetComponent<CharacterStat>();
                WeaponDamage weaponDamage = other.GetComponent<WeaponDamage>();

                if(objStat.TakeDamage(weaponDamage.AtkPow)) animator.SetTrigger("damage");

                ComboSystemMng.GetInstance().AddCombo(1);
            }
        }
    }

    public override void Die()
    {
        base.Die();

        Debug.Log("Die ! ");
        // effect
        IsDead = true;
        nav.isStopped = true;
        animator.SetTrigger("Death");

        this.GetComponent<DropTable>().GetRandomItem();

        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 1);
    }
}
