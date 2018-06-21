using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private GameObject itemEmpty = null;
    public static int SLOTMAX = 5;
    private Transform tr = null;
    private Item[] itemList = new Item[SLOTMAX];
    private Item itemMain = null;
    private bool[] isSlotEmpty = null;
    private int SlotCnt = 0;
    private QuickSlotImage slotImage = null;
    
    public Item[] ItemList { get { return itemList; } }
    public bool IsSlotEmpty(int num) { return isSlotEmpty[num]; }

    private void Start()
    {
        tr = this.transform;
        slotImage = GetComponent<QuickSlotImage>();
        isSlotEmpty = new bool[5];
        SlotCnt = 0;

        AddItemMainEmpty();
        for (int i = 0; i < SLOTMAX; ++i)
        {
            //itemList.Add(itemEmpty.GetComponent<Item>());
            AddItemEmpty(i);
        }
        
    }
    public void AddItem(int num, Item goItem)
    {
        if (SlotCnt >= SLOTMAX) { Debug.Log("QuickSlot is Max : " + SlotCnt); return; }
        itemList[num] = goItem;
        //itemList.Insert(num, goItem);
        isSlotEmpty[num] = false;
        slotImage.Regist(num, goItem.spriteWeaponIcon);
        ++SlotCnt;
        
        Weapon weapon = goItem.GetComponent<Weapon>();
        DebugSystem.GetInstance().ShowQuickSlot(num, "Name : " + goItem.name + "\nDamage : " + weapon.damage + "\nAtkSpeed : " + weapon.attackSpeed + "\nDurability : " + weapon.durability + "\nRemainCnt : " + weapon.usableCount + "\nIsTypeMelee : " + weapon.isWeaponTypeMelee.ToString());
    }
    public void AddItemMain(Item goItem)
    {
        itemMain = goItem;
        slotImage.RegistMain(goItem.spriteWeaponIcon);

        Weapon weapon = itemMain.GetComponent<Weapon>();
        DebugSystem.GetInstance().ShowQuickSlotMain("Name : " + itemMain.name + "\nDamage : " + weapon.damage + "\nAtkSpeed : " + weapon.attackSpeed + "\nDurability : " + weapon.durability + "\nRemainCnt : " + weapon.usableCount + "\nIsTypeMelee : " + weapon.isWeaponTypeMelee.ToString());
    }
    public void AddItemEmpty(int num)
    {
        if (SlotCnt >= SLOTMAX) { Debug.Log("QuickSlot is Max"); }
        Item it = itemEmpty.GetComponent<Item>();
        itemList[num] = it;
        slotImage.Regist(num, it.spriteWeaponIcon);
        isSlotEmpty[num] = true;
    }
    public void AddItemMainEmpty()
    {
        Item it = itemEmpty.GetComponent<Item>();
        itemMain = it;
        slotImage.RegistMain(it.spriteWeaponIcon);
    }
    public void RemoveItemInNumber(int num)
    {
        Item it = itemList[num];
        slotImage.RemoveAt(num);
        itemList[num] = null;
        //itemList.Insert(num, itemEmpty.GetComponent<Item>());
        isSlotEmpty[num] = true;
        --SlotCnt;
    }
    public void RemoveItemMain()
    {
        slotImage.RemoveMain();
        itemMain = null;
    }
    public Item GetItemListNumber(int num) { return itemList[num]; }
    public Item GetItemMain() { return itemMain; }
    public int GetEmptySlot()
    {
        for (int i = 0; i < SLOTMAX; ++i) { if (isSlotEmpty[i]) { return i; } }
        return -1;
    }
    public bool IsCanPickUpItem() { return SlotCnt < SLOTMAX; }
}