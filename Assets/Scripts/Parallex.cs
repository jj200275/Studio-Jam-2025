using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Renamed 'startPos' to 'startPosX' for clarity
    private float startPosX;
    // NEW: Added a variable for the Y start position
    private float startPosY;

    // Renamed 'length' to 'lengthX' for clarity
    private float lengthX;
    // NEW: Added a variable for the sprite's height
    private float heightY;

    public GameObject cam;

    // Renamed 'parallexEffect' to 'parallaxEffectX' (fixed spelling)
    public float parallaxEffectX;
    // NEW: Added a separate effect for Y-axis
    public float parallaxEffectY; 

    void Start()
    {
        // Store the starting X and Y positions
        startPosX = transform.position.x;
        startPosY = transform.position.y; // NEW

        if (TryGetComponent<SpriteRenderer>(out var sr))
        {
            // Get the width and height of the sprite
            lengthX = sr.bounds.size.x;
            heightY = sr.bounds.size.y; // NEW
        }
        else if (TryGetComponent<ParticleSystemRenderer>(out var psr))
        {
            // Get the width and height of the particle system bounds
            lengthX = psr.bounds.size.x;
            heightY = psr.bounds.size.y; // NEW
        }
    }

    void FixedUpdate()
    {
        // Calculate horizontal distance and movement
        float distanceX = cam.transform.position.x * parallaxEffectX;
        float movementX = cam.transform.position.x * (1 - parallaxEffectX);
        
        // NEW: Calculate vertical distance and movement
        float distanceY = cam.transform.position.y * parallaxEffectY;
        float movementY = cam.transform.position.y * (1 - parallaxEffectY);

        // NEW: Update the position on both X and Y axes
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        // --- Horizontal (X-axis) Looping ---
        // This is your original code, just using the new variable names
        if (movementX > startPosX + lengthX)
        {
            startPosX += lengthX;
        }
        else if (movementX < startPosX - lengthX)
        {
            startPosX -= lengthX;
        }
        
        // --- NEW: Vertical (Y-axis) Looping ---
        // This mirrors the horizontal logic, but for the Y-axis
        if (movementY > startPosY + heightY)
        {
            startPosY += heightY;
        }
        else if (movementY < startPosY - heightY)
        {
            startPosY -= heightY;
        }
    }
}