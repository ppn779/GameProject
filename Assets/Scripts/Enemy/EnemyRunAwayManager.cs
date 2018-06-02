using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunAwayManager : MonoBehaviour
{

    public Transform[] enemies;
    public int enemiesLength;  
    // Use this for initialization
    void Start()
    {
        enemies = this.gameObject.GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies != null)
        {
            enemiesLength = GameObject.FindGameObjectsWithTag("Enemy").Length;
            //Debug.Log("Enemies Length = " + enemiesLength);
            if (enemiesLength <= 1)
            {
                Debug.Log("ENEMY RUNAWAY : " + enemies[1].gameObject.GetComponent<EnemyAIScript01>().runAway);
                enemies[1].gameObject.GetComponent<EnemyAIScript01>().runAway = true;
            }
        }
    }
}
