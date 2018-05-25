using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public enum MonsterState { idle, trace, attack, die };

    public MonsterState monsterState = MonsterState.idle;

    public float moveSpeed = 5f;
    public float lookRadius = 10f;
    public float rotSpeed = 10f;
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
    public float stayTime = 0f;

    public bool shotFired = false;
    public bool isDeath = false;

    public Vector3 startPos;
    public Vector3 targetPos;

    public static bool isPlayerAlive = true;

    private Transform monsterTr;
    private Transform target;

    private NavMeshAgent nvAgent;

    private Animator animator;

    private CharacterCombat combat;


    void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();

        combat = GetComponent<CharacterCombat>();
    }

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            Move();
        }
    }


    // 타겟 바라보기
    void LookAtPlayer()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);
    }

    //void EnemyAI()
    //{
    //    if (!isDeath)
    //    {
    //        Move();
    //    }
    //    else
    //    {
    //        if (Time.deltaTime > 3f)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    void Attack()
    {
        CharacterStat targetStats = target.GetComponent<CharacterStat>();

        if (targetStats != null)
        {
            combat.Attack(targetStats);
        }
    }

    void Move()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            nvAgent.SetDestination(target.position);

            if (distance <= nvAgent.stoppingDistance)
            {
                Attack();

                LookAtPlayer();
            }
        }

        else if (distance > lookRadius)
        {
            nvAgent.SetDestination(targetPos);
            Patrolling();
        }
    }

    private float MoveCheck()
    {
        float aniSpeed;
        aniSpeed = Vector3.Project(nvAgent.desiredVelocity, transform.forward).magnitude;

        return aniSpeed;
    }

    void Patrolling()
    {
        float speed = MoveCheck();

        if (speed <= 0f)
        {
            stayTime += Time.deltaTime;
            if (stayTime > 3f)
            {
                Vector3 rePos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                targetPos = startPos + rePos;
                nvAgent.SetDestination(targetPos);
                stayTime = 0f;
            }
        }
    }

    void Run()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}