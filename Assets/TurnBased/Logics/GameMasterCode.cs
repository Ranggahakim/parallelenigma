using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameMasterCode : MonoBehaviour
{
    public temporaryDataForTurnBase tmpData;
    public GameObject player;

    public GameObject[] enemies;

    void Awake()
{
    if (tmpData == null)
    {
        Debug.LogError($"[{name}] tmpData is not assigned! Aborting Awake.");
        return;
    }

    if (!tmpData.isContinue)
        return;

    enemies = GameObject.FindGameObjectsWithTag("Enemy");

    if (tmpData.losingEnemies == null)
    {
        Debug.LogWarning("losingEnemies list is null—nothing to deactivate.");
    }
    else
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) 
                continue;

            var tbc = enemy.GetComponent<TurnBaseCharacter>();
            if (tbc == null)
            {
                Debug.LogWarning(
                  $"Enemy '{enemy.name}' has no TurnBaseCharacter script attached.");
                continue;
            }

            if (tmpData.losingEnemies.Contains(tbc.uniqueCode))
                enemy.SetActive(false);
        }
    }

    if (player != null)
    {
        player.transform.position =
          new Vector3(tmpData.px, tmpData.py, tmpData.pz);
    }
    else
    {
        Debug.LogError("Player reference not set on GameMasterCode!");
    }
}


    public void SetupDataOfEnemy(int uniqueCode, int int_atkDmg, int int_hpEnemy, string string_namaEnemy)
    {
        tmpData.uniqueCode = uniqueCode;
        tmpData.int_atkDmgEnemy = int_atkDmg;
        tmpData.int_hpEnemy = int_hpEnemy;
        tmpData.string_namaEnemy = string_namaEnemy;
    }
    void Start()

    {
        SceneTransitionManager.instance.OnSceneLoaded();
    }
    

    public void SetupDataOfPlayerLocation(float x, float y, float z)
    {
        tmpData.px = x;
        tmpData.py = y;
        tmpData.pz = z;
    }

}
