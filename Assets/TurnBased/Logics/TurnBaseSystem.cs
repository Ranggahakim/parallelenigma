using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TurnBaseSystem : MonoBehaviour
{
    public temporaryDataForTurnBase tmpData;
    [Header("Character")]
    public TurnBaseCharacter myCharacter;
    public TurnBaseCharacter enemy;
    [Header("Event Settings")]
    public UnityEvent ExecuteWhenStartFighting;
    public UnityEvent ExecuteWhenWinning;
    //public UnityEvent ExecuteWhenLosing;

    [Space]
    public PertanyaanRandom[] pertanyaanRandoms;

    [Space]
    [Header("UI Duniawi")]

    public TextMeshProUGUI playerHealth_txt;
    public TextMeshProUGUI enemyHealth_txt;

    [Space]

    public TextMeshProUGUI pertanyaan_txt;
    public TurnBaseButton[] turnBaseButtons;

    //private SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        myCharacter = GameObject.FindWithTag("Pembaik").GetComponent<TurnBaseCharacter>();
        enemy = GameObject.Find("enemy").GetComponent<TurnBaseCharacter>();

        foreach (TurnBaseButton tbb in turnBaseButtons)
        {
            tbb.myTurnBaseSystem = this;
        }

        SetVariableOfEnemy();

        StartFighting();
        SceneTransitionManager.instance.OnSceneLoaded();
    }

    void SetVariableOfEnemy()
    {

        enemy.int_atkDmg = tmpData.int_atkDmgEnemy;
        enemy.int_hp = tmpData.int_hpEnemy;
        enemy.string_nama = tmpData.string_namaEnemy;
    }

    public void StartFighting()
    {
        playerHealth_txt.text = $"{myCharacter.int_hp}";
        enemyHealth_txt.text = $"{enemy.int_hp}";

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
        enemyHealth_txt.text = $"{enemy.int_hp}";

        if (enemy.int_hp <= 0)
        {
            enemy.gameObject.SetActive(false);
            tmpData.isContinue = true;
            SceneManager.LoadScene("Platformer1");
            SceneTransitionManager.instance.TransitionToScene("Platformer1");
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
        playerHealth_txt.text = $"{myCharacter.int_hp}";

        if (myCharacter.int_hp <= 0)
        {
            tmpData.ResetData();
            SceneTransitionManager.instance.TransitionToScene("GameOver");
        }
        else
        {
            MunculinPertanyaan();
        }
    }
}
