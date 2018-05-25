using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour {
    [SerializeField] private GameObject itemEmpty = null;
    private const int slotMax = 4;
    private Transform tr = null;
    private List<Item> itemList = new List<Item>();
    private bool[] isSlotEmpty = new bool[3];
    private int SlotCnt = 0;
    private QuickSlotImage slotImage = null;
    
    public List<Item> ItemList { get { return itemList; } }
    public bool IsSlotEmpty(int num) { return isSlotEmpty[num]; }

    private void Start()
    {
        tr = this.transform;
        slotImage = GetComponent<QuickSlotImage>();
        for (int i=0; i<3; ++i) { itemList.Add(itemEmpty.GetComponent<Item>()); }
        SlotCnt = 0;
    }
    public void AddItem(Item goItem)
    {
        if (SlotCnt >= slotMax) { Debug.Log("QuickSlot is Max : " + SlotCnt); return; }
        itemList.Add(goItem);
        isSlotEmpty[SlotCnt] = false;
        slotImage.Regist(SlotCnt, goItem.spriteWeaponIcon);
        ++SlotCnt;
    }
    public void AddItemEmpty(int num)
    {
        if (SlotCnt >= slotMax) { Debug.Log("QuickSlot is Max"); }
        Item it = itemEmpty.GetComponent<Item>();
        itemList[num] = it;
        slotImage.Regist(SlotCnt, it.spriteWeaponIcon);
        isSlotEmpty[num] = true;
    }
    public void RemoveItemInNumber(int num)
    {
         itemList.RemoveAt(num);
        isSlotEmpty[num] = true;
        --SlotCnt;
    }
    public void HideItemNumber(int num) { itemList[num].gameObject.SetActive(false); }
    public void ShowItemNumber(int num) { itemList[num].gameObject.SetActive(true); }
    public bool IsCanPickUpItem() { return SlotCnt < slotMax; }
}
