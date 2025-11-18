// using UnityEngine;

// public class SurfaceZone : MonoBehaviour
// {
//     public Level5BossController levelController;

//     private void OnTriggerEnter2D(Collider2D col)
//     {
//         if (!col.CompareTag("Player")) return;

//         var pickup = col.GetComponent<PlayerPickup>();
//         if (pickup == null) return;

//         GameObject held = pickup.GetHeldItem();
//         if (held == null) return;

//         // Is the held item one of our axolotl objects?
//         if (levelController != null && levelController.IsAxolotl(held))
//         {
//             levelController.OnAxolotlRescued(held);
//             pickup.ClearHeldItemReference();
//         }
//     }
// }
