using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour {
    private Transform tr = null;
	private void Start () {
        tr = this.transform;
	}
	
	private void Update () {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.x = tr.position.x;
        cameraPos.z = tr.position.z;
        Camera.main.transform.position = cameraPos;
	}
}
