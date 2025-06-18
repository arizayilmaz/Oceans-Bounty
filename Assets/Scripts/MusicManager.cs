using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip[] tracks;            // Inspector'da atayacaðýn 8 þarký
    private AudioSource audioSource;
    private int currentIndex = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (tracks.Length == 0)
        {
            Debug.LogError("MusicManager: tracks dizisi boþ!");
            enabled = false;
            return;
        }
        PlayTrack(currentIndex);
    }

    void Update()
    {
        // Parça bitti mi kontrol et
        if (!audioSource.isPlaying)
        {
            // Sonraki parçaya geç ve baþa sarmayý saðla
            currentIndex = (currentIndex + 1) % tracks.Length;
            PlayTrack(currentIndex);
        }
    }

    void PlayTrack(int index)
    {
        audioSource.clip = tracks[index];
        audioSource.Play();
    }
}
