using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField]
    private int hp = 0;
    [SerializeField]
    private float atkPower = 0f;
    [SerializeField]
    private float atkSpeed = 0f;
    [SerializeField]
    private float movementSpeed = 0f;
    [SerializeField]
    private float resistance = 0f;//저항력

    public int HP
    {
        get
        {
            return hp;
        }
    }

    public float AtkPower
    {
        get
        {
            return atkPower;
        }
    }

    public float AtkSpeed
    {
        get
        {
            return atkSpeed;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
    }

    public float Resistance
    {
        get
        {
            return resistance;
        }
    }
}
