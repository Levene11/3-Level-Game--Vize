using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

//Levelleri Level 1, 2 3 diye sıralamak ve belli bi sırada çalışmaları için SceneManager kullanıyoruz.Tekrardan TMPro'yu Canvastaki Score için kullanıyoruz.
//System.Collecitonu kullanma sebebimiz IEnumerator türünün buradan gelmesi. ClearMessage() ve LoadNextLevel() gibi coroutine fonksiyonları IEnumerator 
//döndürdüğü için gereklidir. 

public class LevelController : MonoBehaviour
{
    [SerializeField] private int targetScore = 30;
    [SerializeField] private TextMeshProUGUI messageText;

    private bool isLoading = false;

    public void TryCompleteLevel(int currentScore)
    {

        if (isLoading) return;
        
        if (currentScore >= targetScore)
        {
            AudioManager.Instance.PlayLevelComplete();
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                isLoading = true;
                StartCoroutine(LoadNextLevel(nextScene));

            }

            else
            {
                messageText.text = "Oyun Tamamlandı!";

            }

        }

        else
        {
            AudioManager.Instance.PlayLevelFail();
            ShowTimedMessage("Yetersiz Puan! Hedef: " + targetScore);

        }

    }

    private void ShowTimedMessage(string message)
    {
        StopAllCoroutines();
        messageText.text = message;
        StartCoroutine(ClearMessage());
    }

    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2f);
        messageText.text = "";
    }

    private IEnumerator LoadNextLevel(int SceneIndex)
    {
        messageText.text = "Level Tamamlandı!";
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneIndex);

    }
}

//int'i puanımızın tam sayı olmasını istediğimiz için koyuyoruz. SerializeFieldi de her levelde farklı bir target score koymak ve bunu kontrol etmek için koyuyoruz
//bi altındaki kodu ise Ekrana “Level Tamamlandı!” gibi mesaj yazabilmek için, TextMeshProUGUI ise türü, UI’daki TextMeshPro yazısına bağlanmak için yazıyoruz.
//boolu yükleniyor mu yüklenmiyor mu kontrol etmek için koyuyoruz. Çünkü Oyuncunun bitiş noktasında durup fonksiyonu üst üste tetiklemesini ve leveli 2 kere
//yüklemesini istemiyoruz.
//int currentScore parametresini fonksiyonun oyuncunun şu anki puanını bilmesi için kullanıyoruz. Çift yüklenmeyi if (isLoading) return; engelliyor.
//if currentScore => targetScore puan hedefe ulaşıldı mı diye bakıyor. Ulaşıldıysa Instancela LevelComplete sesi çalıyor.
//int nextScene = SceneManager.GetActiveScene().buildIndex + 1; kodunu Şu anki sahnenin numarasına 1 ekleyip sıradaki sahneye geçmek için kullanıyoruz.
//if nextScene < SceneManager koduyla Sonraki sahne gerçekten var mı kontrol ediyoruz. Toplam sahne sayısından küçükse var demektir. 
//Bu kontrol olmasa, son level’da olmayan bir sahneyi yüklemeye çalışıp hata alırdık.
//Yükleme başladığından isLoadingi True yapıyoruz. Coroutine başlatıyoruz çünkü yüklemeden önce 1.5 saniye beklemek istiyoruz, normal fonksiyonla bekleme yapılamaz.
//messageText ile Oyun Tamamlandı yazıyoruz. Şayet puan yetmediyse AudioManager.Instance dan Fail sesi çalıyor, Puanın yetmediği ve Hedef skor söyleniyor.
//ShowTimed ile mesaj belli bi süre kalıp yok oluyor.private void ShowTimedMessage(string message)
//Mesajı bir süre gösterip silmek için ayrı fonksiyon yaptık (Ben ve Cloude çünkü Yetersiz Puan yazısının orda hep kalması beni rahatsız etti)
//çünkü birden fazla yerde kullanılabilir. string message parametresi, hangi yazının gösterileceğini dışarıdan alabilmek için var.
//StopAllCoroutines(); ise Eski mesajın silme sayacı varsa iptal eder, yoksa yeni mesajı erken siler.
//private IEnumerator ClearMessage() kodundaysa IEnumerator var çünkü bu bir coroutine, yani içinde bekleme (yield return) var. 
//WaitForSeconds(2f) ile 2 saniye bekleyip yazıyı boşaltır. 2f çünkü saniye değeri float.
//private IEnumerator LoadNextLevel(int SceneIndex) nda yine coroutine kullanıyoruz çünkü “Level Tamamlandı!” yazısını oyuncu görsün diye 1.5 saniye
//bekleyip sonra sahneyi yüklüyor. int SceneIndex parametresi hangi sahnenin yükleneceğini söylüyor. 1.5f yine float çünkü saniye cinsinden.
