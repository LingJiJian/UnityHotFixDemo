using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExportConfigWindow : EditorWindow
{
    public static string EXPORT_PREFABS_PATH = "Assets/Game/Resources/Prefabs";
    public static string EXPORT_SCENE_PATH = "Assets/Game/Resources/Scenes";
    public static string EXPORT_OUT_PATH = Application.dataPath;

    [MenuItem("Hotfix/Config")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ExportConfigWindow window = (ExportConfigWindow)EditorWindow.GetWindow(typeof(ExportConfigWindow),true,"Hotfix");
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Resource Path", EditorStyles.boldLabel);
        EXPORT_SCENE_PATH = EditorGUILayout.TextField("Scenes", EXPORT_SCENE_PATH);
        EXPORT_PREFABS_PATH = EditorGUILayout.TextField("Prefabs", EXPORT_PREFABS_PATH);

        GUILayout.Label("Output Path", EditorStyles.boldLabel);
        EXPORT_OUT_PATH = EditorGUILayout.TextField("Output", EXPORT_OUT_PATH);

        if (GUILayout.Button("run"))
        {
            ExportAssetBundles.Run();
            this.Close();
        }
    }

}
