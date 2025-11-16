using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform itemSlot;
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
        
        // --- !!! KEY CHANGES HERE !!! ---
        
        // 1. Get the collider
        Collider2D col = itemToPickUp.GetComponent<Collider2D>();
        if (col != null)
        {
            // 2. Keep it enabled, but make it a TRIGGER
            //    This lets the NPC detect it
            col.enabled = true;
            col.isTrigger = true; 
        }

        // 3. Change the tag
        itemToPickUp.tag = "HeldItem"; 

        itemInRange = null;
    }

    void DropCurrentItem()
    {
        // Un-parent the item
        heldItem.transform.SetParent(null);

        // --- !!! KEY CHANGES HERE !!! ---
        
        // 1. Get the collider
        Collider2D col = heldItem.GetComponent<Collider2D>();
        if (col != null)
        {
            // 2. Re-enable it as a solid object
            col.enabled = true;
            col.isTrigger = true;
        }

        // 3. Change the tag back
        heldItem.tag = "Item";

        heldItem = null;
    }

    // --- !!! NEW FUNCTION FOR NPC !!! ---
    // This function lets an outside script (the NPC)
    // take the item from the player.
    public GameObject StealItem()
    {
        if (heldItem == null)
        {
            return null; // Nothing to steal
        }

        Debug.Log(heldItem.name + " was stolen from player!");

        // Get a reference to the item
        GameObject stolenItem = heldItem;
        
        // Clear the player's hand
        heldItem = null;

        // Return the stolen item to the NPC
        return stolenItem;
    }
}