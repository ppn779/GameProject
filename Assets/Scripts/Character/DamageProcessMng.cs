//인스펙터 창에서 atkObjTagName에 공격하는 오브젝트의 Tag 이름을 넣어줘야 함.
//WeaponMesh는 AtkMng가 들어있는 오브젝트의 자식 오브젝트로 들어있어야 함.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProcessMng : MonoBehaviour
{
    [SerializeField]
    private string opponentObjAtkTagName;


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == opponentObjAtkTagName)
        {
            CharacterStat objStat = this.gameObject.GetComponent<CharacterStat>();
            AtkMng atkMng = other.GetComponentInParent<AtkMng>();
            objStat.TakeDamage(atkMng.Attack());
            Debug.Log("Take Damage");
        }
    }
}
