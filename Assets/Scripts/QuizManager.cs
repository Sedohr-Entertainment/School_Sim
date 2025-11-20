using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public MathQuestion[] questions;
    public QuizStation quizStation;
    public QuizUI quizUI;   // reference to your UI script
    public int currentQuestionIndex = 0;
    public int correctCount = 0;

    public void AskQuestion()
    {
        if (currentQuestionIndex < questions.Length )
        {
            MathQuestion q = questions[currentQuestionIndex];
            Debug.Log($"Q{currentQuestionIndex + 1}: {q.GetQuestionText()}");

            // Assign the station's current question
            quizStation.SetCurrentQuestion(q);

            // Update UI
            quizUI.RefreshUI();
        }
        else
        {
            Debug.Log($"Quiz finished! Correct answers: {correctCount}/{questions.Length}");
            quizUI.questionText.text = "Quiz Finished!";
            quizUI.answerText.text = $"Score: {correctCount}/{questions.Length}";
        }
    }

    public void SubmitAnswer(int playerAnswer)
    {
        if (currentQuestionIndex < questions.Length)
        {
            MathQuestion q = questions[currentQuestionIndex];
            bool isCorrect = q.GetCorrectAnswer() == playerAnswer;

            if (isCorrect)
            {
                Debug.Log("Correct!");
                correctCount++;
            }
            else
            {
                Debug.Log($"Incorrect! The correct answer was {q.GetCorrectAnswer()}");
            }

            // Update UI with the submitted answer
            quizUI.ShowAnswer(playerAnswer, isCorrect);

            currentQuestionIndex++;

            AskQuestion();
        }
    }
}