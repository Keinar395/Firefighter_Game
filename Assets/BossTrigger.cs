using UnityEngine;
using Cinemachine;

public class CameraAnchorCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera bossVirtualCam;

    public Transform player;

    public GameObject BossHealth, BossPosture, Boss;

    public float triggerDistance = 15f;

    private void Start()
    {
        BossHealth.SetActive(false);
        BossPosture.SetActive(false);
        Boss.SetActive(false);
    }

    void Update()
    {
        // Boss'a olan mesafeyi hesapla
        float distance = Vector3.Distance(player.position, transform.position);

        // Eðer boss'a yaklaþýrsak, kamerayý sabitlemek için offset'i deðiþtirelim
        if (distance < triggerDistance)
        {
            // Kamerayý sabitlemek için m_FollowOffset deðerini deðiþtiriyoruz
            bossVirtualCam.Priority = 100;
            BossHealth.SetActive(true);
            BossPosture.SetActive(true);
            Boss.SetActive(true);
        }
        else
        {
            // Eðer boss'tan uzaklaþýrsak, sabitlemeyi kaldýrýyoruz
            bossVirtualCam.Priority = 0;  // Varsayýlan takip mesafesi
            BossHealth.SetActive(false);
            BossPosture.SetActive(false);

        }
    }


    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        bossVirtualCam.Priority = 100; // Ana kameradan daha yüksek priority ver
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        bossVirtualCam.Priority = 0; // Eski kameraya dön
    //    }
    //}
}
