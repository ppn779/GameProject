using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStat {

    //private bool canBeAttacked;
    
    public override void Die()
    {
        base.Die();

        // effect / death animation

        Destroy(gameObject);
    }

    //public bool isPlayerNearby
    //{
    //    get
    //    {
    //        return canBeAttacked;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        canBeAttacked = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        canBeAttacked = false;
    //    }   
    //}
}
