using UnityEngine;

public class exitScript : MonoBehaviour
{
    public AudioClip exitSound;
    public directorScript gameDirector;
    
    private AudioSource myAudioSource;
    
    // 1. --- Our new "state" variable ---
    // This flag tracks if we are in the trigger or not.
    private bool isNearLadder = false;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 2. --- Use Update() to check for input ---
    // Update() runs every single game frame, so it will 
    // never miss your spacebar press.
    void Update()
    {
        // We just check our two conditions:
        // 1. Is our flag true? (Are we near the ladder?)
        // 2. Did the player just press space?
        if (isNearLadder && Input.GetKeyDown(KeyCode.Space))
        {
            // --- 4. PLAY THE SOUND! ---
            if (exitSound != null)
            {
                myAudioSource.PlayOneShot(exitSound);
            }

            Debug.Log("SCENE END - Calling LevelEndScript");
            
            // Tell the director to handle the level change
            gameDirector.PlayerExitedLevel();
            
            // (Optional) You might want to set this back to false
            // so you can't press it multiple times.
            // isNearLadder = false; 
        }
    }


    // 3. --- Use Physics to set our state ---
    // This runs ONCE when you first enter the trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            // Set our flag to true
            isNearLadder = true;
        }
    }

    // 4. --- Use Physics to clear our state ---
    // This runs ONCE when you leave the trigger.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            // Set our flag back to false
            isNearLadder = false;
        }
    }
}