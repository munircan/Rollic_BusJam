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

public class LevelEditorWindow : EditorWindow
{
    private LevelScriptableObject _levelData;
    private Vector2 _busScroll;
    private Vector2 _slotScroll;
    private Vector2 _tileScroll;

    // Tile interaktif
    private int _hoveredTile = -1;
    private int _selectedTile = -1;
    private TileType _selectedTileType;
    private PersonColor _selectedPersonColor;
    
   

    private const float CellSize = 70f;

    [MenuItem("Tools/Level Editor")]
    public static void OpenWindow() => GetWindow<LevelEditorWindow>("Level Editor");

    private void OnGUI()
    {
        GUILayout.Label("🧩 LEVEL EDITOR", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        _levelData = (LevelScriptableObject)EditorGUILayout.ObjectField("Level Data", _levelData, typeof(LevelScriptableObject), false);

        if (_levelData == null)
        {
            EditorGUILayout.HelpBox("Assign a LevelData asset to edit.", MessageType.Info);
            return;
        }

        // DRAW SECTIONS
        DrawBusSection();
        EditorGUILayout.Space(20);
        DrawSlotSection();
        EditorGUILayout.Space(20);
        DrawTileSection();

        if (GUI.changed) EditorUtility.SetDirty(_levelData);
    }

    // -------------------------------------------------
    // BUS SECTION
    // -------------------------------------------------
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

        _busScroll = EditorGUILayout.BeginScrollView(_busScroll, GUILayout.Height(150));
        for (int i = 0; i < _levelData.Data.Buses.Length; i++)
        {
            var bus = _levelData.Data.Buses[i];
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label($"Bus #{i + 1}", EditorStyles.miniBoldLabel);
            bus.PersonLimit = EditorGUILayout.IntField("Person Limit", Mathf.Max(1, bus.PersonLimit));
            bus.PersonColor = (PersonColor)EditorGUILayout.EnumPopup("Person Color", bus.PersonColor);
            _levelData.Data.Buses[i] = bus;
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }

    // -------------------------------------------------
    // SLOT SECTION
    // -------------------------------------------------
    private void DrawSlotSection()
    {
        GUILayout.Label("🎯 SLOT GRID", EditorStyles.boldLabel);

        int newWidth = EditorGUILayout.IntField("Slot Width", Mathf.Max(1, _levelData.Data.SlotWidth));
        int newHeight = EditorGUILayout.IntField("Slot Height", Mathf.Max(1, _levelData.Data.SlotHeight));

        if (newWidth != _levelData.Data.SlotWidth || newHeight != _levelData.Data.SlotHeight)
            UpdateSlotDimensions(newWidth, newHeight);

        if (GUILayout.Button("Generate Slot Grid", GUILayout.Height(30))) GenerateSlotGrid();

        if (_levelData.Data.Slots == null || _levelData.Data.Slots.Length == 0) return;

        _slotScroll = EditorGUILayout.BeginScrollView(_slotScroll);
        DrawSlotGrid();
        EditorGUILayout.EndScrollView();
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
        _levelData.Data.Initialize();
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

        _levelData.Data.Initialize();
        EditorUtility.SetDirty(_levelData);
    }

    private void DrawSlotGrid()
    {
        int width = _levelData.Data.SlotWidth;
        int height = _levelData.Data.SlotHeight;
        var slots = _levelData.Data.Slots;

        for (int y = 0; y < height; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                var slot = slots[index];

                EditorGUILayout.BeginVertical("box", GUILayout.Width(120));
                GUILayout.Label($"Slot ({x},{y})", EditorStyles.centeredGreyMiniLabel);
                slot.IsLocked = EditorGUILayout.Toggle("Locked", slot.IsLocked);
                if (slot.IsLocked) slot.LockedLevel = EditorGUILayout.IntField("Locked Level", slot.LockedLevel);
                _levelData.Data.Slots[index] = slot;
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    // -------------------------------------------------
    // TILE SECTION (interactive)
    // -------------------------------------------------
    private void DrawTileSection()
    {
        GUILayout.Label("🟩 TILE GRID", EditorStyles.boldLabel);

        int newWidth = EditorGUILayout.IntField("Tile Width", Mathf.Max(1, _levelData.Data.TileWidth));
        int newHeight = EditorGUILayout.IntField("Tile Height", Mathf.Max(1, _levelData.Data.TileHeight));

        if (newWidth != _levelData.Data.TileWidth || newHeight != _levelData.Data.TileHeight)
            UpdateTileDimensions(newWidth, newHeight);

        if (GUILayout.Button("Generate Tile Grid", GUILayout.Height(30))) GenerateTileGrid();

        if (_levelData.Data.Tiles == null || _levelData.Data.Tiles.Length == 0) return;

        EditorGUILayout.HelpBox("Q = Default, W = Person, E = Obstacle | 1-5 = Person Color", MessageType.Info);

        _tileScroll = EditorGUILayout.BeginScrollView(_tileScroll);
        DrawTileGrid();
        EditorGUILayout.EndScrollView();

        DrawSelectedTilePanel();
        HandleKeyboard();
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
        _levelData.Data.Initialize();
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

        _levelData.Data.Initialize();
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
                    TileType.Person => GetPersonColor(tile.PersonData.Color),
                    TileType.Obstacle => new Color(0.3f, 0.3f, 0.3f),
                    _ => Color.gray
                };
                if (isHovered) bgColor *= 1.2f;

                EditorGUI.DrawRect(rect, bgColor);

                Handles.color = Color.black;
                Handles.DrawAAPolyLine(2f, new Vector3[] {
                    new Vector3(rect.xMin, rect.yMin),
                    new Vector3(rect.xMax, rect.yMin),
                    new Vector3(rect.xMax, rect.yMax),
                    new Vector3(rect.xMin, rect.yMax),
                    new Vector3(rect.xMin, rect.yMin)
                });
                if (_selectedTile == index)
                {
                    // Seçili karo için daha belirgin bir vurgu (örn: sarı/beyaz kalın çerçeve)
                    Handles.color = Color.cyan; 
                    Handles.DrawAAPolyLine(5f, new Vector3[] { // Çerçeveyi kalınlaştır
                        new Vector3(rect.xMin, rect.yMin),
                        new Vector3(rect.xMax, rect.yMin),
                        new Vector3(rect.xMax, rect.yMax),
                        new Vector3(rect.xMin, rect.yMax),
                        new Vector3(rect.xMin, rect.yMin)
                    });
                }

                GUIStyle center = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter, wordWrap = true };
                string label = $"{x},{y}\n{tile.Type}";
                if (tile.Type == TileType.Person) label += $"\n{tile.PersonData.Color}";
                GUI.Label(rect, label, center);

                if (isHovered && e.type == EventType.MouseDown && e.button == 0)
                {
                    _selectedTile = index;
                    _selectedTileType = tile.Type;
                    _selectedPersonColor = tile.PersonData.Color;
                    e.Use();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        Repaint();
    }

    private void DrawSelectedTilePanel()
    {
        if (_selectedTile < 0 || _selectedTile >= _levelData.Data.Tiles.Length) return;

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Selected Tile", EditorStyles.boldLabel);

        _selectedTileType = (TileType)EditorGUILayout.EnumPopup("Tile Type", _selectedTileType);
        if (_selectedTileType == TileType.Person)
            _selectedPersonColor = (PersonColor)EditorGUILayout.EnumPopup("Person Color", _selectedPersonColor);

        if (GUILayout.Button("Apply"))
        {
            var tile = _levelData.Data.Tiles[_selectedTile];
            tile.Type = _selectedTileType;
            if (_selectedTileType == TileType.Person)
                tile.PersonData.Color = _selectedPersonColor;
            _levelData.Data.Tiles[_selectedTile] = tile;
        }
        EditorGUILayout.EndVertical();
    }

    private void HandleKeyboard()
    {
        if (_hoveredTile < 0 || _hoveredTile >= _levelData.Data.Tiles.Length) return;
        Event e = Event.current;
        if (e.type != EventType.KeyDown) return;

        var tile = _levelData.Data.Tiles[_hoveredTile];

        if (e.keyCode == KeyCode.Q) tile.Type = TileType.Default;
        if (e.keyCode == KeyCode.W) tile.Type = TileType.Person;
        if (e.keyCode == KeyCode.E) tile.Type = TileType.Obstacle;

        if (tile.Type == TileType.Person)
        {
            if (e.keyCode == KeyCode.Alpha1) tile.PersonData.Color = PersonColor.Red;
            if (e.keyCode == KeyCode.Alpha2) tile.PersonData.Color = PersonColor.Blue;
            if (e.keyCode == KeyCode.Alpha3) tile.PersonData.Color = PersonColor.Green;
        }

        _levelData.Data.Tiles[_hoveredTile] = tile;
        e.Use();
    }

    private Color GetPersonColor(PersonColor color)
    {
        return color switch
        {
            PersonColor.Red => Color.red,
            PersonColor.Blue => Color.blue,
            PersonColor.Green => Color.green,
            _ => Color.white
        };
    }
}
#endif
