using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public List<BaseQuestion> allQuestions;
    public List<MathCategory> activeCategories;
    [Range(1, 10)] public int minDifficulty = 1;
    [Range(1, 10)] public int maxDifficulty = 10;

    private List<BaseQuestion> filteredQuestions;
    private int currentQuestionIndex = 0;
    private int correctCount = 0;
    
    public int CorrectCount
    {
        get { return correctCount; }
    }

    public int GetQuestionsRemaining()
    {
        return (filteredQuestions?.Count ?? 0) - currentQuestionIndex;
    }


    public QuizStation quizStation;
    public QuizUI quizUI;
    public BlackboardRenderer blackboardRenderer;

    private void Start()
    {
        FilterQuestions();
        AskQuestion();
    }

    private void FilterQuestions()
    {
        filteredQuestions = allQuestions
            .Where(q => activeCategories.Contains(q.category)
                        && q.difficulty >= minDifficulty
                        && q.difficulty <= maxDifficulty)
            .ToList();
        currentQuestionIndex = 0;
        correctCount = 0;

    }

    public void AskQuestion()
    {
        if (currentQuestionIndex < filteredQuestions.Count)
        {
            BaseQuestion q = filteredQuestions[currentQuestionIndex];
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

            Debug.Log($"Quiz finished! Correct answers: {correctCount}/{filteredQuestions.Count}");
            quizUI.questionText.text = "Quiz Finished!";
            quizUI.answerText.text = $"Score: {correctCount}/{filteredQuestions.Count}";
        }
    }

    public void SubmitAnswer(string playerAnswer)
    {
        if (currentQuestionIndex < filteredQuestions.Count)
        {
            BaseQuestion q = filteredQuestions[currentQuestionIndex];
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


    public BaseQuestion GetCurrentQuestion()
    {
        if (currentQuestionIndex < filteredQuestions.Count)
            return filteredQuestions[currentQuestionIndex];
        return null;
    }

    public bool HasMoreQuestions()
    {
        return currentQuestionIndex < filteredQuestions.Count;
    }

    public int TotalQuestions()
    {
        return filteredQuestions.Count;
    }

}