using TMPro;
using UnityEngine;

public class QuizUI : MonoBehaviour
{
    public QuizManager quizManager;
    public TMP_Text questionText, answerText;   // shows the submitted answer

    void Start()
    {
        quizManager.AskQuestion();        
    }

    // Refresh only when a new question is asked
    public void RefreshUI()
    {
        if (quizManager.questions.Length > quizManager.currentQuestionIndex)
        {
            questionText.text = quizManager.questions[quizManager.currentQuestionIndex].GetQuestionText();

        }
        else
        {
            questionText.text = "Quiz Finished!";
            answerText.text = $"Score: {quizManager.correctCount}/{quizManager.questions.Length}";
        }
    }

    public void ShowAnswer(int playerAnswer, bool isCorrect)
    {
        answerText.text = $"Your Answer: {playerAnswer} - " +
                          (isCorrect ? "<color=green>Correct!</color>" : "<color=red>Incorrect!</color>");
    }

}