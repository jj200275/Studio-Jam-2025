using UnityEngine;

public class FishAlertReceiver : MonoBehaviour
{
    public FishChaser chaser;  // assign in Inspector

    public void SetAlert(bool alert)
    {
        if (chaser == null) return;

        chaser.enabled = alert;
    }
}
