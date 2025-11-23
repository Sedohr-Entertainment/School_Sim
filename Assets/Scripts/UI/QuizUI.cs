using TMPro;
using UnityEngine;

public class QuizUI : MonoBehaviour
{
    public QuizManager quizManager;
    public TMP_Text questionText, answerText, metaText;   // shows the submitted answer

    void Start()
    {
        quizManager.AskQuestion();        
    }

    // Refresh only when a new question is asked
    public void RefreshUI()
    {
        // Use filteredQuestions instead of questions[]
        if (quizManager.HasMoreQuestions())
        {
            BaseQuestion current = quizManager.GetCurrentQuestion();

            questionText.text = current.GetQuestionText();

            // Optional: show category + difficulty
            if (metaText != null)
                metaText.text = $"Category: {current.category} | Difficulty: {current.difficulty}";
        }
        else
        {
            questionText.text = "Quiz Finished!";
            answerText.text = $"Score: {quizManager.CorrectCount}/{quizManager.TotalQuestions()}";
            if (metaText != null) metaText.text = "";
        }
    }


    public void ShowAnswer(string playerAnswer, bool isCorrect)
    {
        answerText.text = $"Your Answer: {playerAnswer} - " +
                          (isCorrect ? "<color=green>Correct!</color>" : "<color=red>Incorrect!</color>");
    }

}