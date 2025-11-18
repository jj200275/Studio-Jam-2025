using UnityEngine;

public class exitScript : MonoBehaviour
{
    public LevelEndScript levelEndScript;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            if (!Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("SCENE END");
                // levelEndScript.StartLevelEnd();
            }
        }
    }
}
