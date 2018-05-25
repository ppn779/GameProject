using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMng : MonoBehaviour {
    private Transform tr = null;
    private PickUp pickup = null;
    private QuickSlot slot = null;
    private Equipment equipment = null;
	private void Start () {
        tr = this.transform;
        pickup = this.GetComponent<PickUp>();
        if (pickup == null) { pickup = this.gameObject.AddComponent<PickUp>(); }
        slot = this.transform.GetComponentInChildren<QuickSlot>();
        equipment = tr.GetComponent<Equipment>();
        if (equipment == null) { equipment = tr.gameObject.AddComponent<Equipment>(); }
	}

	private void Update () {
        if (Input.GetKeyDown(KeyCode.F)) { pickup.CheckItemInArea(tr.position); }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (slot.IsSlotEmpty(0)) { Debug.Log("비었다"); return; }
            Item it = equipment.UnEquip();
            equipment.Equip(slot.ItemList[0]);
            slot.RemoveItemInNumber(0);
            slot.AddItem(it);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (slot.IsSlotEmpty(1)) { Debug.Log("비었다"); return; }
            Item it = equipment.UnEquip();
            equipment.Equip(slot.ItemList[1]);
            slot.RemoveItemInNumber(1);
            slot.AddItem(it);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (slot.IsSlotEmpty(2)) { Debug.Log("비었다"); return; }
            Item it = equipment.UnEquip();
            equipment.Equip(slot.ItemList[2]);
            slot.RemoveItemInNumber(2);
            slot.AddItem(it);
        }
	}
}
