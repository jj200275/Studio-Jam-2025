using UnityEngine;

public class exitScript : MonoBehaviour
{
    // 1. Create a public slot for your sound file
    public AudioClip exitSound;
    
    // 2. A private variable to hold our 'speaker'
    private AudioSource myAudioSource;


    void Start()
    {
        // 3. Find the 'speaker' (AudioSource component) on this GameObject
        myAudioSource = GetComponent<AudioSource>();

        // (Optional) Add one automatically if you forgot
        if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            // Check for spacebar press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // --- 4. PLAY THE SOUND! ---
                // PlayOneShot is perfect for single, non-looping sounds
                if (exitSound != null)
                {
                    myAudioSource.PlayOneShot(exitSound);
                }

                Debug.Log("SCENE END - Calling LevelEndScript");
                // GameManager.Instance.LevelCompleted(); // Your other logic
            }
        }
    }
}