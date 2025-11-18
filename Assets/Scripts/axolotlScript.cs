using UnityEngine;

public class axolotlScript : MonoBehaviour
{
    public bool followPlayer;
    void Start()
    {
        followPlayer = false;
    }

    private void OnColliderEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            followPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!followPlayer) return;


    }
}
