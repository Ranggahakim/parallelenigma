using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    public string turnBasedSceneName = "TurnBased1";
    public string enemyId = "EnemyA";

    public Animator animator;

    private bool hasTriggered = false;

    public GameMasterCode myGm;
    public GameObject player;

    void Start()
    {
        myGm = GameObject.FindWithTag("gm").GetComponent<GameMasterCode>();
        player = GameObject.FindWithTag("Pembaik");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Pembaik"))
        {
            Debug.Log("Mulai Fight");
            hasTriggered = true;

            SetDataInTemporary();

            myGm.tmpData.losingEnemies.Add(gameObject.GetComponent<TurnBaseCharacter>().uniqueCode);
            SceneTransitionManager.instance.TransitionToScene("TurnBased1");
        }
    }


    void SetDataInTemporary()
    {
        TurnBaseCharacter thisEnemy = gameObject.GetComponent<TurnBaseCharacter>();

        myGm.SetupDataOfEnemy(thisEnemy.int_atkDmg, thisEnemy.int_hp, thisEnemy.string_nama);
        myGm.SetupDataOfPlayerLocation(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    IEnumerator StartBattle()
    {
        Debug.Log("AMBUSHED!");
        SceneTransitionManager.instance.enemyToSpawnId = enemyId;
        yield return new WaitForSeconds(0.2f);
        SceneTransitionManager.instance.TransitionToScene(turnBasedSceneName);
    }
}
