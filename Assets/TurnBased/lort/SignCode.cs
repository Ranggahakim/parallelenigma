using UnityEngine;
using UnityEngine.Events;

public class SignCode : MonoBehaviour
{
    public UnityEvent playerMasuk;
    public UnityEvent playerKeluar;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Masuk");
        if (collision.CompareTag("Pembaik"))
        {
            Debug.Log("Player");
            playerMasuk.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pembaik"))
        {
            playerKeluar.Invoke();
        }
    }
}
