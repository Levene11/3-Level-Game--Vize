using UnityEngine;
using TMPro;

// TMPro nun açılımı TextMeshPro'dur, gelişmiş bir metin işleme ve render sistemidir. Canvasta metin yazarken daha iyi oluşsun diye kullanıyoruz.


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;

    // SerializeField sağolsun ayarları koda girip değiştirmeden Inspectordan değiştirebiliyoruz. Kodu yazarken float kullanıyoruz çünk float kesirli
    //sayıları tutabiliyor. Burda floatla hareket hızımızı ve de Zıplama kuvvetimizi ayarlıyoruz.
    private Rigidbody rb;
    private bool isGrounded = false;

    private float horizontal;
    private float vertical;
    public int Score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    //Rigibodyi rb diye adlandırarak işimiz kolaylatıyoruz böylece her yre Rigidbody yerine rb yazıp geçebiliriz. Bool 2 ee durumu kontrol eden bir değişken
    //genellikle true or false da kullanıyoruz başka kullanış şekli varsa da henüz bilmiyor/hatırlamıyorum. Yerde mi değilmi kontrol ediyoruz bunu zıplama değişkeninde kullanıcaz.
    //float horizontal ve vertical sağ-sol ve ileri geri hareket girişini tutar. GetAxisRawda kullanıyoruz. Public int Score da oyuncunun scoreunu gösteriyor.
    //TextMeshProUGUI yi de UI'da kullanıyoruz fakat TMPro olmazsa çalışmiyi.


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Starttan önce sadece 1 kere çalışır. GetComponent biraz maliyetli bir işlem. Bunu her karede (Update içinde) yapmak yerine bir kere yapıp
    //sonucu rb değişkeninde saklıyoruz.

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    //InPut.GetAxisRaw koyarak Unity dünyasını bir uzaya alanı gibi düşünürsek bu gezegendeki uzayın ufuk ve tepe noktalarını alarak x ve z bileşeşinimizi
    //buna göre belirliyoruz. If le de Space tuşuna bastığında Player yerde mi değilmi kontrol etmesini istiyoruz yerdeyse Zıplamasını istiyoruz.

    private void FixedUpdate()
    {
        Movement();
    }

    //FixedUpdate de yazma sebebimiz Unitynin özellikle fizikle alakalı durumları FixedUpdate de daha iyi yapmasından dolayı.
    //Ve saniyede sahne değişirken Hareket updatelenicek. 

    private void Movement()
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        direction = direction.normalized;
        rb.AddForce(direction * moveSpeed);
    }

    // Vector3 3D uzayda x y ve z bileşenlerini alır.Movement() fonksiyonu, horizontal ve vertical giriş değerlerinden bir yön vektörü oluşturur,
    // bunu normalize eder (çapraz giderken hızlanmayı önlemek için) ve rb.AddForce ile karaktere o yönde moveSpeed kadar kuvvet uygulayıp hareket
    // ettirir.

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGrounded = false;
        print("Zıpladın!");
        AudioManager.Instance.PlayJump();
    }

    //Rigidbody’ye bir kuvvet uygular. İçindeki Vector3.up kuvvetin yönünü (yukarı) belirler, * jumpForce ise o kuvvetin ne kadar güçlü olacağını
    //yani zıplama yüksekliğini ayarlar. ForceMode.Impulse ise kuvvetin tek seferlik, anlık bir itiş olarak verilmesini sağlar, bu yüzden zıplama
    //için kullanılır.

    private void UpdateScoreUi()
    {
        scoreText.text = "Score : " + Score.ToString();
    }

    // UpdateScoreUi()u ekrandaki puan yazısını güncellemek için kullanırız.
    // scoreText.text ekrandaki yazı bileşeninin içeriğidir, ona "Score : " metnini ve Score.ToString() ile sayıya çevrilmiş puanı ekleyerek
    // bize yeni yazıyı verir. ToString() int olan puanı yazıya çevirir, böylece iki metin birleşebilir; puan her değiştiğinde bu fonksiyonu
    // çağırmamız gerekir, yoksa ekrandaki yazı eski kalır.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            print("Yerdesin!");
        }
    }

    //OnCollisionEnterda 2 madde birbiriyle karşı gelip temasa geçtiğinde çarpışıyorlar .d O yüzden OnCollisionEnter kullanıyoruz.
    //Burda Ground Tagına sahip objelerin isGrounded yani "yer" olduğunu gösteriyoruz. Bi manada Jump kodu saolsun zıplayabileceğin yerler demek.
    //Printle de isGrounded = true yani yerdeysen konsolda Yerdesin yazıyor.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GreenBall"))
        {
            Score += 10;
            AudioManager.Instance.PlayPosCoin();
            UpdateScoreUi();
            Debug.Log("GreenBall! Score" + Score);
            Destroy(other.gameObject);
           
        }

        else if (other.CompareTag("RedBall"))
        {
            Score -= 5;
            AudioManager.Instance.PlayNegCoin();
            UpdateScoreUi();
            Debug.Log("RedBall! Score" + Score);
            Destroy(other.gameObject);

        }
    }
    
    //On TriggerEnter da Collisionla aynı mantık tek fark Triggerda içinden geçip tetikliyorsun. Runnder oyunundaki bayrakların içinden geçmek gibi 
    //Burada da GreenBall ve RedBall taglerini kontrol ediyoruz. Greenball ise 10 puan ekle Score a AudioManager arkada ses çalsın
    //UpdateScoreUi ile Scoreu yenile ve Consolda GreenBall! Score! yazsın daha sonra ise gameobject yok olsun diye Destroy yazmışız
    //şayet GreenBall deilse de RedBall'a  geçmişiz ve benzer bi işlemi orda da uygulamışız.

}

