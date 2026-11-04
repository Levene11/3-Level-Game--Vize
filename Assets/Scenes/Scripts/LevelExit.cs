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
