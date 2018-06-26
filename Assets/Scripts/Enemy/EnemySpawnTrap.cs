using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrap : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab = null;
    private Transform tr = null;
    private bool isUsed = false;

	private void Start () {
        if (!enemyPrefab)
            Debug.LogError("enemyPrefab is NULL,, This need Enemy GameObject");
        tr = this.transform;
	}
	
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (isUsed)
            return;
        if (other.CompareTag("Player"))
        {
            isUsed = true;
            //Debug.Log(other);
            StartCoroutine(coroutineSpawn());
        }
    }

    private IEnumerator coroutineSpawn()
    {
        int count = 8;
        while (count > 0)
        {
            Vector3 newPos = tr.position;
            
            newPos.x += 3f * Mathf.Cos(count * 45f * Mathf.Deg2Rad);
            newPos.z += 3f * Mathf.Sin(count * 45f * Mathf.Deg2Rad);
            Debug.Log(newPos);
            Instantiate(enemyPrefab, newPos, tr.rotation);
            --count;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
