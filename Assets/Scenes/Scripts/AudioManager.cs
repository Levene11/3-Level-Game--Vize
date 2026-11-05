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


    //ses dosyalarını koyarken AudioSource u kaynak olarak koyuyoruz. musicSource arka plan müziği için kullanılacak iken sfxSource basitçe yapılan
    //minik davranışlarda kullanacağımız sesler için. Background müziği devamlı çalacak iken sfx lerin uygulandığı süürece basılmasını istiyoruz.
    //O yüzden bu kısımda hepsini teker teker yazıp Inspector için başına SerializeField koyuyoruz.

    //Instance Singleton denen bir tasarım kalıbında kullanılır. Amacı ise bir sınıftan sahnede sadece bir tane olmasını sağlamak ve ona her yerden
    //kolayca ulaşabilmektir. Bu yüüzden Instance kullanırız. Instance ı Awakede çalıştırıyoruz ki bi kerede çağıralım. Instance kullanırken = this oluyor
    //Açıkcası niye tam anlamış değişim sebep olarak ama this kullanılmalı. Startda ses kaynaklarını başlatıyoruz. musicSource da Play() yazmasının sebebi
    //devamlı çalacak olması iken diğerlerinde sfx oldukları için kullanıldıkları vakit bu ses dosyalarının çağrılmasını istediğimizden PlayOneShot yazıyor.



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
