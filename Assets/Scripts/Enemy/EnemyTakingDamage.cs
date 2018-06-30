using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakingDamage : MonoBehaviour
{
    [SerializeField] private string opponentObjAtkTagName = null;
    private static bool isAttackedByWeapon = false;

    private Transform tr = null;

    private void Start()
    {
        tr = this.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (opponentObjAtkTagName == null) { Debug.LogError("WeaponTag Name is null"); }

        if (other.tag == opponentObjAtkTagName)
        {
            CharacterStat objStat = this.gameObject.GetComponent<CharacterStat>();
            WeaponMeshCtrl meshCtrl = other.GetComponent<WeaponMeshCtrl>();
            Weapon weapon = null;

            if (meshCtrl)
            {
                weapon = meshCtrl.WeaponGameObject;
            }
            else
            {
                weapon = other.GetComponent<Weapon>();
            }

            Vector3 newPos = tr.position;
            newPos.y += 1f;

            // Paticle
            Instantiate(ParticleMng.GetInstance().EffectBulletImpactWood(), newPos, tr.rotation);
            Instantiate(ParticleMng.GetInstance().EffectBulletImpactMetal(), newPos, tr.rotation);

            if (!isAttackedByWeapon)
            {
                isAttackedByWeapon = true;
                weapon.SubtractDurability(weapon.durabilityReduce);
            }

            // Combo
            ComboSystemMng.GetInstance().AddCombo(1);

            // Taking Damage
            objStat.TakeDamage(weapon.damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isAttackedByWeapon)
            isAttackedByWeapon = false;
    }
}