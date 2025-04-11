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

        // E�er boss'a yakla��rsak, kameray� sabitlemek i�in offset'i de�i�tirelim
        if (distance < triggerDistance)
        {
            // Kameray� sabitlemek i�in m_FollowOffset de�erini de�i�tiriyoruz
            bossVirtualCam.Priority = 100;
            BossHealth.SetActive(true);
            BossPosture.SetActive(true);
            Boss.SetActive(true);
        }
        else
        {
            // E�er boss'tan uzakla��rsak, sabitlemeyi kald�r�yoruz
            bossVirtualCam.Priority = 0;  // Varsay�lan takip mesafesi
            BossHealth.SetActive(false);
            BossPosture.SetActive(false);

        }
    }


    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        bossVirtualCam.Priority = 100; // Ana kameradan daha y�ksek priority ver
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        bossVirtualCam.Priority = 0; // Eski kameraya d�n
    //    }
    //}
}
