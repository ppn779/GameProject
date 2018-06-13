﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public float pickupRange = 3f;
    public int remainCount = 3;
    public Sprite spriteWeaponIcon = null;
    private bool isUseful = true;
    private bool isDestroyed = false;

    public Item GetItemType() { return this; }
    static public Item Create(GameObject itemGo) {
        GameObject go = Instantiate(itemGo);
        Item it = go.AddComponent<Item>();
        return it;
    }
    static public Item Create(GameObject itemGo, Transform trParent) {
        GameObject go = Instantiate(itemGo , trParent);
        Item it = go.AddComponent<Item>();
        AddComponent(go);
        return it;
    }
    static public Item Create(GameObject itemGo, Vector3 pos , Quaternion rot) {
        GameObject go = Instantiate(itemGo, pos , rot);
        Item it = go.AddComponent<Item>();
        return it;
    }
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

    private static void AddComponent(GameObject go)
    {
        go.AddComponent<ItemMovement>();
    }
}
