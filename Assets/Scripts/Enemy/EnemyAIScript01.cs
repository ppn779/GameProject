using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript01 : MonoBehaviour
{
    public bool on = true;                  // AI active?

    public bool runAway = false;            // 타겟과 거리 유지
    public bool runTo = false;              // 타겟 바라보기
    public float runAwayDistance = 5.0f;   // 도망 시작 거리
    public float runBufferDistance = 10.0f;
    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;
    public float randomSpeed = 3.0f;
    public float rotationSpeed = 1.0f;

    public float moveableRadius = 50.0f;   // 타겟이 너무 멀리 떨어져 있으면 AI가 자동으로 종료
    public float visualRadius = 20.0f;

    public float attackRange = 2.0f;
    public float attackTime = 0.5f;

    public bool useWaypoint = false;
    public bool reversePatrol = true;

    public Transform[] waypoints;           // 웨이포인트, 경로 설정
    public bool pauseAtWaypoints = false;   // 참이면 패트롤 유닛은 도달 할 때마다 각 웨이포인트에서 잠시 멈춤.
    public float pauseMin = 1.0f;
    public float pauseMax = 3.0f;           // pauseAtWaypoints가 true 인 경우 이 시간의 최대치를 일시 정지
    public float huntingTimer = 5.0f;


    public Transform target;
    public bool requireTarget = true;


    //private

    private bool initialGo = false;
    private bool go = true;
    private Vector3 lastVisTargetPos;

    CharacterController characterController;

    private bool playerHasBeenSeen = false;
    private bool enemyCanAttack = false;        // 공격 범위 내에 있는지 확인
    private bool enemyIsAttacking = false;
    private bool executeBufferState = false;    // 도망 속도 제어 변수
    private float lastShotFired;
    private float lostPlayerTimer;
    private bool targetIsOutOfSight;

    private Vector3 randomDirection;
    private float randomDirectionTimer;
    private bool walkInRandomDirection = false;

    private bool waypointCountdown = false;
    private bool monitorRunTo = false;
    private int waypointPatrol = 0;
    private bool pauseWaypointControl;

    private bool smoothAttackRangeBuffer = false;

    private NavMeshAgent nvAgent;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        initialGo = true;

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        //float range = 2.0f;
        //Vector3 forward = Vector3.forward;

        //if (Physics.Raycast(transform.position, forward, range))
        //{
        //    transform.Rotate(0, 90, 0);
        //    if (Physics.Raycast(transform.position, forward, range))
        //    {
        //        transform.Rotate(0, 90, 0);
        //        if (Physics.Raycast(transform.position, forward, range))
        //        {
        //            characterController.Move(forward * 20 * Time.deltaTime);
        //        }

        //    }
        //    else
        //    {
        //        characterController.Move(forward * 20 * Time.deltaTime);
        //    }
        //}

        if (!on || !initialGo)
        {
            return;
        }
        else
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

        lastVisTargetPos = target.position;
        Vector3 moveToward = lastVisTargetPos - transform.position;     // 추적
        Vector3 moveAway = transform.position - lastVisTargetPos;       // 도망

        float distance = Vector3.Distance(transform.position, target.position);
        Debug.Log("RequireTarget : " + requireTarget);
        if (!requireTarget)
        {
            Patrol();
        }
        else if (TargetIsInSight())
        {
            if (!go)
            {
                return;
            }

            if ((distance > attackRange) && (!runAway) && (!runTo))
            {
                enemyCanAttack = false;
                MoveTowards(moveToward);
            }
            else if ((smoothAttackRangeBuffer) && (distance > attackRange + 5.0f))
            {
                WalkNewPath();
            }
            else if ((runAway || runTo) && (distance > runAwayDistance) && (!executeBufferState))
            {
                if (monitorRunTo)
                {
                    monitorRunTo = false;
                }

                if (runAway)
                {
                    WalkNewPath();
                }
                else
                {
                    MoveTowards(moveToward);
                }
            }
            else if ((runAway || runTo) && (distance < runAwayDistance) && (!executeBufferState))
            {
                enemyCanAttack = false;
                if (!monitorRunTo)
                {
                    executeBufferState = true;
                }

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
            else if (executeBufferState && ((runAway) && (distance < runBufferDistance)) || ((runTo) && (distance > runBufferDistance)))
            {
                if (runAway)
                {
                    MoveTowards(moveAway);
                }
                else
                {
                    MoveTowards(moveToward);
                }
            }
            else if (executeBufferState && ((runAway) && (distance > runBufferDistance)) || ((runTo) && (distance < runBufferDistance)))
            {
                monitorRunTo = true;
                executeBufferState = false;
            }

            if ((distance < attackRange) || (!runAway && !runTo) && (distance < runAwayDistance))
            {
                if (runAway)
                {
                    smoothAttackRangeBuffer = true;
                }

                LookAtPlayer();

                if (Time.time > lastShotFired + attackTime)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        else if ((playerHasBeenSeen) && (!targetIsOutOfSight) && (go))
        {
            lostPlayerTimer = Time.time + huntingTimer;
            StartCoroutine(HuntDownTarget(lastVisTargetPos));
        }
        else if (useWaypoint)
        {
            Patrol();
        }
        else if (((!playerHasBeenSeen) && (go)) && ((moveableRadius == 0) || (distance < moveableRadius)))
        {
            WalkNewPath();
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
        enemyCanAttack = true;

        if (!enemyIsAttacking)
        {
            enemyIsAttacking = true;

            while (enemyIsAttacking)
            {
                lastShotFired = Time.time;

                // 공격 변수 구현

                yield return new WaitForSeconds(attackTime);
            }
        }
    }

    bool TargetIsInSight()
    {
        if ((moveableRadius > 0) && (Vector3.Distance(transform.position, target.position) > moveableRadius))
        {
            go = false;
        }
        else
        {
            go = true;
        }
        // 시야 반경 내에 있는지 확인
        if ((visualRadius > 0) && (Vector3.Distance(transform.position, target.position) > visualRadius))
        {
            return false;
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

    // 타겟 추적
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
        Debug.Log("WalkNewPath");
        //RaycastHit hit;
        //float range = Mathf.Infinity;

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

        if ((Time.time - randomDirectionTimer) > 2)
        {
            walkInRandomDirection = false;
        }
    }

    void MoveTowards(Vector3 direction)
    {
        direction.y = 0;

        float speed = walkSpeed;

        if (walkInRandomDirection)
        {
            speed = randomSpeed;
        }

        if (executeBufferState)
        {
            speed = runSpeed;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float speedModifier = Vector3.Dot(forward, direction.normalized);
        speedModifier = Mathf.Clamp01(speedModifier);

        direction = forward * speed * speedModifier;

        characterController.Move(direction * Time.deltaTime);
    }
}
