using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Header("UI Paneller")]
    [SerializeField]public GameObject inventoryPanel;   // 1’e bastýðýnda açýlan panel
    [SerializeField]public GameObject storyPanel;       // 2’ye bastýðýnda açýlan panel

    [Header("Kaðýt Sesi")]
    public AudioClip paperSound;        // Inspector’a sürükleyeceðin ses dosyasý
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource bileþeni ekle (ya da Inspector’dan hazýr koyabilirsin)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TogglePanel(inventoryPanel);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            TogglePanel(storyPanel);
    }

    void TogglePanel(GameObject panel)
    {
        bool willOpen = !panel.activeSelf;
        panel.SetActive(willOpen);

        if (willOpen)
            audioSource.PlayOneShot(paperSound);
    }
}
