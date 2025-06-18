using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SingleMaterialCollectionZone : MonoBehaviour
{
    [Header("Collectible Materials")]
    [SerializeField]
    private string[] materialsToCollect =
        { "Fish", "Moss", "Jellyfish", "Starfish", "Tooth", "IceCube" };

    [Header("Timing Settings")]
    [SerializeField] private float collectionDuration = 10f;
    [SerializeField] private float zoneLifetime = 60f;

    private string selectedMaterial;
    private float collectionTimer;
    private float existenceTimer;
    private bool isPlayerInside;
    private ZoneManager zoneManager;

    private void Awake()
    {
        // 1) ZoneManager referansını yakala
        zoneManager = FindObjectOfType<ZoneManager>();

        // 2) Collider’ı trigger yap
        var col = GetComponent<Collider>();
        col.isTrigger = true;

        // 3) Kinematik Rigidbody ekle (fizik engine tetiksin)
        if (GetComponent<Rigidbody>() == null)
        {
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        // 4) Rastgele materyal seç (fallback)
        selectedMaterial = materialsToCollect[
            Random.Range(0, materialsToCollect.Length)
        ];
        Debug.Log($"[Zone] Initialized with material: {selectedMaterial}");
    }

    private void OnEnable()
    {
        // Her spawn veya yeniden aktifleşmede sıfırla
        existenceTimer = 0f;
        collectionTimer = 0f;
        isPlayerInside = false;
    }

    private void Update()
    {
        existenceTimer += Time.deltaTime;

        // 60s dolunca (player içeride değilse) sil
        if (existenceTimer >= zoneLifetime && !isPlayerInside)
        {
            zoneManager.ZoneCollected(this);
            return;
        }

        if (isPlayerInside)
        {
            collectionTimer += Time.deltaTime;

            // Her saniye 1 item ekle
            if (collectionTimer >= 1f)
            {
                collectionTimer -= 1f;
                PlayerInventory.Instance.AddItem(selectedMaterial, 1);
                Debug.Log($"[Zone] Added 1×{selectedMaterial} → Total now: {PlayerInventory.Instance.GetItemCount(selectedMaterial)}");
            }

            // 10s toplayınca sil
            if (existenceTimer >= collectionDuration)
            {
                zoneManager.ZoneCollected(this);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Zone] OnTriggerEnter by {other.name}, tag={other.tag}");
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            existenceTimer = 0f;
            collectionTimer = 0f;
            Debug.Log("[Zone] Player started collection");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[Zone] OnTriggerExit by {other.name}");
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            Debug.Log("[Zone] Player stopped collection");
        }
    }
}
