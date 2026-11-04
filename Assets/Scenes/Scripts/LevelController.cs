using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Security;

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
