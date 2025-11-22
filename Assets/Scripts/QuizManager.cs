using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public BaseQuestion[] questions;
    public QuizStation quizStation;
    public QuizUI quizUI;
    public int currentQuestionIndex = 0;
    public int correctCount = 0;

    public BlackboardRenderer blackboardRenderer;

    public void AskQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            BaseQuestion q = questions[currentQuestionIndex];
            Debug.Log($"Q{currentQuestionIndex + 1} [{q.category}] (D{q.difficulty}): {q.GetQuestionText()}");

            quizStation.SetCurrentQuestion(q);

            if (blackboardRenderer != null) { 
                q.OnRenderBoard(blackboardRenderer.gameObject);
            }
            quizUI.RefreshUI(); // ensure UI reads from station or manager
        }
        else
        {
            blackboardRenderer.ClearBoard();

            Debug.Log($"Quiz finished! Correct answers: {correctCount}/{questions.Length}");
            quizUI.questionText.text = "Quiz Finished!";
            quizUI.answerText.text = $"Score: {correctCount}/{questions.Length}";
        }
    }

    public void SubmitAnswer(string playerAnswer)
    {
        if (currentQuestionIndex < questions.Length)
        {
            BaseQuestion q = questions[currentQuestionIndex];
            bool isCorrect = q.CheckAnswer(playerAnswer);

            if (isCorrect)
            {
                Debug.Log("Correct!");
                correctCount++;
            }
            else
            {
                Debug.Log($"Incorrect! Correct answer: {q.GetCorrectAnswerText()}");
            }

            quizUI.ShowAnswer(playerAnswer, isCorrect);
            currentQuestionIndex++;
            AskQuestion();
        }
    }

}