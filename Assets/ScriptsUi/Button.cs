using UnityEngine;

public class Button : MonoBehaviour
{
    public SceneTransitionManager sceneTransition;

    public void OnPlayButtonPressed()
    {
        if (SceneTransitionManager.instance != null)
        {
            SceneTransitionManager.instance.TransitionToScene("Platformer1");
        }
    }
}
