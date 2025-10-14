# Bus Jam

GENERAL INFORMATION
This project was developed entirely using Unity's default assets (primitives) for the sake of simplicity and focus on core mechanics.

Simple Save and Replay System
I have created a basic save system. Right now, it works like this:

It Records: It saves the indexes of all the tiles the player clicks.

It Loads: When the game starts from the save, it goes through the saved indexes and executes the logic that should run when that tile is interacted with to load the game state.

Future Upgrade: Recording Player Timing
We can easily upgrade this system to see exactly how the player played the game.

The Upgrade Idea:

Instead of just saving the tile index, we will also save the time between each click.

PACKAGES AND LIBRARIES
The project utilizes the following key packages to enhance performance, editor workflow, and asynchronous task management:

Odin Inspector: Employed for advanced Editor features and streamlined serialization.

DOTween: Used for fluid object movements and animations (tweening).

More Effective Coroutines: Integrated for reliable and performant timer implementations.

Quick Outline: Used to provide clear visual outlines for selected or interactive objects.

UniTask: Utilized to manage asynchronous operations in a modern, efficient way, offering a more robust alternative to standard C# tasks.

HOW TO RUN
To quickly test the game:

Open the project in the Unity Editor.

Navigate to the GameplayScene scene.

Press the Play button.

LEVEL EDITOR
A custom Inspector was developed for the Level Data Scriptable Object. This allows for quick, intuitive editing (usage instructions are available in the infoboxes), and all changes are automatically persisted to the Scriptable Object asset.

<img width="789" height="861" alt="image" src="https://github.com/user-attachments/assets/077accb3-b73f-481c-ab13-afd62af75d8a" />
Custom Inspector Controls (LevelEditorSettings)

You can use keyboard shortcuts while the cursor is hovering over a tile object:

W key: Toggles the tile type to a Person object.

W + [Number Key 1-9]: Assigns the color of the Person object (e.g., pressing 2 makes it Blue).

M key: Sets the person type to Mysterious (Special Type).

D key: Sets the person type back to Default.

All shortcut configurations are managed through the LevelEditorSettings Scriptable Object asset.

<img width="777" height="482" alt="image" src="https://github.com/user-attachments/assets/542cb6cd-3eea-4dd9-84c3-52794e8f6bd4" />
