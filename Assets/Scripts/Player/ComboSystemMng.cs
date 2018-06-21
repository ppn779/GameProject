using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This Component For Player
public class ComboSystemMng : MonoBehaviour
{
    [SerializeField] private GameObject comboTextUi = null;
    private static ComboSystemMng instance = null;
    private Text textUi = null; 
    private int comboCount = 0;

    private void Start()
    {
        if (!comboTextUi)
            Debug.LogError("comboTextUi is Null , This var Must have GameObject what TextUI for use ComboSystem");
        if (comboTextUi)
        {
            textUi = comboTextUi.GetComponent<Text>();
            if (!textUi)
                Debug.LogError("textUi is Null , Maybe comboTextUi dont have Text Component");
        }
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
        textUi.text = comboCount.ToString();
    }
}
