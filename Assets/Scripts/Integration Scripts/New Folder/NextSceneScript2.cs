using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript2 : MonoBehaviour
{
    public void PlayGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}