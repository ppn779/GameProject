using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackProcess : MonoBehaviour
{
    //벡터로 만든 코드
    private PlayerController playerCtrl;

    private Vector3 direction;

    private bool isKnockBackOn = false;

    [SerializeField]
    private float knockBackTime;
    [SerializeField]
    private float forcePow;
    private float time;
    private Transform tr = null;

    private void Awake()
    {
        tr = this.transform;
        playerCtrl = this.GetComponent<PlayerController>();
        time = knockBackTime;
    }

    //질문 필요.
    private void Update()
    {
        if (this.isKnockBackOn && time > knockBackTime)
        {
            isKnockBackOn = false;
            playerCtrl.IsInputSwitchOn = true;
        }
    }

    IEnumerator KnockBack()
    {
        Vector3 newPos = tr.position;
        Quaternion quater = tr.rotation;
        newPos.y += 1f;
        Instantiate(ParticleMng.GetInstance().EffectBloodSprray(), newPos, quater);
        Instantiate(ParticleMng.GetInstance().EffectBulletImpactFleshBig(), newPos, quater);

        while (time < knockBackTime)
        {
            direction.y = 0;
            playerCtrl.transform.position += (direction * forcePow) / 100;
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WeaponMesh" && !isKnockBackOn)
        {
            //Debug.Log(other.transform.forward);
            direction = other.transform.forward;
            time = 0;
            isKnockBackOn = true;
            playerCtrl.IsInputSwitchOn = false;
            StartCoroutine(KnockBack());
        }
    }

    //애드포스로 만든 코드
    //private Rigidbody rb;
    //private PlayerController playerCtrl;

    //private Vector3 direction;

    //private bool isKnockBackOn = false;

    //[SerializeField]
    //private float knockBackTime;
    //[SerializeField]
    //private float forcePow;
    //private float time;

    //private void Awake()
    //{
    //    rb = this.GetComponent<Rigidbody>();
    //    if (rb == null)
    //    {
    //        Debug.LogError(rb);
    //    }
    //    playerCtrl = this.GetComponent<PlayerController>();
    //    time = knockBackTime;
    //}

    //private void FixedUpdate()
    //{
    //    if (this.isKnockBackOn && time > 0)
    //    {
    //        KnockBack();
    //        time -= Time.deltaTime;
    //        playerCtrl.IsInputSwitchOn = false;
    //    }
    //    else if (this.isKnockBackOn && time <= 0)
    //    {
    //        isKnockBackOn = false;
    //        time = knockBackTime;
    //        playerCtrl.IsInputSwitchOn = true;
    //    }
    //}

    //private void KnockBack()
    //{
    //    rb.AddForce(direction * forcePow);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "WeaponMesh")
    //    {
    //        Debug.Log(other.transform.forward);
    //        direction = other.transform.forward;
    //        isKnockBackOn = true;
    //    }
    //}
}
