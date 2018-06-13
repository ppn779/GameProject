//******************************************
//*
//******************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSystem : MonoBehaviour
{
    public static List<RectTransform> listDebug = null;
    private const int ArrayIndexMax = 8192; 
    private static RectTransform rectTr = null;
    private static RectTransform[] rectArr = new RectTransform[ArrayIndexMax];
    private static Text[] textArr = new Text[ArrayIndexMax];
    private static int indexMax = 0;
    private void Start()
    {
        listDebug = new List<RectTransform>();
        rectTr = GetComponent<RectTransform>();
        if (rectTr == null) { Debug.Log("rectTr is null"); }
    }
    public static int Create(Vector3 vecPos, string str)
    {
        int emptyIdx = FindEmptyIndex();
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(go.GetComponent<MeshFilter>());
        Destroy(go.GetComponent<BoxCollider>());
        Destroy(go.GetComponent<MeshRenderer>());
        rectArr[emptyIdx] = go.AddComponent<RectTransform>();
        CanvasRenderer canvasRenderer = go.AddComponent<CanvasRenderer>();
        textArr[emptyIdx] = go.AddComponent<Text>();
        go.AddComponent<DebugDraggable>();
        listDebug.Insert(emptyIdx, rectArr[emptyIdx]);

        vecPos.x += 512f;
        vecPos.y += 384f;
        go.transform.parent = rectTr.transform;
        rectArr[emptyIdx].position = vecPos;
        textArr[emptyIdx].text = str;
        textArr[emptyIdx].font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        textArr[emptyIdx].alignment = TextAnchor.MiddleLeft;
        textArr[emptyIdx].fontSize = 16;



        if (emptyIdx  == indexMax) { ++indexMax; }
        return emptyIdx;
    }

    public static void Destroy(int idx)
    {
        listDebug.RemoveAt(idx);
        Destroy(listDebug[idx].gameObject);

        if (idx+1 == indexMax) { --indexMax; }
    }

    public static void SetPosition(int idx , Vector3 newPos)
    {
        newPos.x += 512f;
        newPos.y += 384f;
        rectArr[idx].position = newPos;
    }
    public static void SetText(int idx , string str) { textArr[idx].text = str; }
    public static void SetTextFont(int idx , string font) { textArr[idx].font = (Font)Resources.GetBuiltinResource(typeof(Font), font); }
    public static void SetTextFontSize(int idx , int size) { textArr[idx].fontSize = size; }
    public static void SetTextAnchor(int idx , TextAnchor anchor) { textArr[idx].alignment = anchor; }

    private static int FindEmptyIndex() {
        if (indexMax <= 0) { return 0; }
        for (int i = 0; i < indexMax; ++i) {
            if (listDebug[i] == null) { return i; }
        }
        return indexMax;
    }
}