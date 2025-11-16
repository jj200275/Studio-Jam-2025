using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    void Start()
    {
        ClearSlot();   // start with no sprite
    }

    public void SetIcon(Sprite sprite)  // call in player pickup code
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    public void ClearSlot()  // call in player pickup code
    {
        icon.enabled = false;  // hide the icon entirely
        icon.sprite = null;    // make sure no old image stays
    }
}

// public InventorySlot uiSlot;

// ItemData data = itemToPickUp.GetComponent<ItemData>();
//     if (data != null)
//     {
//         uiSlot.SetIcon(data.itemIcon);
//     }

// uiSlot.ClearSlot();

    