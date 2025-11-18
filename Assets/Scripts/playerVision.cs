using UnityEngine;

public class playerVision : MonoBehaviour
{
    public GameObject visionFilter;
    private void Start()
    {
        float scaler = 2 + directorScript.playerVision / 5;
        visionFilter.transform.localScale = new Vector3(scaler, scaler, scaler);
    }
}
