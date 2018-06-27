using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControl : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
