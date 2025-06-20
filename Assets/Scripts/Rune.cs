using Unity.VisualScripting;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public Sprite sprite;
    Sound sound;

    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("Audio").GetComponent<Sound>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pembaik"))
        {
            sound.PlaySFX(sound.sfxCollectItem);
            SpriteCollector.instance.CollectSprite(sprite);
            Debug.Log("destroy");

            Destroy(gameObject);
        }
    }
}
