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

        [Header("Tile Tipi Kısayolları")]
        public KeyCode DefaultTileKey = KeyCode.Q;
        public KeyCode PersonTileKey = KeyCode.W;
        public KeyCode ObstacleTileKey = KeyCode.E;
    
        [Header("Person Tipi Kısayolları")] // GÜNCELLENDİ
        public KeyCode DefaultPersonTypeKey = KeyCode.T; // Örn: T
        public KeyCode MysteriousPersonTypeKey = KeyCode.Y; // Örn: Y
    
        [Header("Person Renk Kısayolları")]
        public KeyCode RedPersonKey = KeyCode.Alpha1;
        public KeyCode BluePersonKey = KeyCode.Alpha2;
        public KeyCode GreenPersonKey = KeyCode.Alpha3;
    

        public void ResetToDefaults()
        {
            DefaultTileKey = KeyCode.Q;
            PersonTileKey = KeyCode.W;
            ObstacleTileKey = KeyCode.E;
        
            // GÜNCELLENDİ
            DefaultPersonTypeKey = KeyCode.T;
            MysteriousPersonTypeKey = KeyCode.Y;
        
            RedPersonKey = KeyCode.Alpha1;
            BluePersonKey = KeyCode.Alpha2;
            GreenPersonKey = KeyCode.Alpha3;
        }
    }

// LevelEditorSettingsProvider kısmı aynı kalır.
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
                    if (GUILayout.Button("Varsayılan Tuşlara Dön"))
                    {
                        settings.ResetToDefaults();
                        EditorUtility.SetDirty(settings);
                        AssetDatabase.SaveAssets();
                    }
                },
                keywords = new System.Collections.Generic.HashSet<string>(new[] { "Level", "Editor", "Keybinds", "Kısayol" })
            };
            return provider;
        }
    }
}