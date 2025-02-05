using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eightway : MonoBehaviour
{
    public float rotationSpeed = 20f; // Dönme hýzý
    private Vector2 moveInput;

    void Update()
    {
        // **1. Input Alma (Klavye ve Gamepad)**
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A (-1) ve D (+1) veya Gamepad X Ekseni
        moveInput.y = Input.GetAxisRaw("Vertical");   // W (+1) ve S (-1) veya Gamepad Y Ekseni

        // **2. Eðer input sýfýr deðilse karakteri döndür**
        if (moveInput.sqrMagnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg; // Yönü açýya çevir
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle); // Yeni dönüþ açýsýný oluþtur
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);


        
    }


    
}
