using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public float atkStartDist = 0.0f;
    public float attackRange = 0.0f;
    public int damage = 0;
    public float attackSpeed = 0.0f;
    public float weaponAngle = 0.0f;
    public float remainTime = 5f;
    
    private Transform tr = null;
    private bool isPlayerEquipped = false;
    private Vector3 attackRangeTemp = Vector3.zero;

    private void Start()
    {
        tr = this.transform;
    }
    public bool IsPlayerEquipped
    {
        get { return isPlayerEquipped; }
        set { isPlayerEquipped = value; }
    }
}
