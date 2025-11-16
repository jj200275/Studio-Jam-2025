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
        
        // --- SIMPLIFIED ---
        // Just disable the collider so it doesn't get in the way.
        Collider2D col = itemToPickUp.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false; 
        }
        
        // (We removed the tag change)
        
        itemInRange = null;
    }

    void DropCurrentItem()
    {
        // Un-parent the item
        heldItem.transform.SetParent(null);

        // --- SIMPLIFIED ---
        Collider2D col = heldItem.GetComponent<Collider2D>();
        if (col != null)
        {
            // Re-enable it as a trigger so we can walk through it
            col.enabled = true;
            col.isTrigger = true;
        }

        // (We removed the tag change)
        
        heldItem.tag = "Item"; // Keep this line to make it "Item" again
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