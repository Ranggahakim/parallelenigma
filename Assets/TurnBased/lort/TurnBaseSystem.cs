using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TurnBaseSystem : MonoBehaviour
{
    [Header("Character")]
    public TurnBaseCharacter myCharacter;
    public TurnBaseCharacter enemy;
    [Header("Event Settings")]
    public UnityEvent ExecuteWhenStartFighting;

    [Space]
    public PertanyaanRandom[] pertanyaanRandoms;

    [Space]
    [Header("UI Duniawi")]
    public TextMeshProUGUI pertanyaan_txt;
    public TurnBaseButton[] turnBaseButtons;

    void Start()
    {
        myCharacter = GameObject.FindWithTag("Pembaik").GetComponent<TurnBaseCharacter>();
        foreach (TurnBaseButton tbb in turnBaseButtons)
        {
            tbb.myTurnBaseSystem = this;
        }
    }

    public void StartFighting()
    {
        ExecuteWhenStartFighting.Invoke();
        MunculinPertanyaan();
    }

    public void EndFighting()
    {

    }

    public void MunculinPertanyaan()
    {
        PertanyaanRandom pertanyaanSekarang = pertanyaanRandoms[Random.Range(0, pertanyaanRandoms.Length)];

        pertanyaan_txt.text = pertanyaanSekarang.pertanyaan;
        int jawaban = Random.Range(0, 4);

        int indexJawaban = 0;
        int indexOpsi = 0;
        foreach (TurnBaseButton tbb in turnBaseButtons)
        {
            if (indexJawaban == jawaban)
            {
                tbb.answertxt.text = pertanyaanSekarang.jawaban;
                tbb.isCorrectAnswer = true;
            }
            else
            {
                tbb.answertxt.text = pertanyaanSekarang.opsi[indexOpsi];
                indexOpsi++;
            }
            indexJawaban++;
        }
    }

    public void JawabanBener()
    {
        Debug.Log("Bener");
        MunculinPertanyaan();
    }

    public void JawabanSalah()
    {
        Debug.Log("Salah");
        MunculinPertanyaan();
    }
}
