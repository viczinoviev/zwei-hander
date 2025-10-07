# Zwei-Hander
A 2D sprite-based game built using MonoGame, featuring a controllable player (Link), enemies, items, and environment blocks. The project demonstrates object-oriented game architecture with factories, sprite management, and a command-based input system. It's a very cool project! Please.
## Controls
R to reset to original state, Q to quit the program.

T, Y to cycle through blocks.
U, I to cycle through items.
O, P to cycle through enemies.
E to hurt player.
WASD or arrow keys to move; diagonal movememnt allowed.
1 to shoot arrow, 2 to throw boomerang, 3 to place bomb.
N or Z to make Link attack with sword.
## Known issues
Fairy is bound in imaginary chains, unable to move :(
Items, enemies, and blocks are not yet interactive (no collision or state changes).
Player health system is partially implemented.
Some sprite transitions may not align perfectly with movement direction.
Projectiles currently have no collision detection.
## Code Structure
Game1.cs – Main entry point, handles initialization, loading content, and game loop.
Commands/ – Contains command classes for each input (e.g., ChangeBlockCommand, ResetCommand, HurtPlayerCommand).
Environment/ – Includes block classes and the BlockFactory.
Enemy/ – Manages enemies and the EnemyFactory.
Items/ – Contains item logic and the ItemManager.
Graphics/ – Includes all sprite classes and storage systems for player, blocks, enemies, and items.
Player/ – Defines Player behavior, movement, attacks, and sprite rendering.
Controllers/ – Implements the KeyboardController to map keys to commands. (In Player Class as of Sprint 2)
## Tools/Frameworks
Used .NET messages to help put code into better format.
MonoGame Framework for rendering, input, and game loop.
C# (.NET) for core game logic.
Command Pattern for flexible input handling.
Factory Pattern for object creation (blocks, items, enemies).
XML-based SpriteSheets for efficient sprite management.
