using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterStat))]
public class Enemy : Interactable {
    
    CharacterStat myStats;

	// Use this for initialization
	void Start () {
    
        myStats = GetComponent<CharacterStat>();

	}

    //public override void Interact()
    //{
    //    base.Interact();
    //    //CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();
    //    //if(playerCombat != null)
    //    //{
    //    //    playerCombat.Attack(myStats); 
    //    //}
    //}  

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }
}
