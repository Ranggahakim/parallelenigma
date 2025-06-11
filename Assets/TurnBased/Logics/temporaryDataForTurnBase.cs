using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "temporaryDataForTurnBase", menuName = "Scriptable Objects/temporaryDataForTurnBase")]
public class temporaryDataForTurnBase : ScriptableObject
{
    public bool isContinue;
    [Header("Player")]
    public float px;
    public float py;
    public float pz;

    [Header("For Enemy")]
    public string string_namaEnemy;
    public int int_hpEnemy;
    public int int_atkDmgEnemy;

    [Header("Enemies")]
    public List<int> losingEnemies;

    public void ResetData()
    {
        px = 0;
        py = 0;
        pz = 0;
        losingEnemies.Clear();
        isContinue = false;
    }
}