using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript01 : MonoBehaviour
{
    public bool alive = true;
    public bool runAway = false;            // 도망
    public bool runAwayByHp = false;        // 도망에 체력 조건 사용
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
    public bool targetOn = true;

    private Vector3 targetPos;
    private Vector3 startPos;

    public bool playerHasBeenSeen = false;     // 플레이어 발견
    private bool enemyCanAttack = false;        // 공격 할 수 있는지
    private bool enemyIsAttacking = false;

    private float delayTime = 2f;
    private float deltaTime;

    private float lastShotFired;
    private float lostPlayerTimer;
    private bool targetIsOutOfSight;

    private Vector3 randomDirection;



    private bool waypointCountdown = false;
    private int waypointPatrol = 0;
    private bool pauseWaypointControl;

    private CharacterStat targetStats;
    private CharacterStat myStats;

    private Animator animator;
    private AtkMng atkMng;
    private NavMeshAgent nav;

    private string state = "idle";
    //private string runawayState = "runaway";

    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        targetStats = target.GetComponent<CharacterStat>();

        myStats = this.gameObject.GetComponent<CharacterStat>();
        atkMng = this.gameObject.GetComponent<AtkMng>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponentInChildren<Animator>();

        this.gameObject.GetComponentInChildren<AnimationEventReceiver>().attackHit = AttackHit;

        startPos = this.transform.position;
        nav.speed = walkSpeed;


        yield return null;
    }

    void Update()
    {
        if (alive)
        {
            AIFunctionality();
        }
    }

    void AIFunctionality()
    {
        if ((!target))
        {
            return;
        }

        targetPos = target.transform.position;

        float distance = Vector3.Distance(transform.position, targetPos);

        if (!runAway)
        {

            // 타겟 시야 반경 내
            if (TargetIsInSight())
            {
                // 타겟 따라가기
                animator.SetBool("isWalk", true);

                NavStart();

                SetNav(target.position);

                // 공격거리보다 멀면
                if (distance > attackRange)
                {
                    Debug.Log(" 3 ");
                    enemyCanAttack = false;
                    //MoveTowards(moveToward);
                }

                // 공격 거리 이내
                else if (distance < attackRange)
                {
                    Debug.Log(" 4 ");
                    animator.SetBool("isRun", false);
                    animator.SetBool("isWalk", false);
                    if (Time.time > lastShotFired + attackTime)
                    {
                        StartCoroutine(Attack());
                    }
                }

            }
            // 타겟 시야 반경 밖
            // 발견 했을 때 지속 추격
            else if ((playerHasBeenSeen) && (!targetIsOutOfSight))
            {
                Debug.Log(" 6 ");
                animator.SetBool("isWalk", true);
                lostPlayerTimer = Time.time + huntingTimer;

                StartCoroutine(HuntDownTarget());

            }
            // 발견 못했거나 moveableRadius가 0 또는 플레이어와의 거리가 moveableRadius보다 작으면
            else if (((!playerHasBeenSeen)) && ((moveableRadius == 0) || (distance < moveableRadius)))
            {
                Debug.Log(" 7 ");
                Debug.Log("WalkNewPath");
                WalkNewPath();
            }
            else if ((!playerHasBeenSeen) && (distance > moveableRadius))
            {
                Debug.Log(" 8 ");
                animator.SetBool("isWalk", false);
                animator.SetBool("isRun", false);

                if (useWaypoint)
                {
                    Patrol();
                }
                else
                {
                    SetNav(startPos);

                    if (nav.remainingDistance <= nav.stoppingDistance)
                    {
                        NavStop();
                    }
                }
            }
        }
        else if (runAway)
        {
            Debug.Log(" 10 ");

            NavStop();

            if (distance < runAwayDistance)
            {
                enemyCanAttack = false;
                nav.speed = runSpeed;
                RunAway();
            }
            else if (distance > runAwayDistance)
            {
                enemyCanAttack = false;
                nav.speed = walkSpeed;
                RunAway();
            }
        }

        // 체력 조건 사용
        if ((myStats.currentHealth <= 30) && (runAwayByHp))
        {
            enemyCanAttack = false;
            runAway = true;
        }
    }

    void LookAtPlayer()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
    }

    IEnumerator Attack()
    {
        if (target != null)
        {
            enemyCanAttack = true;//이거 없어도 되도록 만듬.

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

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if ((moveableRadius > 0) && (distance > moveableRadius))
        {
            return false;
        }

        targetOn = true;

        if ((visualRadius > 0) && (distance > visualRadius))
        {
            targetOn = false;
            return false;
        }

        if ((targetOn) && (distance < visualRadius))
        {
            Debug.Log("Target On");
            LookAtPlayer();
        }


        RaycastHit sight;

        if (Physics.Linecast(transform.position, target.transform.position, out sight))
        {
            if (!playerHasBeenSeen && sight.transform == target)
            {
                Debug.Log("Sight");
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
    IEnumerator HuntDownTarget()
    {
        targetIsOutOfSight = true;

        while (targetIsOutOfSight)
        {
            Debug.Log("Hunt");

            NavStart();
            SetNav(target.position);

            if (TargetIsInSight())
            {
                targetIsOutOfSight = false;

                break;
            }

            if (Time.time > lostPlayerTimer)
            {
                targetIsOutOfSight = false;
                playerHasBeenSeen = false;

                Debug.Log("Hunt OFF");
                break;
            }

            yield return null;
        }
    }
    // 순찰
    void Patrol()
    {
        animator.SetBool("isRun", false);
        animator.SetBool("isWalk", true);

        if (pauseWaypointControl)
        {
            return;
        }

        Vector3 destination = CurrentPath();


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
                PatrolNewPath();
            }
        }

    }

    IEnumerator WaypointPause()
    {
        yield return new WaitForSeconds(Random.Range(pauseMin, pauseMax));
        PatrolNewPath();
        pauseWaypointControl = false;
    }

    Vector3 CurrentPath()
    {
        return waypoints[waypointPatrol].position;
    }

    void PatrolNewPath()
    {
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

    // 새로운 길 찾기
    void WalkNewPath()
    {
        NavStart();

        if (state == "idle")
        {
            Vector3 randomPos = Random.insideUnitSphere * 10f;
            NavMeshHit navHit;
            NavMesh.SamplePosition(transform.position + randomPos, out navHit, 10f, NavMesh.AllAreas);
            SetNav(navHit.position);
            state = "walk";
        }

        if (state == "walk")
        {
            animator.SetBool("isWalk", true);
            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                animator.SetBool("isWalk", false);

                deltaTime += Time.deltaTime;

                if (deltaTime >= delayTime)
                {
                    state = "idle";

                    deltaTime = 0f;
                }
            }
        }
    }
    void SetNav(Vector3 target)
    {
        nav.SetDestination(target);
    }

    void NavStart()
    {
        nav.isStopped = false;
    }

    void NavStop()
    {
        nav.isStopped = true;
    }

    void RunAway()
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", true);

        Vector3 runAwayDir = (transform.position - targetPos).normalized;
       
        runAwayDir.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(runAwayDir), Time.deltaTime * rotationSpeed);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 direction = runAwayDir * nav.speed * Time.deltaTime;

        this.transform.position += direction;

        //if (runawayState == "runaway")
        //{
        //    Vector3 randomPos = Random.insideUnitSphere * 10f;

        //    NavMeshHit navHit;
        //    NavMesh.SamplePosition(target.position + randomPos, out navHit, 10f, NavMesh.AllAreas);
        //    SetNav(navHit.position);
        //    runawayState = "move";
        //}

        //if (runawayState == "move")
        //{
        //    if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
        //    {
        //        runawayState = "runaway";
        //    }
        //}
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

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + this.transform.forward);
    }

    public void AttackHit()
    {
        if (atkMng == null) { Debug.LogError(atkMng); }
        else
        {
            atkMng.Attack();
        }
        Debug.Log("ATTACKHIT");
    }
}