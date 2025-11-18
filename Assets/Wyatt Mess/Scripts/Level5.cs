// using UnityEngine;

// public class Level5BossController : MonoBehaviour
// {
//     [Header("Axolotls (assign the 3 item objects here)")]
//     public GameObject[] axolotlItems;   // Axolotl_1, Axolotl_2, Axolotl_3

//     private int rescuedCount = 0;

//     [Header("Air / Exhaustion System")]
//     // HOOK HERE: change this to air script type
//     public PlayerAirManager airManager;

//     public int totalAxolotls => axolotlItems != null ? axolotlItems.Length : 0;

//     public void OnAxolotlRescued(GameObject axolotl)
//     {
//         rescuedCount++;
//         Debug.Log($"Rescued {rescuedCount}/{totalAxolotls}");

//         // Hide or disable the axolotl item
//         axolotl.SetActive(false);

//         if (rescuedCount >= totalAxolotls)
//         {
//             OnAllRescued();
//         }
//     }

//     void OnAllRescued()
//     {
//         Debug.Log("All axolotls rescued! Level complete (passed out from exhaustion, not death).");
//         // TODO:
//         //  - Stop fish & scientist behavior
//         //  - Show UI / transition
//         //  - Load next scene if needed
//     }

//     // Scientist: insta “death” → huge air loss ( -10 bubbles/ all bubbles)
//     public void ScientistBigHit()
//     {
//         if (airManager != null)
//         {
//             airManager.LoseAir(10);   // HOOK: adapt to your air method name
//         }
//     }

//     // Fish 3: small hit → -1 bubble
//     public void FishSmallHit()
//     {
//         if (airManager != null)
//         {
//             airManager.LoseAir(1);    // HOOK: adapt to your air method name
//         }
//     }
// }

