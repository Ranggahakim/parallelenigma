using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    public Animator animator;
    public RectTransform wiper;

    private string targetScene;

    [HideInInspector] public string enemyToSpawnId;
    [HideInInspector] public string playerToSpawnId;

    void Awake()
    {
        Debug.Log($"SceneTransitionManager Awake() called on GameObject: {gameObject.name}");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"<color=green>Singleton instance created.</color>");
        }
        else
        {
            Debug.Log($"<color=orange>Duplicate instance found. Destroying self.</color>");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log($"<color=red>SceneTransitionManager OnDestroy() called on GameObject: {gameObject.name}</color>");
    }
    
    public void TransitionToScene(string sceneName)
    {
        targetScene = sceneName;

        if (wiper != null)
        {
            animator.Play("idle", 0, 0);
            animator.Update(0);

            wiper.anchoredPosition = new Vector2(-2100f, 0f);
            animator.Update(0);
        }

        animator.SetTrigger("WipeIn");
    }

    public void OnWipeInComplete()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void OnSceneLoaded()
    {
        animator.SetTrigger("WipeOut");
    }

    public void ExitGame()
    {
        Debug.Log("Exit game requested.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
