using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGageBarMng : MonoBehaviour
{
    private GameObject lifeGageBar = null;
    private PlayerStats playerStats = null;
    //private GameObject[] enemies = null;
    private RectTransform lifeGageBarTr = null;
    private Image lifeGageBarImg = null;

    private const float MAX_GAGE = 300.0f;
    //색상 변화량, 최대치가 1이기 때문에 파워 최대치 만큼을 나눠서 설정
    private const float COLOR_STEP = 1f / MAX_GAGE;

    private float lifeGage;//콤보 게이지 수치
    private float width;

    //private bool canSpecialAtk = false;

    // Use this for initialization
    void Awake()
    {
        lifeGageBar = GameObject.FindGameObjectWithTag("LifeGageBar");
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        lifeGageBarTr = lifeGageBar.GetComponent<RectTransform>();
        lifeGageBarImg = lifeGageBar.GetComponent<Image>();
        width = lifeGageBarTr.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        lifeGage = ((float)playerStats.currentHealth/(float)playerStats.maxHealth)*width;

        LifeGageBarCtrl();

    }

    //public bool CanSpecialAtk
    //{
    //    get
    //    {
    //        return canSpecialAtk;
    //    }
    //}


    //public void SpecialAtkGageBarUp()
    //{
    //    Debug.Log(specialAtkGage);
    //    specialAtkGage += 10.0f;
    //}

    //public float SpecialAtk()
    //{
    //    specialAtkGage = 0.0f;
    //    return 50.0f;
    //}

    private void LifeGageBarCtrl()
    {
        //if (specialAtkGage >= MAX_GAGE)
        //{
        //    canSpecialAtk = true;
        //    specialAtkGage = MAX_GAGE;
        //}
        //else
        //{
        //    canSpecialAtk = false;
        //}

        lifeGageBarTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lifeGage);

        //색상변화
        Color newColor = lifeGageBarImg.color;
        //green,blue 빼서 red만 남게 만들기
        newColor.g -= 1;
        newColor.b -= 1;
        lifeGageBarImg.color = newColor;
    }
}
