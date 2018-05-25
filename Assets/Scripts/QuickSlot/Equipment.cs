using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    private Transform tr = null;
    private Item equippedItem = null;
    private bool isEquipWeapon = false;
    public bool IsEquipWeapon{ get { return isEquipWeapon; } }
    public Item GetEquippedItem { get { return equippedItem; } }

	private void Start () {
        tr = this.transform;	
	}
    public void Equip(Item it)
    {
        if (!isEquipWeapon)
        {
            GameObject go =  FindForName(tr , "Right Hand");
            GameObject goItem = Instantiate(it.gameObject, go.transform.position, go.transform.rotation, go.transform);
            goItem.AddComponent<Item>();
            equippedItem = goItem.GetComponent<Item>();
            isEquipWeapon = true;
        }
    }
    public Item UnEquip()
    {
        if (isEquipWeapon)
        {
            if (equippedItem != null) {
                Debug.Log(equippedItem.name + "을 장착해제했다");
                equippedItem.gameObject.SetActive(false);
                isEquipWeapon = false;
                return equippedItem;
                
            }
        }
        return null;
    }
    private GameObject FindForName(Transform findTr , string equipmentName)
    {
        Transform[] trChild = findTr.GetComponentsInChildren<Transform>();
        foreach(Transform trEach in trChild)
        {
            if (trEach.name.Contains(equipmentName))
            {
                return trEach.gameObject;
            }
        }
        Debug.Log("Cant FindForName [" + equipmentName + "]");
        return null;
    }
    private GameObject FindForTag(Transform findTr , string equipmentTag)
    {
        Transform[] goChild = findTr.GetComponentsInChildren<Transform>();
        foreach (Transform trEach in goChild)
        {
            if (trEach.gameObject.CompareTag(equipmentTag))
            {
                return trEach.gameObject;
            }
        }
        Debug.Log("Cant FindForTag [" + equipmentTag + "]");
        return null;
    }
}
