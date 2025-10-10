# Summer 2024 Game Project

A 2D platformer built in **Unity (C#)** featuring modular scene loading, finite state machine (FSM)–based AI, and dynamic NPC behavior.

---

## 🧭 Overview

This project was developed during **Summer 2024** as part of an independent game development initiative.  
The goal was to build a scalable, extensible 2D platformer framework with:

- **Additive scene loading** to create a seamless world  
- **Finite State Machine (FSM)** architecture for both enemies and friendly NPCs  
- **Persistent objects** that survive scene transitions  
- **Door-based transport system** to dynamically load/unload areas  
- **Animation-driven interactions** for characters and environment

Although the game is **not currently in a playable state**, the repository demonstrates significant progress in engine architecture, scene management, and AI design patterns.

---

## 🧩 Key Technical Highlights

- **Language & Engine:** Unity 2022.3 (C#)
- **Design Patterns Used:**  
  - Finite State Machine (FSM)  
  - Object Pooling  
  - Singleton  
  - Event System / Observer Pattern
- **Core Systems Implemented:**  
  - Additive scene management (`LoadSceneAsync`)  
  - Door and teleport system with destination mapping  
  - Persistent player and world data between scenes  
  - NPC AI with proximity triggers and behavioral states  
  - Modular animation controller setup

---

## 📂 Repository Structure

