using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    public string turnBasedSceneName = "TurnBased1";
    public string enemyId = "EnemyA";

    public Animator animator;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Pembaik"))
        {
            hasTriggered = true;
            StartCoroutine(StartBattle());
        }
    }

    IEnumerator StartBattle()
    {
        Debug.Log("AMBUSHED!");
        SceneTransitionManager.instance.enemyToSpawnId = enemyId;
        yield return new WaitForSeconds(0.2f);
        SceneTransitionManager.instance.TransitionToScene(turnBasedSceneName);
    }
}
