using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCtrl : MonoBehaviour {
    [SerializeField]
    private float projectileSpeed;
    private float damage;
    private Vector3 direction;

    private void Awake()
    {
        direction = this.transform.forward;
    }

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(direction * (projectileSpeed * 10));
    }

    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
