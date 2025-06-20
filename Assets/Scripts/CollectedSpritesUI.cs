using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectedSpritesUI : MonoBehaviour
{
    public Image[] stoneImages; // Drag 3 Image UI (batuA, batuB, batuC)
    public Sprite batuA, batuB, batuC;

    private HashSet<Sprite> collectedSet = new HashSet<Sprite>();

    public void Collect(Sprite sprite)
    {
        collectedSet.Add(sprite);
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Image img in stoneImages)
        {
            img.enabled = collectedSet.Contains(img.sprite);
        }
    }
}