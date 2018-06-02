using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAtkMng : MonoBehaviour {
    private GameObject specialAtkGageBar = null;
    private RectTransform specialAtkGageBarTr = null;
    private Image specialAtkGageBarImg = null;

    private const float MAX_GAGE = 200.0f;
    //색상 변화량, 최대치가 1이기 때문에 파워 최대치 만큼을 나눠서 설정
    private const float COLOR_STEP = 1f/MAX_GAGE;

    private float specialAtkGage = 0.0f;//필살기 게이지 수치


    private bool canSpecialAtk = false;

    // Use this for initialization
    void Start () {

        specialAtkGageBar = GameObject.FindGameObjectWithTag("SpecialAtkGageBar");
        specialAtkGageBarTr = specialAtkGageBar.GetComponent<RectTransform>();
        specialAtkGageBarImg = specialAtkGageBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {

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
