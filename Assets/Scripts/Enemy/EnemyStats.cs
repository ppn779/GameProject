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

    private void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();
        atkMng = this.gameObject.GetComponent<AtkMng>();
        weapon = this.gameObject.GetComponentInChildren<Weapon>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();

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
            PlayerAtkMng playerAtkMng = other.GetComponentInParent<PlayerAtkMng>();
            Debug.Log("데미지 : " + playerAtkMng.AtkPower);
            objStat.TakeDamage(playerAtkMng.AtkPower);
        }
    }

    public override void Die()
    {
        base.Die();

        // effect
        nav.isStopped = true;
        animator.SetBool("isDeath", true);

        this.GetComponent<DropTable>().GetRandomItem();
        Destroy(gameObject, 3f);
    }
}
