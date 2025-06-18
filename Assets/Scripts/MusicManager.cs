using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip[] tracks;            // Inspector'da atayaca��n 8 �ark�
    private AudioSource audioSource;
    private int currentIndex = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (tracks.Length == 0)
        {
            Debug.LogError("MusicManager: tracks dizisi bo�!");
            enabled = false;
            return;
        }
        PlayTrack(currentIndex);
    }

    void Update()
    {
        // Par�a bitti mi kontrol et
        if (!audioSource.isPlaying)
        {
            // Sonraki par�aya ge� ve ba�a sarmay� sa�la
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
