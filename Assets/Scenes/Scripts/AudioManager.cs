using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip poscoinClip;
    [SerializeField] private AudioClip negcoinClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip levelcompleteClip;
    [SerializeField] private AudioClip levelfailClip;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayPosCoin()
    {
        sfxSource.PlayOneShot(poscoinClip);
    }

    public void PlayNegCoin()
    {
        sfxSource.PlayOneShot(negcoinClip);
    }

    public void PlayJump()
    {
        sfxSource.PlayOneShot(jumpClip);
    }

    public void PlayLevelComplete()
    {
        sfxSource.PlayOneShot(levelcompleteClip);
    }

    public void PlayLevelFail()
    {
        sfxSource.PlayOneShot(levelfailClip);
    }

}
