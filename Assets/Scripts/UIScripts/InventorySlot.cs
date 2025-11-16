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

    public void ClearSlot()
    {
        icon.enabled = false;  // Hide the icon entirely
        icon.sprite = null;    // Make sure no old image stays
    }
}
