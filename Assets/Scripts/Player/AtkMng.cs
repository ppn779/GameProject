using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtkMng : MonoBehaviour
{
    private GameObject player;
    private GameObject specialAtkGageBar = null;
    private WeaponMeshCtrl weaponMesh;
    private RectTransform specialAtkGageBarTr = null;
    private Image specialAtkGageBarImg = null;


    private const float MAX_GAGE = 200.0f;
    //색상 변화량, 최대치가 1이기 때문에 파워 최대치 만큼을 나눠서 설정
    //private const float COLOR_STEP = 1f/MAX_GAGE;

    private float atkPower = 0.0f;
    private float specialAtkGage = 0.0f;//필살기 게이지 수치
    private float atkTimer = 0.05f;//조절 중.
    private bool isAtkButtonOn = false;
    private bool canSpecialAtk = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        specialAtkGageBar = GameObject.FindGameObjectWithTag("SpecialAtkGageBar");
        weaponMesh = gameObject.GetComponentInChildren<WeaponMeshCtrl>();
        specialAtkGageBarTr = specialAtkGageBar.GetComponent<RectTransform>();
        specialAtkGageBarImg = specialAtkGageBar.GetComponent<Image>();
    }

    private void Update()
    {
        UpdateTransformMesh();
        SpecialGageBarCtrl();
    }

   
    public bool CanSpecialAtk
    {
        get
        {
            return canSpecialAtk;
        }
    }

    public void SpecialAtkGageBarUp()
    {
        Debug.Log(specialAtkGage);
        specialAtkGage += 10.0f;
    }

    public float SpecialAtk()
    {
        specialAtkGage = 0.0f;
        return 50.0f;
    }

    public float PlayerAtkPower
    {
        get
        {
            return atkPower;
        }
        set
        {
            atkPower = value;
        }
    }

    public void SearchAtkTarget(bool isClickAtk)//아이템의 공격력 값을 반환시킬 예정(float으로 바꿔도 무방)
                         //공격 애니메이션 bool값 여기서 바꿀 예정.
    {
        this.isAtkButtonOn = isClickAtk;
    }


    public float Attack()
    {
            Debug.Log("Attack");


            if (!this.canSpecialAtk)//게이지가 100이 되면 update 함수를 통해 자동으로 canSpecialAtk변수가 true로 바뀌고 필살기로 숫자를 떨어뜨리기 전까지 안 바뀜.
            {
                
                this.SpecialAtkGageBarUp();
            }
            return atkPower;
        
    }

    private void UpdateTransformMesh()
    {
        //콜리전에 사용할 Mesh를 만든다.
        if (isAtkButtonOn)
        {
            if (weaponMesh == null) { Debug.LogError(weaponMesh); }
            float[] tmpAngle = new float[] { player.transform.rotation.y - 30, player.transform.rotation.y + 30 };
            isAtkButtonOn = weaponMesh.makeFanShape(tmpAngle);
        }
        else
        {
            weaponMesh.clearShape();
        }
    }

    private void SpecialGageBarCtrl()
    {
        if (specialAtkGage >= MAX_GAGE)
        {
            canSpecialAtk = true;
            specialAtkGage = MAX_GAGE;
        }
        else
        {
            canSpecialAtk = false;
        }

        specialAtkGageBarTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, specialAtkGage);

        //색상변화
        Color newColor = specialAtkGageBarImg.color;
        //green,blue 빼서 red만 남게 만들기
        newColor.g -= 1;
        newColor.b -= 1;
        specialAtkGageBarImg.color = newColor;
    }
}
