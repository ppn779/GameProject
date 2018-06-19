using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints = null;

    public GameObject enemy = null;
    public GameObject target = null;

    public float spawnTime = 3f;

    public int enemyCount = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Spawn()
    {
        if(spawnPoints == null) { return; }
        if (enemyCount > 10) { return; }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        EnemyAIScript01 AI = enemy.GetComponent<EnemyAIScript01>();
        AI.SetTarget(target);

        enemyCount++;
    }
}
