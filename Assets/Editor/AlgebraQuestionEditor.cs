using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AlgebraQuestion))]
public class AlgebraQuestionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var q = (AlgebraQuestion)target;
        q.category = (MathCategory)EditorGUILayout.EnumPopup("Category", q.category);
        q.difficulty = EditorGUILayout.IntSlider("Difficulty", q.difficulty, 1, 10);

        q.a = EditorGUILayout.IntField("Coefficient a", q.a);
        q.b = EditorGUILayout.IntField("Constant b", q.b);
        q.c = EditorGUILayout.IntField("Right-hand side c", q.c);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Equation Preview:", EditorStyles.boldLabel);
        string preview = q.GetQuestionText();
        string answer = q.GetCorrectAnswerText();
        EditorGUILayout.HelpBox($"{preview}\nSolve for x -> {answer}", MessageType.Info);
        
        if (GUI.changed)
            EditorUtility.SetDirty(q);
        
    }

}
