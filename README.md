# Zwei-Hander
A 2D sprite-based game built using MonoGame, featuring a controllable player (Link), enemies, items, and environment blocks. The project demonstrates object-oriented game architecture with factories, sprite management, and a command-based input system. It's a very cool project! Please.
## Controls
R to reset to original state, Q/esc to quit the program.

E to hurt player.

WASD or arrow keys to move; diagonal movememnt allowed.

X to use the current item selected next to thr sword.

< and > to swap between items selected.

N or Z to make Link attack with sword.

P to pause the game.

Press H on the main menu to enter Horde mode.
## Known issues
The game lags really hard if reset is used multiple times.

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

Damage/ - Implements damage handling and status effects.
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

Fairy gives a random 30 second buff (strength, speed, or regen)

Bow has unlimited cold (slowing) arrows

Red Potion heals fully

Blue Potion gives 15 seconds of strength and speed

There might be certain items that can be picked up that will have no effect/functionality
