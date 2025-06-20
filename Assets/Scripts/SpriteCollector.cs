using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpriteCollector : MonoBehaviour
{
    public static SpriteCollector instance;

    public List<Image> uiSlots; // Diisi manual dari Inspector
    private int nextSlotIndex = 0;
    public DoorController door;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Pastikan semua slot disiapkan dalam keadaan tidak aktif
        foreach (var slot in uiSlots)
        {
            if (slot != null)
            {
                slot.sprite = null;
                slot.enabled = false;                 // Sembunyikan gambar
                slot.gameObject.SetActive(false);     // Sembunyikan slot secara keseluruhan
            }
        }

        nextSlotIndex = 0;
    }

    public void CollectSprite(Sprite sprite)
    {
        // Cari slot kosong pertama
        for (int i = 0; i < uiSlots.Count; i++)
        {
            if (uiSlots[i].sprite == null)
            {
                Image slot = uiSlots[i];
                slot.gameObject.SetActive(true);  // Tampilkan slot
                slot.sprite = sprite;             // Masukkan sprite
                slot.enabled = true;              // Tampilkan gambar
                break;
            }
        }

        // Cek apakah semua slot sudah terisi
        if (AllSlotsFilled())
        {
            door.OpenDoor();
            Debug.Log("Door Open");
        }
    }

    private bool AllSlotsFilled()
    {
        foreach (var slot in uiSlots)
        {
            if (slot.sprite == null)
                return false;
        }
        return true;
    }

}
