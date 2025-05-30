using TMPro;
using UnityEngine;

public class TurnBaseButton : MonoBehaviour
{
    public TurnBaseSystem myTurnBaseSystem;

    public TextMeshProUGUI answertxt;
    public bool isCorrectAnswer;

    public void NewAnswer(string text, bool isCorrect)
    {
        answertxt.text = text;
        isCorrectAnswer = isCorrect;
    }

    public void PencetButton()
    {
        if (isCorrectAnswer)
        {
            myTurnBaseSystem.JawabanBener();
        }
        else
        {
            myTurnBaseSystem.JawabanSalah();
        }
    }

}
