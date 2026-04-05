# Inventory & Scrap System | Vertical Slice

This project demonstrates a robust inventory management system, procedural environment population, and a dedicated item recycling (Scrap) mechanic.

---

## Tech Stack
* **Unity 6.3+** (Universal Render Pipeline)
* **New Input System** (Action-based)
* **DOTween** (Juicy UI & VFX animations)
* **TextMeshPro** (Rich Text support & SDF fonts)

---

## Architectural Overview
The project follows **Data-Driven Design** and **Event-Driven UI** principles to ensure scalability and clean code separation.

* **Domain-Based Structure:** Logic is organized into functional modules: `Core`, `Data`, `Logic`, and `UI`.
* **Loose Coupling:** Communication between core logic and UI is handled via C# `Actions` to minimize dependencies.
* **Entity Uniqueness:** Every `ItemInstance` is assigned a **GUID** upon creation to prevent duplication bugs and ensure precise selection tracking in the Scrap Mode.

---

## Core Components

### Items & Inventory
* **`InventoryController`**: A singleton manager handling item data, capacity constraints, and the Scrap currency.
* **`LootGenerator`**: A procedural engine that instantiates items based on weighted rarity and randomized modifiers.
* **`ItemInstance`**: A serializable class representing a unique item in the world, holding reference to its base data and generated stats.

### Gameplay & World
* **`PropZoneSpawner`**: Automates environment population using Gizmo-visualized bounds for intuitive level design.
* **`PlayerInputProvider`**: A centralized facade for reading movement, interaction, and UI inputs across the project.
* **`ScrapTable`**: An interactive world object that triggers a specialized batch-processing UI mode for item recycling.
* **`BreakableObject`**: Logic for object destruction featuring synchronized Animation Events and custom Particle Systems.

---

## ScriptableObject (SO) Workflow
The system utilizes SOs for easy balancing without code modifications:

| ScriptableObject | Purpose | Key Parameters |
| :--- | :--- | :--- |
| **`BaseItemData`** | Item Blueprint | Name, Category, Icon, Prefab, Description |
| **`RaritySettings`** | Rarity Config | UI Colors, Scrap Multipliers, Visual Effects |
| **`InventoryData`** | Data Container | Persistent items list, Capacity limits |
| **`Modifier`** | Modifier Config | Different types of item modification |

---

## Controls
* **WASD**: Character Movement.
* **LMB (Left Mouse Button)**: Attack / Destroy.
* **E**: Interact (Pick up items / Use Scrap Table).
* **TAB**: Toggle Inventory.
* **Mouse Pointer**: UI Navigation & Tooltips.

---

## Juice & Polish
* **Dynamic Feedback:** `DOPunchScale` and `DOShake` animations for "Inventory Full" warnings and currency updates.
* **Contextual Tooltips:** Item descriptions follow the cursor and dynamically adjust styling based on item rarity.
* **Rich Text Interaction:** Keybindings (e.g., "Press <color="yellow">E</color>") are highlighted using color tags for better UX readability.
* **Visual Feedback:** Mesh-based particle systems for realistic wood splintering effects upon box destruction.

---

## How to Extend
1.  **Add a New Item:** Create a `BaseItemData` asset in `Resources/Items`, set parameters, and assign a sprite.
2.  **Adjust Balance:** Edit the `RaritySettings` asset to change scrap values or UI colors globally.
3.  **Add Modifiers:** Create the `Modifier` (Positive, Negative, Neutral) asset to make item generation more unique.
4.  **New Prop Zones:** Place a `PropZoneSpawner` prefab in the scene and adjust the Gizmo box to define the spawning area.
