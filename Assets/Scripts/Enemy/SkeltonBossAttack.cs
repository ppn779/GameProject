using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeltonBossAttack : MonoBehaviour
{
    private AtkMng atkMng = null;

    private void Start()
    {
        atkMng = this.gameObject.GetComponent<AtkMng>();

        //this.gameObject.GetComponentInChildren<AnimationEventReceiver>().attackHit = AttackHit;
    }

    public void LongDistanceAttack()
    {
        Debug.Log("Boss Atk");
        atkMng.Attack();
    }

    //public void AttackHit()
    //{
    //    if (atkMng == null) { Debug.LogError(atkMng); }
    //    else { atkMng.Attack(); }
    //}
}
