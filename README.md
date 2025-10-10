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
Core gameplay systems (scene loading, player controller, inventory, and AI logic) are implemented and functional, with future work focused on level content, UI, and additional gameplay mechanics.

---

## 🔍 Notable Scripts

| File | Description |
|------|--------------|
| `FSM/StateMachine.cs` | Generic state machine used by both NPCs and enemies |
| `SceneLoader.cs` | Handles additive scene loading/unloading |
| `PersistentObjects.cs` | Manages cross-scene object persistence |
| `InventoryManager.cs` | Handles item stacking, slot validation, and overflow redistribution |
| `CraftingSystem.cs` | Consumes inventory items to create new ones (e.g., potions) |
| `PotionEffectHandler.cs` | Applies potion effects to health and mana systems |
| `NPCController.cs` | Implements AI state transitions and proximity logic |
| `DoorSystem.cs` | Manages scene transitions between doors |

---

## 📘 Future Plans

- Integrate save/load system  
- Expand crafting recipes and potion types  
- Add NPC dialogue and quest framework  
- Develop a level editor for modular world building  
- Implement complete menu and UI system  

---

## 🧑‍💻 Author

**Developer:** Shantanu Thatte  
**GitHub:** [ShantanuT26](https://github.com/ShantanuT26)  
**Email:** shadowshantanu@gmail.com  

---

## 🏷️ Notes

> This repository is intended to showcase **code quality**, **system architecture**, and **design approach** rather than gameplay completeness.  
> Reviewers are encouraged to explore the `Scripts` directory for insights into modular scene loading, FSM-driven AI, and the inventory–crafting interaction system.

