# Zwei-Hander
A 2D sprite-based game built using MonoGame, featuring a controllable player (Link), enemies, items, and environment blocks. The project demonstrates object-oriented game architecture with factories, sprite management, and a command-based input system. It's a very cool project! Please.
## Controls
R to reset to original state, Q to quit the program.

E to hurt player.

WASD or arrow keys to move; diagonal movememnt allowed.

8 and 9 to rotate through acquired items. X to use selected item.

N or Z to make Link attack with sword.
## Known issues
The game lags really hard if reset is used multiple times.

Player health system is partially implemented.

Some sprite transitions may not align perfectly with movement direction.

## Code Structure
Game1.cs – Main entry point, handles initialization, loading content, and game loop.

Commands/ – Contains command classes for each input (e.g., InventoryCommand, ResetCommand, QuitCommand).

Environment/ – Includes block classes and the BlockFactory.

Enemy/ – Manages enemies and the EnemyManager, with all the actual enemy classes in EnemyStorage.

Items/ – Contains item logic and the ItemManager.

Graphics/ – Includes all sprite classes and storage systems for player, blocks, enemies, items, and portals.

Player/ – Defines Player behavior, movement, attacks, and sprite rendering.

Controllers/ – Implements the KeyboardController to map keys to commands. (In Player Class as of Sprint 2)

CollisionFiles/ - Handles collisions between player, enemies, items, and blocks.

Camera/ - Has Camera.cs, which handles the poisition of what is viewed.

GameStates/ - Handles what section (state) the game itself is in (titlescreen, paused, etc.)

HUD/ - Contains all parts of the HUD like inventory and map.

Map/ - How the game is laid out and handled.

## Tools/Frameworks
Used .NET messages to help put code into better format.

MonoGame Framework for rendering, input, and game loop.

C# (.NET) for core game logic.

Command Pattern for flexible input handling.

Factory Pattern for object creation (blocks, items, enemies).

XML-based SpriteSheets for efficient sprite management.

## Functionality notes
Bombs can explode other bombs

Fire item (denoted by candle in inventory after pickup) shoots fire that chooses and homes in on nearby enemy after initial stopping

Picking up bomb gives 10 bombs

Fairy causes health change of random amount between -2 and 2 (inclusive, 2 life=1 heart)

Bow has unlimited arrows

Map and Clock currently have no functionality
