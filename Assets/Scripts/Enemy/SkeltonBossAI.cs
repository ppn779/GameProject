using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeltonBossAI : MonoBehaviour
{
    public bool isDead = false;

    public float moveableRadius = 30.0f;
    public float attackRange = 3.0f;
    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;
    public float rotationSpeed = 2.0f;

    private Transform target = null;
    private CharacterStat myStats = null;
    private Animator animator = null;
    private NavMeshAgent nav = null;
    private SkeltonBossAttack bossAttack = null;

    private Vector3 startPos;
    private Vector3 myPos;
    private Vector3 targetPos;

    private float distance;

    private bossState state = 0;
    private enum bossState { waiting = 0, move, meleeAtk, longAtk };

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null) { Debug.LogError("target is null"); }

        myStats = this.gameObject.GetComponent<CharacterStat>();
        animator = this.gameObject.GetComponentInChildren<Animator>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        bossAttack = this.gameObject.GetComponent<SkeltonBossAttack>();

    }

    void Update()
    {
        if ((!target) || (isDead)) { return; }

        myPos = this.transform.position;
        targetPos = target.position;
        distance = Vector3.Distance(myPos, targetPos);

        // 패턴 1, 대기
        if (state == bossState.waiting)
        {
            StartCoroutine(Waiting());
        }

        // 패턴 2, 이동
        if (state == bossState.move)
        {
            StartCoroutine(Move());
        }

        // 패턴 3, 근접 공격
        if (state == bossState.meleeAtk)
        {
            bossAttack.MeleeAttack();
        }

        // 패턴 4, 원거리 공격
        if (state == bossState.longAtk)
        {
            bossAttack.LongDistanceAttack();
        }

        if (myStats.currentHealth <= 0)
        {

        }
    }

    private IEnumerator Waiting()
    {
        animator.Play("Idle", 0);

        yield return new WaitForSeconds(5f);

        if (distance > attackRange)
        {
            state = bossState.waiting;
        }
    }

    private IEnumerator Move()
    {
        animator.Play("Walk", 0);

        SetNav(targetPos);

        yield return new WaitForSeconds(5f);

        if (distance < attackRange)
        {
            state = bossState.meleeAtk;
        }
    }

    private void SetNav(Vector3 target)
    {
        nav.SetDestination(target);
    }

    private void LookAtPlayer()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
    }
}
