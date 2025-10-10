# 🎮 Summer 2024 Game Project

A 2D platformer built in **Unity (C#)** featuring modular scene loading, finite-state-machine (FSM)–based AI, a fully functional inventory and crafting system, and dynamic NPC behavior.

---

## 🧭 Overview

This project was developed during **Summer 2024** as an independent game-development initiative.  
The goal was to build a scalable, extensible 2D platformer framework with:

- **Additive scene loading** for a seamless world  
- **Finite State Machine (FSM)** architecture for enemies and friendly NPCs  
- **Persistent objects** that survive scene transitions  
- **Door-based transport system** to dynamically load/unload areas  
- **Complex inventory and crafting system** with stack management and stat interaction  
- **Animation-driven interactions** for characters and environment  

Although the game is **not currently playable**, the repository demonstrates significant progress in engine architecture, gameplay systems, and AI design patterns.

---

## 🧩 Key Technical Highlights

- **Language / Engine:** Unity 2022.3 (C#)  
- **Design Patterns:**  
  - FSM  
  - Object Pooling  
  - Singleton  
  - Event System / Observer Pattern  
- **Core Systems:**  
  - Additive scene management (`LoadSceneAsync`)  
  - Door and teleport system with destination mapping  
  - Persistent player and world data between scenes  
  - NPC AI with proximity triggers and behavioral states  
  - Modular animation controller setup  
  - **Inventory System:**  
    - Each slot holds up to 64 of one item type  
    - Automatic stack merging (e.g., adding 30 items to a slot with 50 fills it to 64 and moves overflow to another slot)  
    - Real-time slot validation and item redistribution  
  - **Crafting System:**  
    - Integrates directly with the inventory system  
    - Allows using herbs and ingredients to craft potions  
    - Consumes items on craft and generates resulting potions  
  - **Health and Mana Systems:**  
    - Potions increase health or mana stats upon use  
    - Fully integrated with the player’s stat and UI systems  

---

## 🚧 Current Status

The project is in a broken state and **not currently playable**.  

---

## 🔍 Notable Scripts

| File | Description |
|------|--------------|
| `Scripts/Entities/FiniteStateMachine.cs` | Core finite state machine (FSM) controller that manages state transitions for entities. Handles initialization, switching, and execution of state-specific logic through well-defined lifecycle methods. |
| `Scripts/Entities/State.cs` | Abstract base class defining the structure and lifecycle for all entity states. Handles environment checks (ground, wall, player proximity) and separates logic and physics updates for clean modular design. |
| `Scripts/Entities/Entity.cs` | Comprehensive base class for all AI-driven entities. Integrates physics, animation, health, combat, and stun logic with the FSM. Manages hit detection, knockback, flipping, and Gizmo-based debugging for visual development. |
| `Scripts/Scenes/SceneSwitchManager.cs` | Manages additive scene loading and unloading for seamless world transitions. Handles door-based scene changes, fade effects, camera persistence, and NPC position saving across scenes. |
| `Scripts/Inventory/InventoryManager.cs` | Manages the player’s inventory, UI, and item stacking logic. Each slot can hold up to 64 of one item type, automatically redistributing overflow across slots. Integrates closely with the crafting system through event-based updates. |
| `Scripts/Crafting/PotionsCraftingManager.cs` | Handles crafting system logic, allowing players to combine herbs from the inventory to create potions. Consumes ingredients, generates potion items, and updates both inventory and UI in real time. |
| `Scripts/PlayerControls/New/States/Player.cs` | Main player controller implementing movement, jumping, attacking, and scene transition handling. Uses a player-specific FSM to modularize behavior, manage animations, and handle full freeze/unfreeze logic during scene switches. |
| `Scripts/PlayerControls/New/States/PlayerState.cs` | Abstract base class for all player states. Defines animation handling, input-based logic and physics updates, and time tracking for state transitions. Enables extensible, maintainable player behavior through inheritance. |


---

## 📘 Future Plans

- Expand crafting recipes and potion types  
- Add NPC dialogue and quest framework   
- Implement complete menu and UI system  

---

## 🏷️ Notes

> This repository is intended to showcase **code quality**, **system architecture**, and **design approach** rather than gameplay completeness.  
> Reviewers are encouraged to explore the `Scripts` directory for insights into modular scene loading, FSM-driven AI, and the inventory–crafting interaction system.

