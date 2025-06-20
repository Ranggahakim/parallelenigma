using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    public string sceneToLoad;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Player pressed W near door. Loading next scene...");
            SceneManager.LoadScene(sceneToLoad); // Ganti dengan nama scene-mu
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pembaik"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pembaik"))
        {
            playerInRange = false;
        }
    }
}