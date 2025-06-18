using UnityEngine;

[ExecuteAlways]
public class FixedIsometricCamera : MonoBehaviour
{
    [Tooltip("Takip edilecek gemi")]
    public Transform target;

    [Header("Pozisyon Ofseti (World Space)")]
    [Tooltip("Ör: (-10, 10, -10)")]
    public Vector3 offset = new Vector3(-10f, 10f, -10f);

    [Header("Sabit Kamera Açýsý (Euler)")]
    [Tooltip("X = eðim, Y = yön, Z = sabit kalacaksa 0")]
    public Vector3 rotationEuler = new Vector3(30f, 45f, 0f);

    [Header("Takip Yumuþaklýðý (saniye)")]
    [Tooltip("0 = anýnda, 0.1–0.3 arasý genelde yumuþak")]
    public float smoothTime = 0.2f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        // 1) Hedef pozisyonu hesapla
        Vector3 desiredPos = target.position + offset;

        // 2) Yumuþakça (veya anýnda) kamera pozisyonunu güncelle
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            smoothTime
        );

        // 3) Rotasyonu sabit Euler açýlarýna kilitle
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}
