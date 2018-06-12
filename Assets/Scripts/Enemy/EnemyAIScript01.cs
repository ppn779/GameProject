using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript01 : MonoBehaviour
{

    //public Rigidbody rigidbody;

    public bool runAway = false;            // 타겟과 거리 유지
    public bool runTo = false;              // 타겟 바라보기
    public float runAwayDistance = 5.0f;    // 도망 시작 거리

    public float speed = 0.0f;
    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;
    public float rotationSpeed = 1.0f;

    public float moveableRadius = 30.0f;    // 움직이는 범위, 값이 0이거나 설정값 내에서만 움직임
    public float visualRadius = 20.0f;      // 시야 범위

    public float attackRange = 2.0f;
    public float attackTime = 2.0f;

    public Transform[] waypoints;           // 웨이포인트, 경로 설정
    public bool useWaypoint = false;
    public bool reversePatrol = true;
    public bool pauseAtWaypoints = false;   // 참이면 패트롤 유닛은 도달 할 때마다 각 웨이포인트에서 잠시 멈춤.
    public float pauseMin = 1.0f;
    public float pauseMax = 3.0f;           // pauseAtWaypoints가 true 인 경우 이 시간의 최대치를 일시 정지

    public float huntingTimer = 5.0f;       // 추적 지속 시간

    public Transform target;
    public bool requireTarget = true;

    private Vector3 lastVisTargetPos;

    private bool playerHasBeenSeen = false;     // 플레이어 발견
    private bool enemyCanAttack = false;        // 공격 할 수 있는지
    private bool enemyIsAttacking = false;
    private bool isRun = false;

    private float lastShotFired;
    private float lostPlayerTimer;
    private bool targetIsOutOfSight;

    private Vector3 randomDirection;
    private float randomDirectionTimer = 0.0f;
    private bool walkInRandomDirection = false;

    private bool waypointCountdown = false;
    private int waypointPatrol = 0;
    private bool pauseWaypointControl;

    private CharacterStat targetStats;
    private CharacterStat myStats;

    private Animator animator;
    private AtkMng atkMng;
    private NavMeshAgent agent;


    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        //rigidbody = this.gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        targetStats = target.GetComponent<CharacterStat>();

        myStats = this.gameObject.GetComponent<CharacterStat>();
        atkMng = this.gameObject.GetComponent<AtkMng>();
        animator = this.gameObject.GetComponentInChildren<Animator>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        speed = agent.speed;

        this.gameObject.GetComponentInChildren<AnimationEventReceiver>().attackHit = AttackHit;

        yield return null;
    }

    void Update()
    {
        animator.SetBool("isRun", isRun);
        AIFunctionality();
    }

    void AIFunctionality()
    {
        if ((!target))
        {
            return;
        }

        lastVisTargetPos = target.position;

        Vector3 moveToward = lastVisTargetPos - transform.position;     // 추적
        Vector3 moveAway = transform.position - lastVisTargetPos;       // 도망

        float distance = Vector3.Distance(transform.position, target.position);

        // 타겟 시야 반경 내
        if (TargetIsInSight())
        {

            LookAtPlayer();

            if ((distance > attackRange) && (!runAway) && (!runTo))
            {
                enemyCanAttack = false;
                MoveTowards(moveToward);
            }

            else if ((myStats.currentHealth <= 30) && (!runAway))
            {
                runAway = true;
            }

            else if ((distance > runAwayDistance) && (runAway || runTo))
            {
                if (runAway)
                {
                    WalkNewPath();
                }
                else
                {
                    MoveTowards(moveToward);
                }
            }
            else if ((distance < runAwayDistance) && (runAway || runTo))
            {
                enemyCanAttack = false;

                walkInRandomDirection = false;

                if (runAway)
                {
                    MoveTowards(moveAway);
                }
                else
                {
                    MoveTowards(moveToward);
                }

            }

            if ((distance < attackRange) && (!runAway))
            {
                if (Time.time > lastShotFired + attackTime)
                {
                    StartCoroutine(Attack());
                    Debug.Log("Attack!!");
                }
                // }
            }
        }

        // 타겟 시야 반경 밖

        // 발견 했을 때 지속 추격
        else if ((playerHasBeenSeen) && (!targetIsOutOfSight))
        {
            lostPlayerTimer = Time.time + huntingTimer;
            StartCoroutine(HuntDownTarget(lastVisTargetPos));
        }
        // 발견 못했거나 moveableRadius가 0 또는 플레이어와의 거리가 moveableRadius보다 작으면
        else if (((!playerHasBeenSeen)) && ((moveableRadius == 0) || (distance < moveableRadius)))
        {
            WalkNewPath();
        }
        // useWaypoint = true 면 패트롤
        else if (useWaypoint)
        {
            Patrol();
        }
        else
        {
            isRun = false;
        }
    }

    void LookAtPlayer()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
    }

    IEnumerator Attack()
    {
        if (target != null)
        {
            enemyCanAttack = true;

            if (!enemyIsAttacking)
            {
                enemyIsAttacking = true;

                while (enemyIsAttacking)
                {
                    lastShotFired = Time.time;

                    if (targetStats != null)
                    {
                        animator.SetTrigger("attack");
                    }

                    yield return new WaitForSeconds(attackTime);

                    enemyIsAttacking = false;
                }
            }
        }
    }

    // 시야 반경 내에 있는지 확인
    bool TargetIsInSight()
    {
        if (target == null)
        {
            return false;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if ((moveableRadius > 0) && (distance > moveableRadius))
        {
            requireTarget = false;
        }

        if ((visualRadius > 0) && (distance > visualRadius))
        {
            requireTarget = false;
            return false;
        }
        else
        {
            requireTarget = true;
        }

        RaycastHit sight;

        if (Physics.Linecast(transform.position, target.position, out sight))
        {
            if (!playerHasBeenSeen && sight.transform == target)
            {
                playerHasBeenSeen = true;
            }
            return sight.transform == target;
        }
        else
        {
            return false;
        }
    }

    // 타겟 추적 지속
    IEnumerator HuntDownTarget(Vector3 position)
    {
        targetIsOutOfSight = true;

        while (targetIsOutOfSight)
        {
            Vector3 moveToward = position - transform.position;

            MoveTowards(moveToward);

            if (TargetIsInSight())
            {
                targetIsOutOfSight = false;
                break;
            }

            if (Time.time > lostPlayerTimer)
            {
                targetIsOutOfSight = false;
                playerHasBeenSeen = false;

                break;
            }

            yield return null;
        }
    }
    // 순찰
    void Patrol()
    {
        Debug.Log("Patrol");
        if (pauseWaypointControl)
        {
            return;
        }

        Vector3 destination = CurrentPath();
        Vector3 moveToward = destination - transform.position;
        Debug.Log("CurrentPath : " + CurrentPath());
        float distance = Vector3.Distance(transform.position, destination);

        if (distance <= 1.5f)
        {
            if (!pauseWaypointControl)
            {
                pauseWaypointControl = true;
                StartCoroutine(WaypointPause());
            }
            else
            {
                NewPath();
            }
        }
        MoveTowards(moveToward);
    }

    IEnumerator WaypointPause()
    {
        yield return new WaitForSeconds(Random.Range(pauseMin, pauseMax));
        NewPath();
        pauseWaypointControl = false;
    }

    Vector3 CurrentPath()
    {
        return waypoints[waypointPatrol].position;
    }

    void NewPath()
    {
        Debug.Log("NewPath");
        if (!waypointCountdown)
        {
            waypointPatrol++;

            if (waypointPatrol >= waypoints.GetLength(0))
            {
                if (reversePatrol)
                {
                    waypointCountdown = true;
                    waypointPatrol -= 2;
                }
                else
                {
                    waypointPatrol = 0;
                }
            }
        }
        else if (reversePatrol)
        {
            waypointPatrol--;
            if (waypointPatrol < 0)
            {
                waypointCountdown = false;
                waypointPatrol = 1;
            }
        }
    }

    void WalkNewPath()
    {
        //Debug.Log("WalkNewPath");

        if (!walkInRandomDirection)
        {
            walkInRandomDirection = true;

            if (!playerHasBeenSeen)
            {
                randomDirection = new Vector3(Random.Range(-0.15f, 0.15f), 0, Random.Range(-0.15f, 0.15f));
            }
            else
            {
                randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            }

            randomDirectionTimer = Time.time;
        }
        else if (walkInRandomDirection)
        {
            MoveTowards(randomDirection);
        }

        if ((Time.time - randomDirectionTimer) > 5)
        {
            walkInRandomDirection = false;
        }
    }

    void MoveTowards(Vector3 direction)
    {
        direction.y = 0;

        isRun = true;

        agent.speed = walkSpeed;

        if (runAway)
        {
            agent.speed = runSpeed;
        }

        float speed = agent.speed;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        direction = forward * speed * Time.deltaTime;

        this.transform.position += direction;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, runAwayDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visualRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveableRadius);
    }

    public void AttackHit()
    {
        if (atkMng == null) { Debug.LogError(atkMng); }
        else
        {
            atkMng.AtkMngOn(enemyCanAttack);
        }
        Debug.Log("ATTACKHIT");
    }
}
