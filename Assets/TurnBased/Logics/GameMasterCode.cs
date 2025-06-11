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
        if (tmpData.isContinue)
        {

            enemies = GameObject.FindGameObjectsWithTag("Enemy");


            foreach (GameObject enemy in enemies)
            {

                if (tmpData.losingEnemies.Contains(enemy.GetComponent<TurnBaseCharacter>().uniqueCode))
                {
                    enemy.SetActive(false);
                }
            }


            player.transform.position = new Vector3(tmpData.px, tmpData.py, tmpData.pz);
        }
    }

    public void SetupDataOfEnemy(int int_atkDmg, int int_hpEnemy, string string_namaEnemy)
    {
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
