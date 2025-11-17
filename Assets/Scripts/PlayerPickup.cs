using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform itemSlot;
    
    // --- NEW UI CODE ---
    // Assign your UI slot object to this in the Inspector
    public InventorySlot uiSlot; 
    
    private GameObject heldItem;
    private GameObject itemInRange;

    // --- (OnTriggerEnter2D and OnTriggerExit2D are unchanged) ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            itemInRange = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == itemInRange)
        {
            itemInRange = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemInRange != null)
            {
                if (heldItem != null)
                {
                    DropCurrentItem();
                }
                PickUpItem(itemInRange);
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && heldItem != null)
        {
            DropCurrentItem();
        }
    }

    void PickUpItem(GameObject itemToPickUp)
    {
        Debug.Log("Player picked up: " + itemToPickUp.name);
        heldItem = itemToPickUp;
        
        itemToPickUp.transform.SetParent(itemSlot);
        itemToPickUp.transform.localPosition = Vector3.zero;
        itemToPickUp.transform.localRotation = Quaternion.identity;
        
        Collider2D col = itemToPickUp.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false; 
        }
        
        itemInRange = null;
        
        // --- NEW UI CODE ---
        // Get the ItemData script from the object we just picked up
        ItemData data = itemToPickUp.GetComponent<ItemData>();
        
        // Check if we found the data and our UI slot is assigned
        if (data != null && uiSlot != null)
        {
            // Set the icon in the UI slot
            uiSlot.SetIcon(data.itemIcon);
        }
    }

    void DropCurrentItem()
    {
        // Un-parent the item
        heldItem.transform.SetParent(null);

        Collider2D col = heldItem.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;
            col.isTrigger = true;
        }
        
        heldItem.tag = "Item"; 
        heldItem = null;

        // --- NEW UI CODE ---
        // Clear the UI slot since we're no longer holding anything
        if (uiSlot != null)
        {
            uiSlot.ClearSlot();
        }
    }

    public GameObject StealItem()
    {
        if (heldItem == null)
        {
            return null; // Nothing to steal
        }

        Debug.Log(heldItem.name + " was stolen from player!");
        
        // --- NEW UI CODE ---
        // Clear the UI slot because the fish stole the item
        if (uiSlot != null)
        {
            uiSlot.ClearSlot();
        }

        GameObject stolenItem = heldItem;
        heldItem = null;
        return stolenItem;
    }
}