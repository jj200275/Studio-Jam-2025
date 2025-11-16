using UnityEngine;

public class testMove2 : MonoBehaviour
{
    public Rigidbody2D myBody;
    public BoxCollider2D myCollider;

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
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
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
        if (left) { direction += Vector2.left; transform.localScale = new Vector3(-1, 1, 1); }
        if (right) { direction += Vector2.right; transform.localScale = new Vector3(1, 1, 1); }

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