using UnityEngine;

public class SignBehavior : MonoBehaviour
{
    public GameObject canvasToShow; // Canvas berisi RawImage dan Text TMP

    private void Start()
    {
        if (canvasToShow != null)
            canvasToShow.SetActive(false); // Sembunyikan di awal
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pembaik")) // pastikan Player punya tag "Player"
        {
            if (canvasToShow != null)
                canvasToShow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pembaik"))
        {
            if (canvasToShow != null)
                canvasToShow.SetActive(false);
        }
    }
}
