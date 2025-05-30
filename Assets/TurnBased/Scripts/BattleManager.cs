using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform enemySpawnPoint;
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;

    void Start()
    {
        string idEnemy = SceneTransitionManager.instance.enemyToSpawnId;

        GameObject enemyToSpawn = null;

        switch (idEnemy)
        {
            case "EnemyA":
                enemyToSpawn = enemyAPrefab;
                break;
            case "EnemyB":
                enemyToSpawn = enemyBPrefab;
                break;
            default:
                Debug.LogWarning("Unknown enemy ID: " + idEnemy);
                break;
        }


        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, enemySpawnPoint.position, Quaternion.identity);
        }
    }
}
