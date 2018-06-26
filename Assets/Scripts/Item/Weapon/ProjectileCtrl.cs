using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCtrl : Weapon {
    [SerializeField]
    private float projectileSpeed;
    private Vector3 direction;

    private void Awake()
    {
        direction = this.transform.forward;
    }

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(direction * (projectileSpeed * 10));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Weapon")
        {
            Destroy(this.gameObject);
        }
    }
}
