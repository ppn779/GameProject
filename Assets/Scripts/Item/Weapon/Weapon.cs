using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public Vector3 attackRange = Vector3.zero;
    public float damage = 50f;
    public float attackSpeed = 1.5f;
    public float remainTime = 5f;
    
    private Transform tr = null;
    private bool isPlayerEquipped = false;
    private BoxCollider boxcollider = null;
    private Vector3 attackRangeTemp = Vector3.zero;

    private void Start()
    {
        tr = this.transform;
        boxcollider = tr.GetComponent<BoxCollider>();
        if (boxcollider == null) { tr.gameObject.AddComponent<BoxCollider>(); }
        boxcollider.size = attackRange;
    }
    public bool IsPlayerEquipped
    {
        get { return isPlayerEquipped; }
        set { isPlayerEquipped = value; }
    }
    public void ChangeAttackRange(Vector3 vecNewRange, float time)
    {
        attackRangeTemp = attackRange;
        StartCoroutine(ExpiredAttackRange());
    }
    private IEnumerator ExpiredAttackRange()
    {
        attackRange = attackRangeTemp;
        yield return null;
    }
}
