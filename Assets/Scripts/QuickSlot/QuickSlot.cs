using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour {
    [SerializeField] private GameObject itemEmpty = null;
    private const int slotMax = 5;
    private Transform tr = null;
    private List<Item> itemList = new List<Item>();
    private Item itemMain = null;
    private bool[] isSlotEmpty = null;
    private int SlotCnt = 0;
    private QuickSlotImage slotImage = null;
    
    public List<Item> ItemList { get { return itemList; } }
    public bool IsSlotEmpty(int num) { return isSlotEmpty[num]; }

    private void Awake()
    {
        tr = this.transform;
        slotImage = GetComponent<QuickSlotImage>();
        for (int i=0; i<slotMax; ++i) { itemList.Add(itemEmpty.GetComponent<Item>()); }
        SlotCnt = 0;
        isSlotEmpty = new bool[5];
    }
    public void AddItem(int num , Item goItem)
    {
        if (SlotCnt >= slotMax) { Debug.Log("QuickSlot is Max : " + SlotCnt); return; }
        itemList.Insert(num, goItem);
        isSlotEmpty[num] = false;
        slotImage.Regist(num, goItem.spriteWeaponIcon);
        ++SlotCnt;

        //DebugSystem.Create(new Vector3(0f , 0f , 0f), "done");
    }
    public void AddItemMain(Item goItem)
    {
        itemMain = goItem;
        slotImage.RegistMain(goItem.spriteWeaponIcon);
    }
    public void AddItemEmpty(int num)
    {
        if (SlotCnt >= slotMax) { Debug.Log("QuickSlot is Max"); }
        //Item it = itemEmpty.GetComponent<Item>();
        //itemList[num] = it;
        //slotImage.Regist(SlotCnt, it.spriteWeaponIcon);
        isSlotEmpty[num] = true;
    }
    public void RemoveItemInNumber(int num)
    {
        Item it = itemList[num];
        slotImage.RemoveAt(num);
        itemList.RemoveAt(num);
        isSlotEmpty[num] = true;
        --SlotCnt;
    }
    public void RemoveItemMain()
    {
        slotImage.RemoveMain();
        itemMain = null;
    }
    public Item GetItemListNumber(int num)
    {
        return itemList[num];
    }
    public int GetEmptySlot()
    {
        for (int i = 0; i < slotMax; ++i) { if (isSlotEmpty[i]) { return i; } }
        return -1;
    }
    public bool IsCanPickUpItem() { return SlotCnt < slotMax; }
}