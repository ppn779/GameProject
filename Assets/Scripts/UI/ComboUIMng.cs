using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIMng : MonoBehaviour
{
    //private Enemy[] Enemies = null;

    private Text ComboText = null;

    private int comboNum = 0;

    public const float OFFSET_Y=130.0f;                 // 플레이어와의 간격 Y
                                                         // Use this for initialization
    void Awake()
    {
        ComboText = GameObject.FindGameObjectWithTag("ComboText").GetComponent<Text>();
    }

    // Update is called once per frame

    public void FollowPlayerComboPrint(Vector3 _pos,int _comboNum)
    {
        // 월드(World)상에 존재하는 플레이어의 위치를
        // UI가 있는 스크린 좌표로 변환
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, _pos);
        // 간격 적용
        pos.y += OFFSET_Y;
        // 위치 갱신
        ComboText.transform.position = pos;
        ComboText.text = "Combo  "+_comboNum.ToString();
    }
}