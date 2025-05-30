using UnityEngine;

[CreateAssetMenu(fileName = "PertanyaanRandom", menuName = "Scriptable Objects/PertanyaanRandom")]
public class PertanyaanRandom : ScriptableObject
{
    [TextArea] public string pertanyaan;
    [TextArea] public string jawaban;

    [TextArea] public string[] opsi = new string[3];

}
