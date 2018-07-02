using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectile = null;

    private Transform tr;
    private float fireTime = 0.2f;


    public override void Attack(Transform _objTr, float _objAtkPow)
    {
        this.tr = _objTr;
        fireTime = 0.2f;
        StartCoroutine(Fire(_objAtkPow));
    }

    private IEnumerator Fire(float _objAtkPow)
    {
        while (fireTime > 0)
        {
            fireTime -= Time.deltaTime;
            //GameObject go = Instantiate<GameObject>(projectile, this.transform.position, this.transform.rotation);
            //go.transform.parent = this.transform;
            Vector3 newPos = this.transform.position;
            newPos += (tr.forward * 1.2f);
            newPos.y = 1.0f;

            float projectileAngleX = tr.transform.rotation.eulerAngles.x;
            float projectileAngleY = tr.transform.rotation.eulerAngles.y;
            float projectileAngleZ = tr.transform.rotation.eulerAngles.z;

            Quaternion wantProjectileAngle1 = Quaternion.Euler(projectileAngleX, projectileAngleY - 40.0f, projectileAngleZ);
            Quaternion wantProjectileAngle2 = Quaternion.Euler(projectileAngleX, projectileAngleY - 20.0f, projectileAngleZ);
            Quaternion wantProjectileAngle3 = Quaternion.Euler(projectileAngleX, projectileAngleY, projectileAngleZ);
            Quaternion wantProjectileAngle4 = Quaternion.Euler(projectileAngleX, projectileAngleY + 20.0f, projectileAngleZ);
            Quaternion wantProjectileAngle5 = Quaternion.Euler(projectileAngleX, projectileAngleY + 40.0f, projectileAngleZ);

            GameObject objProjectile1 = Instantiate<GameObject>(projectile, newPos, wantProjectileAngle1);
            GameObject objProjectile2 = Instantiate<GameObject>(projectile, newPos, wantProjectileAngle2);
            GameObject objProjectile3 = Instantiate<GameObject>(projectile, newPos, wantProjectileAngle3);
            GameObject objProjectile4 = Instantiate<GameObject>(projectile, newPos, wantProjectileAngle4);
            GameObject objProjectile5 = Instantiate<GameObject>(projectile, newPos, wantProjectileAngle5);

            //Debug.Log("objProjectile = " + objProjectile);
            //objProjectile.GetComponent<ProjectileCtrl>().damage = _objAtkPow;
            //SubtractUsableCount(1);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
