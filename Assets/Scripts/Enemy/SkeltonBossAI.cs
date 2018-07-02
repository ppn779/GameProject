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
    private enum bossState { none = 0, waiting, longAtk };

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null) { Debug.LogError("target is null"); }

        myStats = this.gameObject.GetComponent<CharacterStat>();
        animator = this.gameObject.GetComponentInChildren<Animator>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        bossAttack = this.gameObject.GetComponent<SkeltonBossAttack>();
        StartCoroutine(Waiting());
    }

    void Update()
    {
        if ((!target) || (isDead)) { return; }

        myPos = this.transform.position;
        targetPos = target.position;
        distance = Vector3.Distance(myPos, targetPos);


        if (moveableRadius < distance)
        {
            LookAtPlayer();
            if (state == bossState.none)
            {
                // 패턴 2, 원거리 공격
                if (state == bossState.longAtk)
                {
                    bossAttack.LongDistanceAttack();
                }
            }

        }
    }

    private IEnumerator Waiting()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            state = bossState.longAtk;
            animator.Play("Attack", 0);
            state = bossState.none;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
    }
}
