using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button exitButton;

    void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneTransitionManager.instance.TransitionToScene("Platformer1");
        });

        exitButton.onClick.AddListener(() =>
        {
            SceneTransitionManager.instance.ExitGame();
        });
    }
}
