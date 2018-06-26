using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPrint : MonoBehaviour {
    Text debugText = null;
    PlayerAtkMng playerAtkMng = null;
    PlayerStats playerStats = null;

	// Use this for initialization
	void Start () {
        debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
        if (debugText == null) { Debug.LogError("디버그의 디버그텍스트 Null"); }
        playerAtkMng = GetComponent<PlayerAtkMng>();
        if (playerAtkMng == null) { Debug.LogError("디버그의 플레이어 어택매니져 Null"); }
        playerStats = GetComponent<PlayerStats>();
        if (playerStats == null) { Debug.LogError("디버그의 플레이어 스탯 null"); }
	}

    private void Update()
    {
        debugText.text = "플레이어 공격력  : " + playerAtkMng.AtkPower + "\n" + "플레이어 공격속도: " + playerAtkMng.AtkSpeed + "\n"
            + "플레이어 전체체력 :  " + playerStats.maxHealth + "\n" + "플레이어 현재체력 : " + playerStats.currentHealth;



    }

}
