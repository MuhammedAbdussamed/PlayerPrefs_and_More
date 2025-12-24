using UnityEngine;

public class Death_Sound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;

    // Bools
    private bool isDeathing;

    void Update()
    {
        PlayDeathSound();
    }

    void PlayDeathSound()
    {
        if (playerScript._isDeath && !isDeathing)
        {
            isDeathing = true;
            audioSource.PlayOneShot(deathSound);
        }
        
    }
}
