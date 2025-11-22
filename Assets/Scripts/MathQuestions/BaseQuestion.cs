using UnityEngine;

public enum MathCategory { Arithmetric, Algebra, Geometry, Trigonometry, Calculus, Statistics }

public abstract class BaseQuestion : ScriptableObject
{

    [Header("Question metadata")]
    public MathCategory category;

    [Range(1,10)] public int difficulty = 1;

    //Text shown to player and editor preview
    public abstract string GetQuestionText();

    //Canonical correct answer as text; gives flexibitity for non-integer answers
    public abstract string GetCorrectAnswerText();

    //Core validation method used by QuizManager
    public abstract bool CheckAnswer(string playerAnswer);

    //Optional method to render custom board elements
    public virtual void OnRenderBoard(GameObject boardroot) { }

}
