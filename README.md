# Forge Project

## How to run:
- Open project in unity **6000.0.32f1**
- Open scene _main_
- For the best experience set Game View resolution to **1920x1080**
- Click **Play**
- Drag items using the mouse

## Project Structure

All game data is stored in ScriptableObjects inside **_Assets/ScriptableObjects_**. These can be modified directly to change game behavior.  
I've used ScriptableObjects because they are easy to work with. For a full-scale project, I would consider using a custom data storage method depending on specific needs.

Currently, there is no data validation in place, so unusual changes to data could cause issues (mostly `NullReferenceExceptions`).

The codebase separates domain logic (main gameplay logic) from the view layer (mostly user interface) by placing them in two different assemblies:
- **_Forge.Domain_**
- **_Forge.View_**

The view layer references the domain, but the domain does not reference the view (and does not depend on it in any way).  
No domain classes should inherit from `MonoBehaviour`. The only exception is the **_Root_** class, which defines the entry point and initializes all game logic.

The code also includes several **_Template_** classes. Each of them serves as a **blueprint** (_template_) describing what a given object consists of.  
They are used by actual in-game objects to create instances, following the **flyweight** pattern.

I've focused on delivering a modular and extensible base that could be built upon.  
Given the tight time constraints, there is still plenty of room for improvement (see _Possible Improvements_).  
You’ll also find some `TODOs` in the code suggesting enhancements or pointing out missing feature implementations.

## Design Decisions

Design decisions I made that were not explicitly stated:

- Items are dragged using the mouse
- If a machine's output slot is occupied when crafting completes, the user loses the blocking resource (but receives a notification)
- You can insert items into machine inputs/outputs even if the machine doesn't process that specific item
- **Bonuses** are active only when the source item is in the **Inventory**  
  (For example: the player can move the *Time Amulet* to a smelter output, but will no longer benefit from its bonus)

## Possible Improvements

- Add editor tools for data validation. Ensure that data in ScriptableObjects is valid (e.g., validate that every recipe has a non-null output)
- Refactor the `Machine` class to use the **State** pattern instead of relying on multiple variables
- Try/catch for exceptions in gameplay logic
- Add a **Restart** button
- Improve statistics and upgrades:
  - Move stat tracking out of the `Player` class
  - Track how each item influences specific stats, and store that data
- Test UI scaling and resizing
- Add more structure to UI classes:
  - Consider adding a common interface for all `Initialize` methods
  - Or introduce a base class providing access to common operations
- Implement proper object pooling for game objects
- Improve notifications:
  - Use a list-based system instead of a single text message
- Obviously lots of UI quality-of-life features:
  - Display item names and descriptions
  - Show available recipes for machines
  - Allow dragging multiple items at once