using UnityEngine;
using Cinemachine;

public class CameraAnchorCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera bossVirtualCam;

    public Transform player;

    public GameObject BossHealth, BossPosture, Boss;

    public float triggerDistance = 15f;

    [Header("Müzik Ayarlarý")]
    public AudioSource musicSource;        // Canvas içindeki AudioSource
    public AudioClip bossMusicClip;        // Boss müziði

    private bool bossTriggered = false;    // Müziði sadece bir kez deðiþtirmek için

    private void Start()
    {
        BossHealth.SetActive(false);
        BossPosture.SetActive(false);
        Boss.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < triggerDistance)
        {
            bossVirtualCam.Priority = 100;
            BossHealth.SetActive(true);
            BossPosture.SetActive(true);
            Boss.SetActive(true);

            if (!bossTriggered)
            {
                // Müzik deðiþimi
                if (musicSource != null && bossMusicClip != null)
                {
                    musicSource.clip = bossMusicClip;
                    musicSource.Play();
                }

                bossTriggered = true; // Bir kez tetiklenmesini saðlýyoruz
            }
        }
        else
        {
            bossVirtualCam.Priority = 0;
            BossHealth.SetActive(false);
            BossPosture.SetActive(false);

            // Eðer istersen müziði burada da eski haline çevirebilirsin
        }
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

