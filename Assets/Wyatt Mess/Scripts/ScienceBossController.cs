using UnityEngine;

public class ScientistBossController : MonoBehaviour
{
    public Transform player;
    public Level5BossController levelController;

    [Header("Ranges")]
    public float ignoreRadius = 10f; // too far, scientist doesn't care
    public float alertRadius = 6f;   // within this, alerts fish

    [Header("Fish")]
    public FishAlertReceiver fish1;
    public FishAlertReceiver fish2;
    public FishAlertReceiver fish3;  // the attacker

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > ignoreRadius)
        {
            SetAlert(false);
        }
        else if (dist <= alertRadius)
        {
            SetAlert(true);
        }
        else
        {
            SetAlert(false);
        }
    }

    void SetAlert(bool alert)
    {
        if (fish1 != null) fish1.SetAlert(alert);
        if (fish2 != null) fish2.SetAlert(alert);
        if (fish3 != null) fish3.SetAlert(alert);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        // insta “death” / -10 bubbles
        if (levelController != null)
        {
            levelController.ScientistBigHit();
        }
    }
}
