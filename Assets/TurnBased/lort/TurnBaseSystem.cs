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
    public UnityEvent ExecuteWhenWinning;
    public UnityEvent ExecuteWhenLosing;

    [Space]
    public PertanyaanRandom[] pertanyaanRandoms;

    [Space]
    [Header("UI Duniawi")]

    public TextMeshProUGUI playerHealth_txt;
    public TextMeshProUGUI enemyHealth_txt;

    [Space]

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

        Time.timeScale = 0;

        playerHealth_txt.text = $"player : {myCharacter.int_hp}";
        enemyHealth_txt.text = $"enemy : {enemy.int_hp}";

        ExecuteWhenStartFighting.Invoke();

        MunculinPertanyaan();

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
                tbb.isCorrectAnswer = false;
                indexOpsi++;
            }
            indexJawaban++;
        }
    }

    public void JawabanBener()
    {
        Debug.Log("Bener");

        myCharacter.AttackTarget(enemy);
        enemyHealth_txt.text = $"enemy : {enemy.int_hp}";

        if (enemy.int_hp <= 0)
        {
            Destroy(enemy.gameObject);
            Time.timeScale = 1;
            ExecuteWhenWinning.Invoke();
        }
        else
        {
            MunculinPertanyaan();
        }

    }

    public void JawabanSalah()
    {
        Debug.Log("Salah");

        enemy.AttackTarget(myCharacter);
        playerHealth_txt.text = $"player : {myCharacter.int_hp}";

        if (myCharacter.int_hp <= 0)
        {
            ExecuteWhenLosing.Invoke();
        }
        else
        {
            MunculinPertanyaan();
        }
    }
}
