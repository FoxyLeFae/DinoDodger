using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string correctAnswer;
        public string wrongAnswer;
    }

    public Question[] questions;

    public TextMeshProUGUI questionTextUI;
    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;

    bool correctIsButton1;

    void OnEnable()
    {
        ShowRandomQuestion();
    }

    public void ShowRandomQuestion()
    {
        int index = Random.Range(0, questions.Length);
        Question q = questions[index];

        questionTextUI.text = q.questionText;

        // Random hvem der er korrekt
        correctIsButton1 = Random.value > 0.5f;

        if (correctIsButton1)
        {
            button1Text.text = q.correctAnswer;
            button2Text.text = q.wrongAnswer;
        }
        else
        {
            button1Text.text = q.wrongAnswer;
            button2Text.text = q.correctAnswer;
        }
    }

    public void Button1Pressed()
    {
        if (correctIsButton1)
            CorrectAnswer();
        else
            WrongAnswer();
    }

    public void Button2Pressed()
    {
        if (!correctIsButton1)
            CorrectAnswer();
        else
            WrongAnswer();
    }

    public void CorrectAnswer()
    {
        FindFirstObjectByType<GameManager>().ResumeGame();
        Debug.Log("Rigtigt svar trykket!");
    }

    public void WrongAnswer()
    {
        FindFirstObjectByType<GameManager>().WrongAnswer();
    }
}