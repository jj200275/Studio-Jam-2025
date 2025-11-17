using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
