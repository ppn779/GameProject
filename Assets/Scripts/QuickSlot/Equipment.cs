using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private GameObject rightHand = null;
    private Transform tr = null;
    private Weapon equippedItem = null;
    private AtkMng atkMng = null;
    private bool isEquipWeapon = false;
    public bool IsEquipWeapon
    {
        get { return isEquipWeapon; }
        set { isEquipWeapon = value; }
    }
    public Item GetEquippedItem { get { return equippedItem; } }

    private void Start()
    {
        tr = this.transform;
        atkMng = this.GetComponent<AtkMng>();
    }

    public void Equip(Item it)
    {
        if (!isEquipWeapon)
        {
            //int abc = DebugSystem.Create(it.name);
            //DebugSystem.SetText(abc, "I am Groot");

            GameObject go = rightHand;
            BoxCollider boxCollider = it.GetComponent<BoxCollider>();

            if (go == null) { Debug.LogError("go is null"); }
            if (!it.gameObject.activeInHierarchy) { it.gameObject.SetActive(true); }
            if (boxCollider != null) { Destroy(boxCollider); }

            it.transform.parent = go.transform;
            it.transform.position = go.transform.position;
            equippedItem = it.transform.GetComponent<Weapon>();
            isEquipWeapon = true;
            equippedItem.IsPlayerEquipped = true;

            if (equippedItem != null)
            {
                atkMng.AtkPower += equippedItem.damage;
                atkMng.AtkSpeed += equippedItem.attackSpeed;
                atkMng.IsEquippedWeapon = isEquipWeapon;
                atkMng.Weapon = equippedItem;
            }
        }
    }

    public Item UnEquip()
    {
        if (isEquipWeapon)
        {
            if (equippedItem != null)
            {
                isEquipWeapon = false;
                atkMng.IsEquippedWeapon = isEquipWeapon;

                atkMng.AtkPower -= equippedItem.damage;
                atkMng.AtkSpeed -= equippedItem.attackSpeed;
                equippedItem.transform.parent = null;
                equippedItem.transform.parent = null;
                equippedItem.gameObject.SetActive(false);
                return equippedItem;
            }
        }
        return null;
    }
}
