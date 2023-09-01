using UnityEngine;
using UnityEditor;
using System.IO;
// script reference by - Zee Vasilyev - https://www.youtube.com/watch?v=H2qMxWcO9dg&list=PLFY3TFPG0dkomvWyb2fcPeqDSSvIO0NYb&index=13
// script edited by Oliver lancashire
// sid 1901981
[CustomEditor(typeof(AchievementDatabase))]
public class AchievementDatabaseEditor : Editor {
    // data base info
    private const string ENUM_NAME = "AchievementID";
    private const string ENUM_FILE_NAME = ENUM_NAME + ".cs";
    // reference
    private AchievementDatabase database;

    private void OnEnable()
    {
        database = target as AchievementDatabase;
    }
    // update gui info
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Enum", GUILayout.Height(30)))
        {
            GenerateEnum();
        }
    }
    // generate list
    private void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath, ENUM_FILE_NAME);
        string code = "public enum " + ENUM_NAME + " {\n";
        foreach (Achievement achievement in database.achievements)
        {
            //TODO: Validate the id is proper format
            code += "\t" + achievement.id + ",\n";
        }
        code += "}\n";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/" + ENUM_FILE_NAME);
    }

}
