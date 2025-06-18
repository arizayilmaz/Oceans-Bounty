using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Header("UI Paneller")]
    [SerializeField]public GameObject inventoryPanel;   // 1�e bast���nda a��lan panel
    [SerializeField]public GameObject storyPanel;       // 2�ye bast���nda a��lan panel

    [Header("Ka��t Sesi")]
    public AudioClip paperSound;        // Inspector�a s�r�kleyece�in ses dosyas�
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource bile�eni ekle (ya da Inspector�dan haz�r koyabilirsin)
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
