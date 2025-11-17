using UnityEngine;

public class exitScript : MonoBehaviour
{
    public LevelEndScript levelEndScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("SCEHE END");
            // levelEndScript.StartLevelEnd();
        }
    }
}
