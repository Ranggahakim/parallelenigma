using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    public string turnBasedSceneName = "TurnBased1";
    public string enemyId = "EnemyA";

    public Animator animator;

    private bool hasTriggered = false;

    public temporaryDataForTurnBase tmpData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Pembaik"))
        {
            Debug.Log("Mulai Fight");
            hasTriggered = true;

            SetDataInTemporary();

            SceneManager.LoadScene("TurnBased1");
        }
    }

    void SetDataInTemporary()
    {
        TurnBaseCharacter thisEnemy = gameObject.GetComponent<TurnBaseCharacter>();

        tmpData.int_atkDmgEnemy = thisEnemy.int_atkDmg;
        tmpData.int_hpEnemy = thisEnemy.int_hp;
        tmpData.string_namaEnemy = thisEnemy.string_nama;
    }

    IEnumerator StartBattle()
    {
        Debug.Log("AMBUSHED!");
        SceneTransitionManager.instance.enemyToSpawnId = enemyId;
        yield return new WaitForSeconds(0.2f);
        SceneTransitionManager.instance.TransitionToScene(turnBasedSceneName);
    }
}
