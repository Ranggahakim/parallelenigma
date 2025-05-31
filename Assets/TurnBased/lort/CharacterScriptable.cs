using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptable", menuName = "Scriptable Objects/CharacterScriptable")]
public class CharacterScriptable : ScriptableObject
{
    public string string_nama;
    public int int_hp;
    public int int_atkDmg;
    public EnemyType enemyType;

    private void OnValidate()
    {
        // Mengatur namaKarakter ke nama file Scriptable Object
#if UNITY_EDITOR // Kode di dalam blok ini hanya dijalankan di dalam Unity Editor
        string_nama = name;
#endif
    }
}

public enum EnemyType
{
    kecil,
    sedang,
    besar
}