using UnityEngine;

[ExecuteAlways]
public class FixedIsometricCamera : MonoBehaviour
{
    [Tooltip("Takip edilecek gemi")]
    public Transform target;

    [Header("Pozisyon Ofseti (World Space)")]
    [Tooltip("�r: (-10, 10, -10)")]
    public Vector3 offset = new Vector3(-10f, 10f, -10f);

    [Header("Sabit Kamera A��s� (Euler)")]
    [Tooltip("X = e�im, Y = y�n, Z = sabit kalacaksa 0")]
    public Vector3 rotationEuler = new Vector3(30f, 45f, 0f);

    [Header("Takip Yumu�akl��� (saniye)")]
    [Tooltip("0 = an�nda, 0.1�0.3 aras� genelde yumu�ak")]
    public float smoothTime = 0.2f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        // 1) Hedef pozisyonu hesapla
        Vector3 desiredPos = target.position + offset;

        // 2) Yumu�ak�a (veya an�nda) kamera pozisyonunu g�ncelle
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            smoothTime
        );

        // 3) Rotasyonu sabit Euler a��lar�na kilitle
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}
