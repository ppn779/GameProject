using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private GameObject rightHand = null;
    private Transform tr = null;
    private Weapon equippedItem = null;
    private PlayerAtkMng playerAtkMng = null;
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
        playerAtkMng = this.GetComponent<PlayerAtkMng>();
    }

    public void Equip(Item it)
    {
        if (!isEquipWeapon)
        {
            //int abc = DebugSystem.Create(it.name);
            //DebugSystem.SetText(abc, "I am Groot");

            if (it == null) { Debug.LogError("it is null"); }

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
                playerAtkMng.AtkPower += equippedItem.damage;
                playerAtkMng.AtkSpeed += equippedItem.attackSpeed;
                playerAtkMng.IsEquippedWeapon = isEquipWeapon;
                playerAtkMng.EquippedWeapon = equippedItem;
                playerAtkMng.MakeDebugWeaponMesh();//디버그용
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
                playerAtkMng.IsEquippedWeapon = isEquipWeapon;

                playerAtkMng.AtkPower -= equippedItem.damage;
                playerAtkMng.AtkSpeed -= equippedItem.attackSpeed;
                playerAtkMng.EquippedWeapon = null;
                playerAtkMng.IsReady = false;
                equippedItem.transform.parent = null;
                equippedItem.gameObject.SetActive(false);
                return equippedItem;
            }
        }
        return null;
    }
}
