using UnityEngine;

public class Eightway : MonoBehaviour
{
    public Transform weaponTransform; // Silah�n d�n��� i�in
    public Transform handsTransform;  // Ellerin d�n��� i�in
    public float rotationSpeed = 7f;

    private Vector2 moveInput;
    private float currentAngle; // �u anki a��y� sakla (Lerp i�in)

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;

            // SADECE Z ekseninde Lerp'li d�n�� (X ve Y rotasyonu 0)
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            // Karakterin Z pozisyonunu sabitle (2D'de derinlik sorunu olmas�n)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            // Sola m� bak�yor? (90�'den fazla veya -90�'den az)
            bool isFacingLeft = Mathf.Abs(currentAngle) > 90f;

            // Silah ve elleri ters �evir (Y ekseninde flip)
            if (isFacingLeft)
            {
                weaponTransform.localScale = new Vector3(1, 1, 1);
                handsTransform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                weaponTransform.localScale = Vector3.one;
                handsTransform.localScale = Vector3.one;
            }
        }
    }
}