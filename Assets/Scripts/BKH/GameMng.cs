using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    //public string sceneName;

    private static GameMng instance = null;
    private Transform resultUI = null;

    private void Awake()
    {
        resultUI = this.transform.GetComponentInChildren<RectTransform>();
        Debug.Log(resultUI);
        resultUI.gameObject.SetActive(false);
    }


    public static GameMng Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(GameMng)) as GameMng;

                if(instance == null)
                {
                    Debug.LogError("GameMng is null");
                    return null;
                }
            }
            return instance;
        }
    }

    public void ReGame()
    {
        StartCoroutine(GameMng.Instance.Restart(1));
    }

    public IEnumerator Restart(int wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("Main");
    }

    public void SetActiveResultCanvas()
    {
        Debug.Log("SetActive Canvas");
        StartCoroutine(GameMng.Instance.SetActiveResultCavasCoroutine());
    }

    private IEnumerator SetActiveResultCavasCoroutine()
    {
        yield return new WaitForSeconds(1);

        Debug.Log(resultUI);
        resultUI.gameObject.SetActive(true);
    }
}
