using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEnding : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
}
