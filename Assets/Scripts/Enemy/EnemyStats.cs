using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStat
{
    [SerializeField] private List<string> listStrDeathSound = null;
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

    private void Equipment()
    {
        atkMng.IsEquippedWeapon = true;
        atkMng.Weapon = weapon;
    }

    public override void Die()
    {
        IsDead = true;
        if (nav)
        {
            nav.isStopped = true;
        }


        // Animation
        animator.SetTrigger("Death");

        // Drop Item
        this.GetComponent<DropTable>().GetRandomItem();

        if (listStrDeathSound.Capacity > 0)
        {
            int rand = Random.Range(0, listStrDeathSound.Capacity - 2);
            AudioMng.GetInstance().PlaySound(listStrDeathSound[rand], this.transform.position, 120f);
        }


        // Destroy
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 1);
    }
}
