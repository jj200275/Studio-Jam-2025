using UnityEngine;

public class FishChaser : MonoBehaviour
{
    [Header("Chase Settings")]
    public float chaseRange = 10f;
    public float stopDistance = 1f;
    public float moveSpeed = 3f;

    [Header("Flee Settings")]
    public float fleeDuration = 4f;   // How long to flee after stealing
    public Transform npcItemSlot;   // Where the NPC will hold the stolen item

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    
    // --- New State Variables ---
    private bool isFleeing = false;
    private float fleeTimer = 0;
    private GameObject stolenItem = null;
    private PlayerPickup playerInventory; // Reference to player's script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            player = playerObject.transform;
            // Get the player's inventory script
            playerInventory = playerObject.GetComponent<PlayerPickup>();
        }
        else
        {
            Debug.LogError("NpcChaser: Player not found!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // --- NEW STATE LOGIC ---
        if (isFleeing)
        {
            // --- FLEEING STATE ---
            fleeTimer -= Time.deltaTime;
            if (fleeTimer <= 0)
            {
                isFleeing = false;
                DropStolenItem(); // Optionally drop the item after fleeing
            }

            // Calculate flee direction (AWAY from player)
            Vector3 direction = (transform.position - player.position).normalized;
            moveDirection = new Vector2(direction.x, direction.y);
            
            FlipSprite(direction.x);
        }
        else
        {
            // --- CHASING STATE (Original Logic) ---
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= chaseRange && distanceToPlayer > stopDistance)
            {
                // Chase
                Vector3 direction = (player.position - transform.position).normalized;
                moveDirection = new Vector2(direction.x, direction.y);
                FlipSprite(direction.x);
            }
            else
            {
                // Stop
                moveDirection = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    // --- NEW: This handles the "touching" ---
    // --- UPDATED: This handles the "touching" ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we touched the "PLAYER" and we aren't already fleeing
        if (other.tag == "Player" && !isFleeing && stolenItem == null)
        {
            if (playerInventory != null)
            {
                // Call the player's StealItem() function
                stolenItem = playerInventory.StealItem();

                if (stolenItem != null)
                {
                    // --- We successfully stole it! ---
                    Debug.Log("NPC: I stole the " + stolenItem.name);

                    // Parent the item to the NPC's item slot
                    stolenItem.transform.SetParent(npcItemSlot);
                    stolenItem.transform.localPosition = Vector3.zero;

                    // Disable its collider so we don't trigger it again
                    stolenItem.GetComponent<Collider2D>().enabled = false; 

                    // --- Start Fleeing! ---
                    isFleeing = true;
                    fleeTimer = fleeDuration;
                }
            }
        }
    }

    // --- NEW: Helper function to drop the item ---
    void DropStolenItem()
    {
        if (stolenItem == null) return;

        Debug.Log("NPC: Dropping the " + stolenItem.name);

        // Un-parent
        stolenItem.transform.SetParent(null);
        
        // Re-enable its collider and set tag back to "Item"
        Collider2D col = stolenItem.GetComponent<Collider2D>();
        col.enabled = true;
        col.isTrigger = true;
        stolenItem.tag = "Item";

        stolenItem = null; // NPC is no longer holding it
    }

    // --- NEW: Helper function to flip sprite ---
    void FlipSprite(float directionX)
    {
        // Get the current local scale
        Vector3 newScale = transform.localScale;

        if (directionX < 0)
        {
            // Set the x-component to the NEGATIVE of its absolute value
            // This preserves your custom scale (e.g., 2 -> -2)
            newScale.x = -Mathf.Abs(newScale.x);
        }
        else if (directionX > 0)
        {
            // Set the x-component to the POSITIVE of its absolute value
            // This preserves your custom scale (e.g., -2 -> 2)
            newScale.x = Mathf.Abs(newScale.x);
        }
        
        // Apply the new scale
        transform.localScale = newScale;
    }
}