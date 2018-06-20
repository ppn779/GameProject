using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private const float PICKUP_RANGE = 6f;
    private Transform tr = null;
    private List<Item> listAround = null;
    private bool isExistAroundItem = false;
    private Item pickupItem = null;
    public List<Item> GetListAround { get { return listAround; } }
    public bool IsExistAroundItem { get { return isExistAroundItem; } set { isExistAroundItem = value; } }
    public Item GetPickupItem { get { return pickupItem; } }

    private void Start()
    {
        tr = this.transform;
        listAround = new List<Item>();
    }
    public void CheckItemInArea(Vector3 pos)
    {
        foreach (Item item in listAround)
        {
            if (Vector3.Distance(pos, item.transform.position) < PICKUP_RANGE)
            {
                IsExistAroundItem = true;
                pickupItem = item;
                listAround.Remove(item);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            //Debug.Log(collider.gameObject.name + "아이템이 근처에 있다");
            Item it = collider.gameObject.GetComponent<Item>();
            if (it == null) { collider.gameObject.AddComponent<Item>(); }
            listAround.Add(it);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            //Debug.Log(collider.gameObject.name + "아이템이 근처에서 벗어났다");
            Item it = collider.gameObject.GetComponent<Item>();
            if (it == null) { collider.gameObject.AddComponent<Item>(); }
            listAround.Remove(it);
        }
    }
}
