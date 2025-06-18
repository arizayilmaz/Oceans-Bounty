using UnityEngine;
using TMPro;  // TextMeshPro isim uzay�

public class IslandTrigger : MonoBehaviour
{
    [Header("Handlers")]
    public BattleCrewHandler battleHandler;
    public RaidMissionHandler raidHandler;

    [Header("Island Settings")]
    public int islandPower = 300;

    [Header("Icon Settings")]
    public Sprite iconSprite;           // Yan�na gelecek ikon sprite'�
    public float iconOffsetX = 0f;     // �kon ile metin aras�ndaki yatay mesafe (negatif sola kayd�r�r)

    private bool playerInside;
    private Transform playerTransform;

    // 3D Text ve ikon i�in referanslar
    private TextMeshPro powerText;

    private void Awake()
    {
        // Ada collider��ndan y�ksekli�i al
        Collider col = GetComponent<Collider>();
        if (col == null || !col.isTrigger)
            Debug.LogWarning("IslandTrigger: Bu objede isTrigger = true olan bir Collider bulunmal�!");

        float islandHeight = col != null ? col.bounds.size.y : 1f;

        // --- Metin objesi olu�tur ---
        GameObject txtObj = new GameObject("PowerText");
        txtObj.transform.SetParent(transform);
        // Ada tepesinden +20 birim yukar�
        txtObj.transform.localPosition = new Vector3(0f, islandHeight + 15f, 0f);
        txtObj.transform.localRotation = Quaternion.identity;

        powerText = txtObj.AddComponent<TextMeshPro>();
        powerText.text = islandPower.ToString();
        powerText.fontSize = 100;                   // B�y�k metin
        // Metin rengini #DA412F hex koduna ayarla
        if (ColorUtility.TryParseHtmlString("#DA412F", out Color customColor))
            powerText.color = customColor;
        else
            powerText.color = Color.red;            // Fallback
        powerText.fontStyle = FontStyles.Bold;      // Kal�n yaz� stili
        powerText.alignment = TextAlignmentOptions.Center;

        // Billboard: metin her zaman kameraya d�ns�n
        txtObj.AddComponent<FaceCamera>();

        // --- �kon objesi olu�tur ---
        if (iconSprite != null)
        {
            GameObject iconObj = new GameObject("PowerIcon");
            iconObj.transform.SetParent(transform);
            // Ada tepesinden +20 birim yukar�, sola 7 birim ofset
            iconObj.transform.localPosition = new Vector3(iconOffsetX, islandHeight + 25f, 0f);
            iconObj.transform.localRotation = Quaternion.identity;
            // �l�e�i 2x2x2 yap
            iconObj.transform.localScale = new Vector3(2f, 2f, 2f);

            // SpriteRenderer ile ikon g�ster
            var sr = iconObj.AddComponent<SpriteRenderer>();
            sr.sprite = iconSprite;
            sr.sortingOrder = 1;

            iconObj.AddComponent<FaceCamera>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerInside && other.CompareTag("Player"))
        {
            playerInside = true;
            playerTransform = other.transform;
            battleHandler.Setup(islandPower, playerTransform);
            battleHandler.battlePanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerInside && battleHandler.HasBattleStarted)
        {
            battleHandler.battlePanel.SetActive(false);
            raidHandler.StartRaid(playerTransform, islandPower, battleHandler.GetTotalPower());
            playerInside = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerInside && other.CompareTag("Player"))
        {
            playerInside = false;
            battleHandler.CancelBattle();
            battleHandler.battlePanel.SetActive(false);
        }
    }
}

// Kameraya d�nme i�in basit billboard script'i
public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}