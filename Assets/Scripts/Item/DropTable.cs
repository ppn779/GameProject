using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour {
    [SerializeField] private GameObject dropTable01 = null;
    [SerializeField] private GameObject dropTable02 = null;
    [SerializeField] private GameObject dropTable03 = null;
    [SerializeField] private GameObject dropTable04 = null;
    [SerializeField] private GameObject dropTable05 = null;
    [SerializeField] private GameObject dropTable06 = null;
    private const int tableMax = 6;
    private List<GameObject> listTemp = new List<GameObject>();
    private void Start () {
        if (dropTable01.GetComponent<Item>() == null) { dropTable01.AddComponent<Item>(); }
        if (dropTable02.GetComponent<Item>() == null) { dropTable02.AddComponent<Item>(); }
        if (dropTable03.GetComponent<Item>() == null) { dropTable03.AddComponent<Item>(); }
        if (dropTable04.GetComponent<Item>() == null) { dropTable04.AddComponent<Item>(); }
        if (dropTable05.GetComponent<Item>() == null) { dropTable05.AddComponent<Item>(); }
        if (dropTable06.GetComponent<Item>() == null) { dropTable06.AddComponent<Item>(); }
        
        if (dropTable01 != null) { listTemp.Add(dropTable01); }
        if (dropTable02 != null) { listTemp.Add(dropTable02); }
        if (dropTable03 != null) { listTemp.Add(dropTable03); }
        if (dropTable04 != null) { listTemp.Add(dropTable04); }
        if (dropTable05 != null) { listTemp.Add(dropTable05); }
        if (dropTable06 != null) { listTemp.Add(dropTable06); }
    }
    /*
    public Item GetRandomItem()
    {
        listTemp.ForEach()
    }
    */
}
