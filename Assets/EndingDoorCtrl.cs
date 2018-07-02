using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDoorCtrl : MonoBehaviour {
    public GameObject obj= null;

	private void Update()
    {
        if (!obj)
        {
            Destroy(this.gameObject);
        }
    }
}
