using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    // The "ItemSlot" to parent the held item to
    public Transform itemSlot;

    // The item we are currently holding
    private GameObject heldItem;

    // The item currently in our pickup range
    private GameObject itemInRange;

    // --- 1. Detect when an item enters our trigger range ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we collided with has the "Item" tag
        if (other.tag == "Item")
        {
            // Store this item as the one we can pick up
            itemInRange = other.gameObject;
        }
    }

    // --- 2. Detect when an item leaves our trigger range ---
    private void OnTriggerExit2D(Collider2D other)
    {
        // If the item leaving is the one we had stored, clear it
        if (other.gameObject == itemInRange)
        {
            itemInRange = null;
        }
    }

    void Update()
    {
        // --- 3. Check for the "E" key press ---
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if we are standing near an item
            if (itemInRange != null)
            {
                // Check if our inventory slot is full
                if (heldItem != null)
                {
                    // Slot is full. Drop our current item.
                    DropCurrentItem();
                }

                // Pick up the new item
                PickUpItem(itemInRange);
            }
        }

        // --- Bonus: Drop key ---
        if (Input.GetKeyDown(KeyCode.G) && heldItem != null)
        {
            DropCurrentItem();
        }
    }

    void PickUpItem(GameObject itemToPickUp)
    {
        // --- Add this line ---
        Debug.Log("Player picked up: " + itemToPickUp.name);
        
        // Store this item as our held item
        heldItem = itemToPickUp;
        
        // Parent the item to our ItemSlot
        itemToPickUp.transform.SetParent(itemSlot);
        
        // Snap it to the slot's position
        itemToPickUp.transform.localPosition = Vector3.zero;
        itemToPickUp.transform.localRotation = Quaternion.identity;
        
        // Disable its collider so we don't trigger it again
        itemToPickUp.GetComponent<Collider2D>().enabled = false;

        // Clear the item in range, since we just picked it up
        itemInRange = null;
    }

    void DropCurrentItem()
    {
        // Un-parent the item
        heldItem.transform.SetParent(null);

        // Re-enable its collider so we can pick it up again
        heldItem.GetComponent<Collider2D>().enabled = true;

        // We are no longer holding an item
        heldItem = null;
    }
}