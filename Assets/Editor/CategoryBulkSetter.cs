using UnityEditor;
using UnityEngine;

public class CategoryBulkSetter
{
    [MenuItem("Tools/Set TimesTables Category")]
    public static void SetTimesTablesCategory()
    {
        // Adjust path to your TimesTables folder
        string path = "Assets/Scripts/ScriptableObjects/Multiplication Tables";
        string[] guids = AssetDatabase.FindAssets("t:BaseQuestion", new[] { path });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            BaseQuestion question = AssetDatabase.LoadAssetAtPath<BaseQuestion>(assetPath);

            if (question != null)
            {
                question.category = MathCategory.TimesTables;
                EditorUtility.SetDirty(question);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Updated {guids.Length} questions to TimesTables category.");
    }

}
