using UnityEngine;

[DisallowMultipleComponent]
public class SoundManager : MonoBehaviour
{
    [Header("Sound Clips")]
    [SerializeField] private AudioClip chopSound;
    [SerializeField] private AudioClip drinkSound;
    [SerializeField] private AudioClip eatSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayChopSound()
    {
        PlaySound(chopSound);
    }

    public void PlayDrinkSound()
    {
        PlaySound(drinkSound);
    }

    public void PlayEatSound()
    {
        PlaySound(eatSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
