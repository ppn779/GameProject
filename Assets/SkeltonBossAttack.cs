using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeltonBossAttack : MonoBehaviour
{

    public GameObject TheEnemy;
    public int AttackTrigger;
    public int DealingDamage;

    void Update()
    {
        if (AttackTrigger == 0)
        {
            TheEnemy.GetComponent<Animation>().Play("Walk");
        }
        if (AttackTrigger == 1)
        {
            if (DealingDamage == 0)
            {
                TheEnemy.GetComponent<Animation>().Play("Attack");
                StartCoroutine(TakingDamage());
            }
        }
    }


    IEnumerator TakingDamage()
    {
        DealingDamage = 2;
        yield return new WaitForSeconds(0.5f);

    }
}
