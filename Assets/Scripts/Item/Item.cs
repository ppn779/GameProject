using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public float pickupRange = 3f;
    public int remainCount = 3;
    public Sprite spriteWeaponIcon = null;
    private bool isUseful = true;
    private bool isDestroyed = false;

    public Item GetItemType() { return this; }
    static public Item Create(Item it) { return Instantiate(it); }
    static public Item Create(Item it , Transform trParent) { return Instantiate(it, trParent); }
    static public Item Create(Item it , Vector3 pos , Quaternion rot) { return Instantiate(it, pos, rot); }
    public bool IsUseful
    {
        get { return isUseful; }
        set { isUseful = value; }
    }
    public bool IsDestroyed
    {
        get { return isDestroyed; }
        set { isDestroyed = value; }
    }
}
