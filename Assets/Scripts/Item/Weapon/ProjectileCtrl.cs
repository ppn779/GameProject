using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCtrl : MonoBehaviour {
    [SerializeField]
    private float speed;
    private Vector3 direction;

    public void Fire(Vector3 direction)
    {
        this.direction = direction;
        StartCoroutine(FireProjectile());
    }

    IEnumerator FireProjectile()
    {
        while(true)
        {
            this.transform.position += (direction/10)*speed;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
