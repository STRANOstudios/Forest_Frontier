using UnityEngine;

[DisallowMultipleComponent]
public class SoundManager : MonoBehaviour
{
    [Header("Sound Clips")]
    [SerializeField] private AudioClip chopSound;
    [SerializeField] private AudioClip drinkSound;
    [SerializeField] private AudioClip eatSound;

    [Header("Reference")] 
    [SerializeField] private AudioSource audioSource;

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
