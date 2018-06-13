using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour {
    [System.Serializable]
    public class DropCurrency {
        public string name;
        public GameObject item;
        public float dropDice;
    }
    public List<DropCurrency> dropTable = new List<DropCurrency>();
    private Transform tr = null;

    private void Start()
    {
        tr = this.transform;
    }

    public void GetRandomItem()
    {
        float rand = 0f;
        for (int i = 0; i < dropTable.Count; ++i)
        {
            rand = Random.Range(0f, 1f);
            if (rand > dropTable[i].dropDice)
            {
                Item.Create(dropTable[i].item, tr.position, tr.rotation);
                return;
            }
        }
    }
}