using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement; //GetAxisRaw 처리 다음에 반환값을 담을 변수                 
    private Animator playerAnim;
    private PlayerAbilities playerAbilities;
    private Equipment equipment;
    private AtkMng atkMng;

    private bool isAlive = true;//죽으면 true

    private bool isClicked = true;//한번 공격하면 일정 시간 이후 공격 가능, 공격속도와 연관

    private int floorMask; //raycast Layer 정보를 담을 변수                    
    private float camRayLength = 100f; //raycast 거리 값 
    private float playerAtkPower = 0f;

    private float atkTimer = 0.0f;//누르고 있으면 무한공격이 되지 않도록 만드는 변수

    private void Start()
    {
        playerAnim = this.GetComponent<Animator>();
        playerAbilities = this.GetComponent<PlayerAbilities>();
        equipment = this.GetComponent<Equipment>();
        atkMng = this.GetComponent<AtkMng>();
        floorMask = LayerMask.GetMask("Floor");//"Floor"로 layer 위치값 등록
        //나중에 Update에서 갱신하도록 수정해야 함.
        atkTimer = 1.5f - (playerAbilities.AtkSpeed / 140);//공격속도 숫자가 커질수록 타이머 시간은 줄어듬.
    }

    private void Update()
    {
        
        if (this.isAlive)
        {
            this.AtkCtrl();

            float h = Input.GetAxisRaw("Horizontal");//가로 값
            float v = Input.GetAxisRaw("Vertical");//세로 값

            
         
            Move(h, v,playerAbilities.MovementSpeed);
            Turning();
            Animating(h, v);
        }
    }

    private void AtkCtrl()//아이템의 공격력 값을 반환시킬 예정(float으로 바꿔도 무방)
                          //공격 애니메이션 bool값 여기서 바꿀 예정.
    {
        if (isClicked)
        {
            if (Input.GetMouseButton(0))
            {
                if (atkMng == null) { Debug.LogError(atkMng); }
                else
                    atkMng.SearchAtkTarget(isClicked);
                isClicked = false;
            }
        }

        else
        {
            if (atkTimer >= 0.0f)
                this.atkTimer -= Time.deltaTime;
            else if (atkTimer < 0.0f)
            {
                isClicked = true;
                atkTimer = 1.5f - (playerAbilities.AtkSpeed / 140);//공격속도 숫자가 커질수록 타이머 시간은 줄어듬.
            }
        } 
    }

    private void Move(float h, float v,float speed)
    {
        //방향 값 담음.
        movement.Set(h, 0f, v);

        //정규화 처리
        movement = movement.normalized * speed * Time.deltaTime;

        //캐릭터 이동
        this.transform.position+=movement;
    }

    private void Turning()
    {
        //카메라에서 마우스 포지션으로 Ray 발사
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        RaycastHit floorHit;


        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //플레이어와 마우스의 거리 값 구함.
            Vector3 playerToMouse = floorHit.point - transform.position;

            //Y는 변함 없으므로 0 처리
            playerToMouse.y = 0f;

            //플레이어 위치에서 마우스 위치로의 방향 값 구하고 저장
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //저장한 방향 값으로 회전
            this.transform.rotation = newRotation;
        }
    }

    private void Animating(float h, float v)
    {
        //방향키 있으면 ||연산자에 의해 1이 반환되어서 true, 방향키 없으면 false 반환 됨.
        bool walking = h != 0f || v != 0f;

        //저장된 bool값 들어감.
        playerAnim.SetBool("IsWalking", walking);
    }

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }
}
