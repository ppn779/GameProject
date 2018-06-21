using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(CharacterStat))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab = null;
    public GameObject debugPrefab = null;
    public Transform target = null;

    private float visibleTime = 10f;
    private float lastMadeVisibleTime = 0f;

    private Transform ui = null;
    private Transform debug = null;
    private Image healthSlider = null;

    private Text healthText = null;
    private CharacterStat stat = null;

    private const float OFFSET_Y = 140.0f;

    private void Awake()
    {
        stat = this.transform.GetComponent<CharacterStat>();
    }

    private void Start()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;

                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(true);
                break;
            }
        }
        //foreach (Canvas c in FindObjectsOfType<Canvas>())
        //{
        //    if (c.renderMode == RenderMode.ScreenSpaceOverlay)
        //    {
        //        debug = Instantiate(debugPrefab, c.transform).transform;

        //        healthText = debug.GetChild(0).GetComponent<Text>();
        //        debug.gameObject.SetActive(true);
        //        break;
        //    }
        //}
        GetComponent<CharacterStat>().OnHealthChanged += OnHealthChanged;
    }

    private void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            DisplayHP();
            // 일정 시간 후 체력바 사라짐
            if (Time.time - lastMadeVisibleTime > visibleTime)
            {
                //ui.gameObject.SetActive(false); 
            }
            //Follow();
        }
    }

    //private void Follow()
    //{
    //    // 월드(World)상에 존재하는 플레이어의 위치를
    //    // UI가 있는 스크린 좌표로 변환
    //    Debug.Log(this.name);
    //    Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
    //    // 간격 적용
    //    pos.y += OFFSET_Y;
    //    // 위치 갱신
    //    healthText.transform.position = pos;

    //}

    private void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0 || target == null)
            {
                Destroy(ui.gameObject);
            }
        }
    }
    private void DisplayHP()
    {
        //Debug.Log(stat.currentHealth + "  /  " + stat.maxHealth);
        //healthText.text = "";
        //healthText.text = stat.currentHealth + " / " + stat.maxHealth;
    }
}
