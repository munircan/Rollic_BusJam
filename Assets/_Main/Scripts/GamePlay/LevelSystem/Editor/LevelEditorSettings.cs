// LevelEditorSettings.cs (GÜNCELLENMİŞ)

using UnityEngine;
using UnityEditor;

public class LevelEditorSettings : ScriptableObject
{
    // ... (s_Instance ve Instance kısmı aynı kalır) ...
    private static LevelEditorSettings s_Instance;
    public static LevelEditorSettings Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = AssetDatabase.LoadAssetAtPath<LevelEditorSettings>("Assets/Editor/LevelEditorSettings.asset");
                
                if (s_Instance == null)
                {
                    s_Instance = CreateInstance<LevelEditorSettings>();
                    s_Instance.ResetToDefaults();
                    AssetDatabase.CreateAsset(s_Instance, "Assets/Editor/LevelEditorSettings.asset");
                    AssetDatabase.SaveAssets();
                    Debug.Log("Level Editor Settings dosyası oluşturuldu: Assets/Editor/LevelEditorSettings.asset");
                }
            }
            return s_Instance;
        }
    }

    [Header("Tile Tipi Kısayolları")]
    public KeyCode DefaultTileKey = KeyCode.Q;
    public KeyCode PersonTileKey = KeyCode.W;
    public KeyCode ObstacleTileKey = KeyCode.E;
    
    [Header("Person Tipi Kısayolu")]
    public KeyCode NextPersonTypeKey = KeyCode.T;
    
    [Header("Person Renk Kısayolları")] // YENİ BAŞLIK
    public KeyCode RedPersonKey = KeyCode.Alpha1;   // Örn: 1
    public KeyCode BluePersonKey = KeyCode.Alpha2;  // Örn: 2
    public KeyCode GreenPersonKey = KeyCode.Alpha3; // Örn: 3
    // Daha fazla renk varsa, buraya ekleyin...

    public void ResetToDefaults()
    {
        DefaultTileKey = KeyCode.Q;
        PersonTileKey = KeyCode.W;
        ObstacleTileKey = KeyCode.E;
        NextPersonTypeKey = KeyCode.T;
        
        // YENİ: Varsayılan renk tuşları
        RedPersonKey = KeyCode.Alpha1;
        BluePersonKey = KeyCode.Alpha2;
        GreenPersonKey = KeyCode.Alpha3;
    }
}
// LevelEditorSettingsProvider kısmı aynı kalır.