using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotImage : MonoBehaviour {
    [SerializeField] private GameObject slot0 = null;
    [SerializeField] private GameObject slot1 = null;
    [SerializeField] private GameObject slot2 = null;
    private Image imageSlot0 = null;
    private Image imageSlot1 = null;
    private Image imageSlot2 = null;
	private void Start () {
        if (slot0 != null) { imageSlot0 = slot0.GetComponent<Image>(); }
        if (slot1 != null) { imageSlot1 = slot1.GetComponent<Image>(); }
        if (slot2 != null) { imageSlot2 = slot2.GetComponent<Image>(); }
	}
    public void Regist(int slotNum , Sprite spriteImage)
    {
        switch (slotNum)
        {
            case 0:
                imageSlot0.sprite = spriteImage;
                break;
            case 1:
                imageSlot1.sprite = spriteImage;
                break;
            case 2:
                imageSlot2.sprite = spriteImage;
                break;
        }
    }
}
