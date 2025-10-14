using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.GamePlay.LevelSystem.Editor
{
    public class LevelEditorSettings : ScriptableObject
    {
        private static LevelEditorSettings s_Instance;
        public static LevelEditorSettings Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = AssetDatabase.LoadAssetAtPath<LevelEditorSettings>("Assets/_Main/Data/LevelEditor/LevelEditorSettings.asset");

                    if (s_Instance == null)
                    {
                        s_Instance = CreateInstance<LevelEditorSettings>();
                        s_Instance.ResetToDefaults();
                        AssetDatabase.CreateAsset(s_Instance, "Assets/_Main/Data/LevelEditor/LevelEditorSettings.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
                return s_Instance;
            }
        }

        [Header("Tile Type Shortcuts")]
        public KeyCode NoneTileKey = KeyCode.A;
        public KeyCode DefaultTileKey = KeyCode.Q;
        public KeyCode PersonTileKey = KeyCode.W;
        public KeyCode ObstacleTileKey = KeyCode.E;

        [Header("Person Type Shortcuts")]
        public KeyCode DefaultPersonTypeKey = KeyCode.T;
        public KeyCode MysteriousPersonTypeKey = KeyCode.Y;

        [Header("Person Color Shortcuts")]
        public KeyCode WhitePersonKey = KeyCode.Alpha1;
        public KeyCode BluePersonKey = KeyCode.Alpha2;
        public KeyCode RedPersonKey = KeyCode.Alpha3;
        public KeyCode GreenPersonKey = KeyCode.Alpha4;
        public KeyCode OrangePersonKey = KeyCode.Alpha5;
        public KeyCode YellowPersonKey = KeyCode.Alpha6;


        public void ResetToDefaults()
        {
            NoneTileKey = KeyCode.A;
            DefaultTileKey = KeyCode.Q;
            PersonTileKey = KeyCode.W;
            ObstacleTileKey = KeyCode.E;

            DefaultPersonTypeKey = KeyCode.T;
            MysteriousPersonTypeKey = KeyCode.Y;

            WhitePersonKey = KeyCode.Alpha1;
            BluePersonKey = KeyCode.Alpha2;
            RedPersonKey = KeyCode.Alpha3;
            GreenPersonKey = KeyCode.Alpha4;
            OrangePersonKey = KeyCode.Alpha5;
            YellowPersonKey = KeyCode.Alpha6;
        }
    }

    public class LevelEditorSettingsProvider
    {
        private static UnityEditor.Editor s_SettingsEditor;

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Project/LevelEditorSettings", SettingsScope.Project)
            {
                label = "Level Editor",
                guiHandler = (searchContext) =>
                {
                    var settings = LevelEditorSettings.Instance;

                    if (s_SettingsEditor == null || s_SettingsEditor.target != settings)
                    {
                        s_SettingsEditor = UnityEditor.Editor.CreateEditor(settings);
                    }

                    if (s_SettingsEditor != null)
                    {
                        s_SettingsEditor.OnInspectorGUI();
                    }

                    GUILayout.Space(15);
                    if (GUILayout.Button("Reset to Default Keybinds"))
                    {
                        settings.ResetToDefaults();
                        EditorUtility.SetDirty(settings);
                        AssetDatabase.SaveAssets();
                    }
                },
                keywords = new System.Collections.Generic.HashSet<string>(new[] { "Level", "Editor", "Keybinds", "Shortcut" })
            };
            return provider;
        }
    }
}