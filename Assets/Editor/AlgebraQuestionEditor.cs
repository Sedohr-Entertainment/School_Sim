using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AlgebraQuestion))]
public class AlgebraQuestionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var q = (AlgebraQuestion)target;

        // Metadata
        q.category = (MathCategory)EditorGUILayout.EnumPopup("Category", q.category);
        q.difficulty = EditorGUILayout.IntSlider("Difficulty", q.difficulty, 1, 10);

        EditorGUILayout.Space();

        // Left-side terms
        EditorGUILayout.LabelField("Left Side Terms:", EditorStyles.boldLabel);
        for (int i = 0; i < q.leftSide.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            q.leftSide[i].coefficient = EditorGUILayout.IntField($"Coeff {i + 1}", q.leftSide[i].coefficient);
            q.leftSide[i].hasVariable = EditorGUILayout.Toggle("Has Variable (x)", q.leftSide[i].hasVariable);
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                q.leftSide.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Term"))
        {
            q.leftSide.Add(new AlgebraTerm { coefficient = 1, hasVariable = true });
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Right side constant
        q.rightSide = EditorGUILayout.IntField("Right Side Constant", q.rightSide);

        EditorGUILayout.Space();

        // Preview
        EditorGUILayout.LabelField("Equation Preview:", EditorStyles.boldLabel);
        string preview = q.GetQuestionText();
        string answer = q.GetCorrectAnswerText();
        EditorGUILayout.HelpBox($"{preview}\nSolve for x → {answer}", MessageType.Info);

        if (GUI.changed)
            EditorUtility.SetDirty(q);

    }

}
