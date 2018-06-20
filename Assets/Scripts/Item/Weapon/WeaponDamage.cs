using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour {

    protected float atkPow;

    public float AtkPow
    {
        get
        {
            return atkPow;
        }
        set
        {
            atkPow = value;
        }
    }
}
