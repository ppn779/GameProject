using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuickSlot))]
[RequireComponent(typeof(QuickSlotImage))]
public class QuickSlotMng : MonoBehaviour {
    private Transform tr = null;
    private QuickSlot slot = null;
    private QuickSlotImage slotImage = null;
    private PickUp pickup = null;
    private Equipment equipment = null;

    private void Start()
    {
        tr = this.transform;
        slot = GetComponent<QuickSlot>();
        slotImage = GetComponent<QuickSlotImage>();
        pickup = tr.parent.GetComponent<PickUp>();
        if (pickup == null) { pickup = tr.parent.gameObject.AddComponent<PickUp>(); }
        equipment = tr.parent.GetComponent<Equipment>();
        if (equipment == null) { equipment = tr.parent.gameObject.AddComponent<Equipment>(); }

        for (int i=0; i<3; ++i) { slot.AddItemEmpty(i); }
    }
    private void Update()
    {
        if (pickup.IsExistAroundItem)
        {
            pickup.IsExistAroundItem = false;
            if (!equipment.IsEquipWeapon) { equipment.Equip(pickup.GetPickupItem); }
            else {
                slot.AddItem(pickup.GetPickupItem);
            }
            pickup.GetPickupItem.gameObject.SetActive(false);
        }
    }
}
