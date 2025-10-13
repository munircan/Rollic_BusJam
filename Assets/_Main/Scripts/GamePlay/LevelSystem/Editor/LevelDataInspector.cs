#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.SlotSystem;
using _Main.Scripts.GamePlay.BusSystem.Data;
using System;
using _Main.Scripts.GamePlay.PersonSystem;
using _Main.Scripts.GamePlay.Settings;

[CustomEditor(typeof(LevelScriptableObject))]
public class LevelDataInspector : Editor
{
    private LevelScriptableObject _levelData;

    private Vector2 _tileScroll;

    private int _hoveredTile = -1;
    private int _selectedTile = -1;
    private TileType _selectedTileType;
    private ColorType _selectedColorType;
    private Appearance _selectedPersonType;

    private const float CellSize = 70f;
    private const float SlotCellWidth = 70f;

    private void OnEnable()
    {
        _levelData = (LevelScriptableObject)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Label("🧩 LEVEL EDITOR (Inspector)", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        DrawBusSection();
        EditorGUILayout.Space(20);
        DrawSlotSection();
        EditorGUILayout.Space(20);
        DrawTileSection();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_levelData);
        }

        HandleKeyboard();
        Repaint();
    }

    private void DrawBusSection()
    {
        GUILayout.Label("🚌 BUS DATA", EditorStyles.boldLabel);

        if (_levelData.Data.Buses == null) _levelData.Data.Buses = Array.Empty<BusData>();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Bus", GUILayout.Height(25)))
        {
            Undo.RecordObject(_levelData, "Add Bus");
            Array.Resize(ref _levelData.Data.Buses, _levelData.Data.Buses.Length + 1);
            _levelData.Data.Buses[^1] = new BusData();
            EditorUtility.SetDirty(_levelData);
        }

        if (_levelData.Data.Buses.Length > 0 && GUILayout.Button("Remove Last", GUILayout.Height(25)))
        {
            Undo.RecordObject(_levelData, "Remove Bus");
            Array.Resize(ref _levelData.Data.Buses, _levelData.Data.Buses.Length - 1);
            EditorUtility.SetDirty(_levelData);
        }

        EditorGUILayout.EndHorizontal();

        if (_levelData.Data.Buses.Length == 0)
        {
            EditorGUILayout.HelpBox("No buses added yet.", MessageType.Info);
            return;
        }

        EditorGUILayout.BeginVertical("box");
        for (int i = 0; i < _levelData.Data.Buses.Length; i++)
        {
            var bus = _levelData.Data.Buses[i];
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label($"Bus #{i + 1}", EditorStyles.miniBoldLabel);

            bus.PersonLimit = EditorGUILayout.IntField("Person Limit", Mathf.Max(3, bus.PersonLimit));

            bus.ColorType = (ColorType)EditorGUILayout.EnumPopup("Person Color", bus.ColorType);

            bus.Appearance = (Appearance)EditorGUILayout.EnumPopup("Person Type", bus.Appearance);

            _levelData.Data.Buses[i] = bus;
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawSlotSection()
    {
        GUILayout.Label("🎯 SLOT GRID", EditorStyles.boldLabel);

        int newWidth = EditorGUILayout.IntField("Slot Width", Mathf.Max(1, _levelData.Data.SlotWidth));
        int newHeight = EditorGUILayout.IntField("Slot Height", Mathf.Max(1, _levelData.Data.SlotHeight));

        if (newWidth != _levelData.Data.SlotWidth || newHeight != _levelData.Data.SlotHeight)
            UpdateSlotDimensions(newWidth, newHeight);

        if (GUILayout.Button("Generate Slot Grid", GUILayout.Height(30))) GenerateSlotGrid();

        if (_levelData.Data.Slots == null || _levelData.Data.Slots.Length == 0) return;

        DrawSlotGrid();
    }

    private void UpdateSlotDimensions(int newWidth, int newHeight)
    {
        int oldWidth = _levelData.Data.SlotWidth;
        int oldHeight = _levelData.Data.SlotHeight;
        SlotData[] oldSlots = _levelData.Data.Slots;

        _levelData.Data.SlotWidth = newWidth;
        _levelData.Data.SlotHeight = newHeight;

        SlotData[] newSlots = new SlotData[newWidth * newHeight];
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                int newIndex = y * newWidth + x;
                if (x < oldWidth && y < oldHeight && oldSlots != null && oldSlots.Length > 0)
                {
                    int oldIndex = y * oldWidth + x;
                    newSlots[newIndex] = oldSlots[oldIndex];
                }
                else
                {
                    newSlots[newIndex] = new SlotData { IsLocked = false, LockedLevel = 0 };
                }
            }
        }

        _levelData.Data.Slots = newSlots;
        EditorUtility.SetDirty(_levelData);
    }

    private void GenerateSlotGrid()
    {
        int width = _levelData.Data.SlotWidth;
        int height = _levelData.Data.SlotHeight;

        Undo.RecordObject(_levelData, "Generate Slot Grid");
        _levelData.Data.Slots = new SlotData[width * height];
        for (int i = 0; i < _levelData.Data.Slots.Length; i++)
            _levelData.Data.Slots[i] = new SlotData { IsLocked = false, LockedLevel = 0 };

        EditorUtility.SetDirty(_levelData);
    }

    private void DrawSlotGrid()
    {
        int width = _levelData.Data.SlotWidth;
        int height = _levelData.Data.SlotHeight;
        var slots = _levelData.Data.Slots;

        float slotItemWidth = SlotCellWidth;

        for (int y = 0; y < height; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                var slot = slots[index];

                EditorGUILayout.BeginVertical("box", GUILayout.Width(slotItemWidth), GUILayout.ExpandHeight(false));

                GUILayout.Label($"({x},{y})", EditorStyles.centeredGreyMiniLabel);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("L:", GUILayout.Width(15));
                slot.IsLocked = EditorGUILayout.Toggle(slot.IsLocked);
                EditorGUILayout.EndHorizontal();

                if (slot.IsLocked)
                {
                    slot.LockedLevel = EditorGUILayout.IntField(slot.LockedLevel, GUILayout.Width(slotItemWidth - 4));
                }

                _levelData.Data.Slots[index] = slot;
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }
    }


    private void DrawTileSection()
    {
        GUILayout.Label("🟩 TILE GRID", EditorStyles.boldLabel);

        int newWidth = EditorGUILayout.IntField("Tile Width", Mathf.Max(1, _levelData.Data.TileWidth));
        int newHeight = EditorGUILayout.IntField("Tile Height", Mathf.Max(1, _levelData.Data.TileHeight));

        if (newWidth != _levelData.Data.TileWidth || newHeight != _levelData.Data.TileHeight)
            UpdateTileDimensions(newWidth, newHeight);

        if (GUILayout.Button("Generate Tile Grid", GUILayout.Height(30))) GenerateTileGrid();

        if (_levelData.Data.Tiles == null || _levelData.Data.Tiles.Length == 0) return;

        var settings = LevelEditorSettings.Instance;

        string typeShortcuts =
            $"{settings.DefaultPersonTypeKey} = Default Type, " +
            $"{settings.MysteriousPersonTypeKey} = Mysterious Type";

        string colorShortcuts =
            $"{settings.RedPersonKey}=Red, " +
            $"{settings.BluePersonKey}=Blue, " +
            $"{settings.GreenPersonKey}=Green";

        string infoText =
            $"{settings.DefaultTileKey} = Default, {settings.PersonTileKey} = Person, {settings.ObstacleTileKey} = Obstacle\n" +
            $"Types: ({typeShortcuts})\n" +
            $"Colors: ({colorShortcuts})";

        EditorGUILayout.HelpBox(infoText, MessageType.Info);

        _tileScroll = EditorGUILayout.BeginScrollView(_tileScroll, GUILayout.MinHeight(100), GUILayout.MaxHeight(500));
        DrawTileGrid();
        EditorGUILayout.EndScrollView();

        DrawSelectedTilePanel();
    }

    private void UpdateTileDimensions(int newWidth, int newHeight)
    {
        int oldWidth = _levelData.Data.TileWidth;
        int oldHeight = _levelData.Data.TileHeight;
        TileData[] oldTiles = _levelData.Data.Tiles;

        _levelData.Data.TileWidth = newWidth;
        _levelData.Data.TileHeight = newHeight;

        TileData[] newTiles = new TileData[newWidth * newHeight];
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                int newIndex = y * newWidth + x;
                if (x < oldWidth && y < oldHeight && oldTiles != null && oldTiles.Length > 0)
                {
                    int oldIndex = y * oldWidth + x;
                    newTiles[newIndex] = oldTiles[oldIndex];
                }
                else
                {
                    newTiles[newIndex] = new TileData { Type = TileType.Default, PersonData = new PersonData() };
                }
            }
        }

        _levelData.Data.Tiles = newTiles;
        EditorUtility.SetDirty(_levelData);
    }

    private void GenerateTileGrid()
    {
        int width = _levelData.Data.TileWidth;
        int height = _levelData.Data.TileHeight;

        Undo.RecordObject(_levelData, "Generate Tile Grid");
        _levelData.Data.Tiles = new TileData[width * height];
        for (int i = 0; i < _levelData.Data.Tiles.Length; i++)
            _levelData.Data.Tiles[i] = new TileData { Type = TileType.Default, PersonData = new PersonData() };

        EditorUtility.SetDirty(_levelData);
    }

    private void DrawTileGrid()
    {
        int width = _levelData.Data.TileWidth;
        int height = _levelData.Data.TileHeight;
        var tiles = _levelData.Data.Tiles;
        Event e = Event.current;
        Vector2 mousePos = e.mousePosition;
        _hoveredTile = -1;

        for (int y = 0; y < height; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                var tile = tiles[index];

                Rect rect = GUILayoutUtility.GetRect(CellSize, CellSize);

                bool isHovered = rect.Contains(mousePos);
                if (isHovered) _hoveredTile = index;

                Color bgColor = tile.Type switch
                {
                    TileType.Person => GetPersonColor(tile.PersonData.colorType),
                    TileType.Obstacle => new Color(0.3f, 0.3f, 0.3f),
                    _ => Color.gray
                };
                if (isHovered || _selectedTile == index) bgColor *= 1.2f;
                if (_selectedTile == index) bgColor = Color.yellow;

                EditorGUI.DrawRect(rect, bgColor);

                Handles.color = Color.black;
                Handles.DrawAAPolyLine(2f, new Vector3[]
                {
                    new Vector3(rect.xMin, rect.yMin),
                    new Vector3(rect.xMax, rect.yMin),
                    new Vector3(rect.xMax, rect.yMax),
                    new Vector3(rect.xMin, rect.yMax),
                    new Vector3(rect.xMin, rect.yMin)
                });

                GUIStyle center = new GUIStyle(EditorStyles.boldLabel)
                    { alignment = TextAnchor.MiddleCenter, wordWrap = true };

                string label = $"{x},{y}\n{tile.Type}";
                if (tile.Type == TileType.Person)
                {
                    int colorCode = (int)tile.PersonData.colorType;
                    label += $"\n[{colorCode}] {tile.PersonData.colorType}\n{tile.PersonData.Appearance}";
                }

                GUI.Label(rect, label, center);

                if (isHovered && e.type == EventType.MouseDown && e.button == 0)
                {
                    _selectedTile = index;
                    _selectedTileType = tile.Type;
                    _selectedColorType = tile.PersonData.colorType;
                    _selectedPersonType = tile.PersonData.Appearance;

                    e.Use();
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawSelectedTilePanel()
    {
        if (_selectedTile < 0 || _selectedTile >= _levelData.Data.Tiles.Length) return;

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label($"Selected Tile ({_selectedTile})", EditorStyles.boldLabel);

        _selectedTileType = (TileType)EditorGUILayout.EnumPopup("Tile Type", _selectedTileType);

        if (_selectedTileType == TileType.Person)
        {
            _selectedColorType = (ColorType)EditorGUILayout.EnumPopup("Person Color", _selectedColorType);
            _selectedPersonType = (Appearance)EditorGUILayout.EnumPopup("Person Type", _selectedPersonType);
        }

        if (GUILayout.Button("Apply"))
        {
            Undo.RecordObject(_levelData, "Apply Tile Changes");
            var tile = _levelData.Data.Tiles[_selectedTile];
            tile.Type = _selectedTileType;
            if (_selectedTileType == TileType.Person)
            {
                tile.PersonData.colorType = _selectedColorType;
                tile.PersonData.Appearance = _selectedPersonType;
            }

            _levelData.Data.Tiles[_selectedTile] = tile;
            EditorUtility.SetDirty(_levelData);
        }

        EditorGUILayout.EndVertical();
    }

    private void HandleKeyboard()
    {
        if (_hoveredTile < 0 || _hoveredTile >= _levelData.Data.Tiles.Length) return;
        Event e = Event.current;
        if (e.type != EventType.KeyDown) return;

        var settings = LevelEditorSettings.Instance;
        var tile = _levelData.Data.Tiles[_hoveredTile];
        bool tileChanged = false;

        if (e.keyCode == settings.DefaultTileKey)
        {
            tile.Type = TileType.Default;
            tileChanged = true;
        }
        else if (e.keyCode == settings.PersonTileKey)
        {
            tile.Type = TileType.Person;
            tileChanged = true;
        }
        else if (e.keyCode == settings.ObstacleTileKey)
        {
            tile.Type = TileType.Obstacle;
            tileChanged = true;
        }

        if (tile.Type == TileType.Person)
        {
            if (e.keyCode == settings.RedPersonKey)
            {
                tile.PersonData.colorType = ColorType.Red;
                tileChanged = true;
            }
            else if (e.keyCode == settings.BluePersonKey)
            {
                tile.PersonData.colorType = ColorType.Blue;
                tileChanged = true;
            }
            else if (e.keyCode == settings.GreenPersonKey)
            {
                tile.PersonData.colorType = ColorType.Green;
                tileChanged = true;
            }

            else if (e.keyCode == settings.DefaultPersonTypeKey)
            {
                tile.PersonData.Appearance = Appearance.Default;
                tileChanged = true;
            }
            else if (e.keyCode == settings.MysteriousPersonTypeKey)
            {
                tile.PersonData.Appearance = Appearance.Mysterious;
                tileChanged = true;
            }
        }

        if (tileChanged)
        {
            Undo.RecordObject(_levelData, "Tile Data Changed via Keyboard");
            _levelData.Data.Tiles[_hoveredTile] = tile;
            EditorUtility.SetDirty(_levelData);
            e.Use();
        }
    }

    private Color GetPersonColor(ColorType colorType)
    {
        return colorType switch
        {
            ColorType.Red => Color.red,
            ColorType.Blue => Color.blue,
            ColorType.Green => Color.green,
            _ => Color.white
        };
    }
}
#endif