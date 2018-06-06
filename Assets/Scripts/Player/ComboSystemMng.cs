using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystemMng : MonoBehaviour
{
    private PlayerStats playerStats;

    private int count = 0;
    private bool resetComboCountTimer = false;
    private bool attackSuccessed = false;
    private float countTimer = 0.0f;
    private void Awake()
    {
        playerStats = this.GetComponentInParent<PlayerStats>();
    }

    private void Update()
    {
        if (resetComboCountTimer && countTimer > 0)
        {
            countTimer -= Time.deltaTime;
        }
        else if (resetComboCountTimer && countTimer <= 0)
        {
            count = 0;
            countTimer = 2.5f;
            resetComboCountTimer = false;
        }
    }
    public int Count
    {
        get
        {
            return count;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            count++;
            resetComboCountTimer = true;
            countTimer = 2.5f;
            playerStats.HealthUp(300);
        }
    }
}
