using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrapTrigger : MonoBehaviour {
    [SerializeField] private string tagName = "Player";

    private EnemySpawnTrap[] spawnTraps = null;
    private bool isUsed = false;
	private void Start () {
        spawnTraps = this.GetComponentsInChildren<EnemySpawnTrap>();
        if (spawnTraps.Length == 0)
            Debug.LogError("spawnTraps Length is 0,,,");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!isUsed && other.CompareTag(tagName))
        {
            isUsed = true;
            StartCoroutine(coroutineSpawn());
        }
    }

    private IEnumerator coroutineSpawn()
    {
        int count = spawnTraps.Length;
        while (count > 0)
        {
            --count;
            spawnTraps[count].OnStart();
            yield return new WaitForSeconds(0.66f);
        }
        Destroy(this.gameObject, 1f);
    }
}
