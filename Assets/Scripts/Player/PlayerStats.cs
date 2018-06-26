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
    public float downLifeValueForCustomSec = 0.0f;//정해진 초당 체력을 깍는 수치
    public float customSec = 0.0f;
    private float elapsedTime = 0.0f;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //Debug.Log("커스톰시간 : "+customSec);
        //Debug.Log("지나간시간 : " + elapsedTime);
        if (elapsedTime > customSec)
        {
            if (currentHealth > 0)
            {
                currentHealth -= downLifeValueForCustomSec;
            }
            else
            {
                Die();
            }
            elapsedTime = 0.0f;
        }
        elapsedTime += Time.deltaTime;
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

        GameMng.Instance.SetActiveResultCanvas();

        Destroy(gameObject);
    }
}
