using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    //public string sceneName;
    private bool isPause = false;
    private static GameMng instance = null;
    private Transform resultUI = null;

    private void Awake()
    {
        resultUI = this.transform.GetComponentInChildren<RectTransform>();
        if (resultUI == null) { return; }
        resultUI.gameObject.SetActive(false);
    }

    public static GameMng Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameMng)) as GameMng;

                if (instance == null)
                {
                    Debug.LogError("GameMng is null");
                    return null;
                }
            }
            return instance;
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameMng.Instance.GameOver(1));
    }

    //public void Menu()
    //{
    //    if (isPause)
    //    {
    //        Time.timeScale = 0;
    //        isPause = false;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1;
    //        isPause = true;
    //    }
    //}

    public IEnumerator GameOver(int wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("EndingScene");
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            resultUI.gameObject.SetActive(true);
        }
    }


}
