using UnityEngine;

public class testMove2 : MonoBehaviour
{
    public Rigidbody2D myBody;
    public BoxCollider2D myCollider;
    public Animator myAnimator;

    [Header("Movement")]
    public float speed;
    public float sprintMultiplier = 2f;

    [Header("Stamina")]
    public StaminaScript2 staminaScript; // Assign this in the Inspector!
    public float sprintStaminaCost = 20f; // Stamina drained per second

    // Input checks
    bool up;
    bool down;
    bool left;
    bool right;
    bool shift;

    Vector2 direction;

    void Start()
    {
        sprintStaminaCost = 20f * (5 / (5 + PlayerStats.playerStamina));

        speed = 3 + PlayerStats.playerSpeed;
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Input ---
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        shift = Input.GetKey(KeyCode.LeftShift);

        // --- Movement Direction ---
        direction = Vector2.zero;

        if (up) direction += Vector2.up;
        if (down) direction += Vector2.down;
        if (left) 
        { 
            direction += Vector2.left; 
            
            // Create a temporary variable to hold the current scale
            Vector3 newScale = transform.localScale;
            
            // Set the x-component to the NEGATIVE of its absolute value
            // This takes (2, 2, 2) and makes it (-2, 2, 2)
            newScale.x = -Mathf.Abs(newScale.x); 
            
            // Apply the new scale
            transform.localScale = newScale; 
        }
        if (right) 
        { 
            direction += Vector2.right; 
            
            // Create a temporary variable to hold the current scale
            Vector3 newScale = transform.localScale;

            // Set the x-component to the POSITIVE of its absolute value
            // This takes (-2, 2, 2) and makes it (2, 2, 2)
            newScale.x = Mathf.Abs(newScale.x); 
            
            // Apply the new scale
            transform.localScale = newScale; 
        }

        bool amIMoving = false;
        if (direction != Vector2.zero) {
            amIMoving = true;
        }
        myAnimator.SetBool("Moving", amIMoving);


        direction.Normalize();
        myBody.linearVelocity = speed * direction;

        // --- Stamina Logic ---
        // Check if pressing shift AND we have stamina left
        if (shift && staminaScript.currentStamina > 0)
        {
            // We are sprinting
            myBody.linearVelocity *= sprintMultiplier;
            staminaScript.DecreaseStamina(sprintStaminaCost * Time.deltaTime);
        }
        // --- Regeneration logic has been removed ---

        // water gravity
        myBody.linearVelocity += new Vector2(0, -1);
    }
}