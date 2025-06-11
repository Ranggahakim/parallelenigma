using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptable", menuName = "Scriptable Objects/CharacterScriptable")]
public class CharacterScriptable : ScriptableObject
{
    public string string_nama;
    public int int_hp;
    public int int_atkDmg;

    private void OnValidate()
    {
#if UNITY_EDITOR 
        string_nama = name;
#endif
    }
}
