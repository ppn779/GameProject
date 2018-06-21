using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystemMng : MonoBehaviour
{
    GameObject playerObj = null;
    EnemyStats[] enemies = null;
    WeaponMeshCtrl playerWeaponMesh = null;
    ComboUIMng comboUIMng = null;

    private int comboCount = 0;
    private bool resetComboCountTimer = false;
    private float countTimer = 0.0f;
    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) { Debug.LogError("콤보시스템 매니져의 플레이어 오브젝트가 Null"); }
        enemies = GameObject.FindObjectsOfType<EnemyStats>();
        if (enemies == null) { Debug.LogError("콤보시스템 매니져의 에너미들이 NUll"); }
        playerWeaponMesh = playerObj.GetComponentInChildren<WeaponMeshCtrl>();
        if (playerWeaponMesh == null) { Debug.LogError("콤보시스템 매니져의 플레이어 웨폰 메쉬 Null"); }
        comboUIMng = GetComponent<ComboUIMng>();
        if (comboUIMng == null) { Debug.LogError("콤보유아이매니져 Null"); }
    }

    private void Update()
    {
        if (!resetComboCountTimer && countTimer > 0)
        {
            countTimer -= Time.deltaTime;
        }
        else if (!resetComboCountTimer && countTimer <= 0)
        {
            comboCount = 0;
            countTimer = 2.5f;
            resetComboCountTimer = false;
        }
        comboUIMng.FollowPlayerComboPrint(playerObj.transform.position,comboCount);
    }
}
