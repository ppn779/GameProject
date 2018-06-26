using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This Component For Player
// comboUiText is only use GUI Text GameObject
// comboUiBar is only use GUI Image GameObject
public class ComboSystemMng : MonoBehaviour
{
    [SerializeField] private GameObject comboUiText = null;
    [SerializeField] private GameObject comboUiBar = null;
    public const float comboTimeDefault = 4f;
    private const float textUiHeight = 4f;
    private static ComboSystemMng instance = null;
    private Transform tr = null;
    private RectTransform comboUiTextRct = null;
    private RectTransform comboUiBarRctOver = null;
    private Text textUi = null;
    private int comboCount = 0;
    private float comboMaxTime = comboTimeDefault;
    private float comboCurTime = comboTimeDefault;

    private void Start()
    {
        tr = this.transform;
        if (!comboUiText)
            Debug.LogError("comboUiText is Null , This var Must have GameObject what TextUI for use ComboSystem");
        if (!comboUiBar)
            Debug.LogError("comboUiBar is NULL , This var Must have GameObject what BarUI for use ComboSystem");
        if (comboUiText)
        {
            textUi = comboUiText.GetComponent<Text>();
            if (!textUi)
                Debug.LogError("textUi is Null , Maybe comboTextUi dont have Text Component");
            comboUiTextRct = comboUiText.GetComponent<RectTransform>();
            comboUiBarRctOver = comboUiBar.GetComponent<RectTransform>();
        }
        StartCoroutine(CoroutineReduceLife());
    }

    public static ComboSystemMng GetInstance()
    {
        if (!instance)
        {
            instance = (ComboSystemMng)GameObject.FindObjectOfType(typeof(ComboSystemMng));
            if (!instance)
                Debug.LogError("instance is Null,, Can't Find GameObject what Have ComboSystemMng Component");
        }
        return instance;
    }
    
    public void AddCombo(int num)
    {
        ++comboCount;
        SetTextCount(comboCount);
        comboCurTime = comboMaxTime;
    }
    private IEnumerator CoroutineReduceLife()
    {
        float fps60 = 1 / 60f;
        Image imageUiOver = comboUiBarRctOver.GetComponent<Image>();
        if (!imageUiOver)   
            Debug.LogError("imageUiOver is NULL,,");
        while (true)
        {
            Vector2 vecSize = comboUiBarRctOver.sizeDelta;
            Vector3 newPos = tr.position;
            float remainTime = (comboCurTime / comboMaxTime);
            if (remainTime <= 0f)
                SetTextCount(0);
            comboCurTime -= fps60;
            newPos.y += textUiHeight;
            comboUiTextRct.position = Camera.main.WorldToScreenPoint(newPos);
            vecSize.y = remainTime * 100f;
            comboUiBarRctOver.sizeDelta = vecSize;
            if (remainTime > 0.6f)
                imageUiOver.color = Color.green;
            else if (remainTime > 0.3f)
                imageUiOver.color = Color.yellow;
            else
                imageUiOver.color = Color.red;
            yield return new WaitForSeconds(fps60);
        }
    }
    private void SetTextCount(int num)
    {
        textUi.text = "Combo " + num.ToString();
    }
}
