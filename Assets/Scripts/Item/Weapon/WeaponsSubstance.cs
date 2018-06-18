using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSubstance : MonoBehaviour {

    protected float calculatedAtkPow;

    public float CalculatedAtkPow
    {
        get
        {
            return calculatedAtkPow;
        }
        set
        {
            calculatedAtkPow = value;
        }
    }
}
