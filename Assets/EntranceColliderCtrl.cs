using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceColliderCtrl : MonoBehaviour {
    private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null) { Debug.LogError("입구 트리거 콜라이더 null"); }
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            boxCollider.isTrigger = false;
        }
    }
}
