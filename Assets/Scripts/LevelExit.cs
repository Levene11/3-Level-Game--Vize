using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private LevelController levelController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            levelController.TryCompleteLevel(player.Score);
        }
    }


}

//Bu script’in LevelController’a ulaşmasının sebebi çünkü leveli bitirme kararını o veriyor. Tür LevelController çünkü o script’in TryCompleteLevel 
//fonksiyonunu çağıracağız.
//OnTriggerEnter ile Exit kübünün içine girdiğinde kodun tetiklenmesini sağlıyoruz. Şayet Player taglı kişi Bu kodu tetiklerse Exit kübüne girerek.
//Koddan PlayerController Scriptine ulaşıyoruz çünkü Score u oraya yazdık.
//LevelController.TryCompleteLevel la ise LevelController’ın fonksiyonunu çağırıp oyuncunun şu anki puanını veriyoruz. Puana bakıp geçip geçmeyeceğine
//karar vermek LevelController’ın işi.