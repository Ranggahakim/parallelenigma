using UnityEngine;

public class InitializedScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneTransitionManager.instance.OnSceneLoaded();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
