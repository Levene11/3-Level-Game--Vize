3 Levelli Coin Toplama Oyunu

Ad Soyad: Beyza Keskin
Öğrenci No: 2502040018
Ders: Oyun İçin Kod Yazımı I(DOT6105) – Vize Ödevi

Oynanış Videosu: https://youtu.be/MCajWATTJFE

Oyunun Açıklaması
Unity ve C# ile yapılmış üç seviyeli coin toplama oyunudur. Oyuncu WASD tuşları ile "Player"ı oynatır. Yeşil toplar 10 puan kazandırır, kırmızı toplar 5 puan eksiltir. 
Hedef puanlar Level 1 için 30, Level 2 için 50, Level 3 için 70'tir. Yeterli puanla çıkış alanına giren oyuncu sonraki seviyeye geçer. Puan her seviyede sıfırdan başlar.

Alınan Yardımlar:
PlayerController 5.hafta gönderilen ballscript.cs kodundan yardım alınarak yazıldı.
Audio Manager için geçen hafta derste çektiğim fotoğraftan yardım aldım. Sıralamada nasıl kodları dizeceğim konusunda (başta SerializeField yazdığım yerlerde Cloudedan yardım aldım)
LevelControllerda InBuildSettingsi derste pek anlamadığımdan yaparkende şaşırdım Cloudedan manalarını sorarak istedim sonra kendim yazdım.
Level Exitten de öncedeki ödevlerden ve birazcık da Cloudedan yardım aldım

Scriptlerin Görevleri
-PlayerController: Rigidbody ve AddForce ile hareketi, zıplamayı, OnTriggerEnter ve CompareTag ile top toplamayı, puan hesabını ve puan yazısını yönetir.
-LevelController: Hedef puanı kontrol eder, SceneManager ile sahne geçişini yapar, mesajları gösterir.
-LevelExit: Çıkış alanını algılar ve oyuncunun puanını LevelController'a gönderir.
-AudioManager: Müziği ve ses efektlerini (toplama, ceza, zıplama, seviye geçişi, başarısızlık) çalar.

Sahne Geçişi Algoritması
1. Oyuncu çıkış alanına (trigger) girer.
2. CompareTag("Player") ile giren nesnenin oyuncu olduğu doğrulanır.
3. GetComponent ile oyuncunun puanı alınır ve TryCompleteLevel çağrılır.
4. Puan hedefe eşit ya da büyükse: sıradaki sahne varsa SceneManager.LoadScene ile yüklenir, yoksa "Oyunu Tamamladınız!" yazılır.
5. Puan yetersizse sahne değişmez, "Yetersiz puan!" mesajı 2 saniye gösterilir.

Karşılaşılan Hata ve Çözümü
1.ScoreText atanmamıştı:Puan artmıyor, top yok olmuyordu. Console'da UpdateScoreUi içinde NullReferenceException vardı. scoreText Inspector'da atanmadığı için metot yarıda 
kesiliyordu.Inspector'dan atanarak çözüldü.
2. Input ayarı: Kod eski Input.GetAxisRaw kullanırken Active Input Handling "New" seçiliydi. Project Settings → Player bölümünde "Both" yapılarak çözüldü.
3. Target Score: Level 2 ve 3'ün hedef puanını Inspector'daki [SerializeField] alanı yerine script içinden değiştirmiştim, bu doğru yöntem değildi.
Hedef puan her sahnede Inspector'dan ayarlandı.


Nasıl Çalıştırılır
1. Projeyi Unity 6 ile açın.
2. Assets klasöründeki Level 1 sahnesini açın (Build Profiles'ta sıra Level 1, 2, 3'tür).
3. Play tuşuna basın.

Kontroller: WASD ile hareket, Space ile zıplama.
