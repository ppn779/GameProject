using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCtrl : MonoBehaviour {
    [SerializeField]
    private float projectileSpeed;

    private void FixedUpdate()
    {
        this.transform.position += (this.transform.forward / 10) * projectileSpeed;
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
