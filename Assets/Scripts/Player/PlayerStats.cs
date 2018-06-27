using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStat {

    [SerializeField]
    private float atkSpeed = 0f;
    [SerializeField]
    private float movementSpeed = 0f;
    [SerializeField]
    private float resistance = 0f;//저항력

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void HealthUp(float HP)
    {
        currentHealth += HP;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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

    public override void Die()
    {
        base.Die();

        //CanvasControl canvasControl = GetComponent<CanvasControl>();
        //if (canvasControl == null) { Debug.LogError("canvasControl is null"); }
        //canvasControl.ShowResultCanvas(true);

        GameMng.Instance.GameOver();

        Destroy(gameObject);
    }
}
