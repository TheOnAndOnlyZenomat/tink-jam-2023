using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic1;
    public AudioClip backgroundMusic2;

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Start()
    {
        // Create two AudioSources
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        // Set the clips for each AudioSource
        audioSource1.clip = backgroundMusic1;
        audioSource2.clip = backgroundMusic2;

        // Set loop to true for both audio tracks
        audioSource1.loop = true;
        audioSource2.loop = true;

        // Play both audio tracks simultaneously
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        // Play both audio tracks simultaneously
        audioSource1.Play();
        audioSource2.Play();
    }
}