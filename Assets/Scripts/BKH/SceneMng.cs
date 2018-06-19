using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
