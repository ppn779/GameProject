﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackProcess : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerController playerCtrl;

    private Vector3 direction;

    private bool isKnockBackOn=false;

    [SerializeField]
    private float knockBackTime;
    [SerializeField]
    private float forcePow;
    private float time;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        if (rb == null) { Debug.LogError(rb);
        }
        playerCtrl=this.GetComponent<PlayerController>();
        time = knockBackTime;
    }

    private void FixedUpdate()
    {
        if (this.isKnockBackOn && time > 0)
        {
            KnockBack();
            time -= Time.deltaTime;
            playerCtrl.IsInputSwitchOn=false;
        }
        else if (this.isKnockBackOn && time <= 0)
        {
            isKnockBackOn = false;
            time=knockBackTime;
            playerCtrl.IsInputSwitchOn = true;
        }
    }

    private void KnockBack()
    {
        rb.AddForce(direction * forcePow);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WeaponMesh")
        {
            //Debug.Log(other.transform.forward);
            direction = other.transform.forward;
            isKnockBackOn = true;
        }
    }
}
