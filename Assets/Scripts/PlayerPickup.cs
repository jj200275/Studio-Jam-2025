using UnityEngine;

// --- NEW AUDIO CODE ---
// This automatically adds an AudioSource component to your GameObject
// if you don't already have one. It's good practice.
[RequireComponent(typeof(AudioSource))]
public class PlayerPickup : MonoBehaviour
{
    public Transform itemSlot;

    // --- NEW UI CODE ---
    public InventorySlot uiSlot;

    // --- NEW AUDIO CODE ---
    // Create a public slot in the Inspector to drag your audio clip into
    public AudioClip pickupSound;
    public AudioClip stealSound;
    // Private variable to hold the AudioSource component
    private AudioSource audioSource;

    private GameObject heldItem;
    private GameObject itemInRange;

    // --- NEW AUDIO CODE ---
    // Get the AudioSource component reference when the script wakes up
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        // --- NEW AUDIO CODE ---
        // Check if you've assigned a sound clip before trying to play it
        if (pickupSound != null)
        {
            // Play the assigned clip once
            audioSource.PlayOneShot(pickupSound);
        }

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
        ItemData data = itemToPickUp.GetComponent<ItemData>();

        if (data != null && uiSlot != null)
        {
            uiSlot.SetIcon(data.itemIcon);
        }

        PlayerStats.ApplyItemStats(data);
    }

    void DropCurrentItem()
    {
        // (This function is unchanged)
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
        if (uiSlot != null)
        {
            uiSlot.ClearSlot();
        }
        
    }

    public GameObject StealItem()
    {
        // (This function is unchanged)
        if (heldItem == null)
        {
            return null;
        }

        // --- NEW AUDIO CODE ---
        // Check if you've assigned a sound clip before trying to play it
        if (pickupSound != null)
        {
            // Play the assigned clip once
            audioSource.PlayOneShot(stealSound);
        }
        Debug.Log(heldItem.name + " was stolen from player!");

        // --- NEW UI CODE ---
        if (uiSlot != null)
        {
            uiSlot.ClearSlot();
        }

        GameObject stolenItem = heldItem;
        heldItem = null;
        return stolenItem;
    }
}