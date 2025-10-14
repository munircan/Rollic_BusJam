# Bus Jam

General Information

Note on Assets: This project was developed entirely using Unity's default assets (primitives).


Packages Used

Odin Inspector: For advanced Editor features and improved serialization.

DOTween: Used for fluid animations (Tweening) and object movements.

More Effective Coroutines: For timer.

Quick Outline: For outline.

UniTask: To manage asynchronous operations (asynchronous programming) in a more modern and efficient way.

How to Run

To Play: Simply open the GameplayScene scene and press the Play button in the Unity Editor.

Level Editor


The custom Inspector for the Level Data Scriptable Object allows for quick editing (usage is in infoboxes), and all changes are automatically persisted to the asset.

<img width="789" height="861" alt="image" src="https://github.com/user-attachments/assets/077accb3-b73f-481c-ab13-afd62af75d8a" />
Custom Inspector Controls (LevelEditorSettings)

You can use keyboard shortcuts while the cursor is hovering over a tile object:

W key: Toggles the tile type to a Person object.

W + [Number Key 1-9]: Assigns the color of the Person object (e.g., pressing 2 makes it Blue).

M key: Sets the person type to Mysterious (Special Type).

D key: Sets the person type back to Default.

All shortcut configurations are managed through the LevelEditorSettings Scriptable Object asset.

<img width="777" height="482" alt="image" src="https://github.com/user-attachments/assets/542cb6cd-3eea-4dd9-84c3-52794e8f6bd4" />
