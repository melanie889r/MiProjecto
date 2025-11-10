# Boomerang 2D Framework - Adventure Game

A complete 2D game framework built in Unity with a demo adventure game showcasing actors, maps, and game systems.

## 🎮 About the Game

This project is a **Boomerang 2D Framework** - a Unity-based engine for creating 2D adventure games. It includes a demo game featuring:

- **Actor System**: Player and NPC characters with behaviors
- **Map System**: Multiple interconnected levels (Town, Forest)
- **Game Events**: Data-driven event system
- **UI Management**: HUD elements, menus, and dialogs
- **State Management**: Game state and flag persistence

## 🚀 Getting Started

### Prerequisites

- Unity 2021.3 or later
- Git (for version control)
- Windows, Mac, or Linux

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/melanie889r/MiProjecto.git
   cd MiProjecto
   ```

2. **Open in Unity:**
   - Open Unity Hub
   - Click "Add" and select the project folder
   - Open the project

3. **Open the main scene:**
   - Navigate to `Assets/PART1.unity` or `Assets/menuscripts/MainMenus.unity`
   - Double-click to open

4. **Play the game:**
   - Click the Play button in Unity Editor

## 📚 Framework Overview

### Core Systems

#### 1. **Actors System** (`Assets/Boomerang2DFramework/Framework/Actors/`)

Actors are the characters in your game (players, NPCs, enemies).

**Step-by-step to create an Actor:**

1. Create actor JSON definition in your resources
2. Define properties:
   - `actorId`: Unique identifier
   - `sprite`: Visual representation
   - `behavior`: Movement and AI logic
   - `stats`: Health, speed, etc.

3. Use `ActorManager` to spawn actors:
   ```csharp
   ActorManager.SpawnActor("actorId", position);
   ```

**Key Components:**
- `Actor.cs` - Base actor class
- `ActorBehavior.cs` - AI and movement logic
- `ActorProperties.cs` - Actor data definition
- `ActorManager.cs` - Spawning and lifecycle management

#### 2. **Map System** (`Assets/Boomerang2DFramework/Framework/MapGeneration/`)

Maps define the game world layout.

**Step-by-step to create a Map:**

1. Create a new scene or use existing map containers
2. Define map JSON:
   - `mapId`: Unique map identifier
   - `tilesets`: Visual tile assets
   - `layers`: Terrain, collision, decoration
   - `triggers`: Interactive regions

3. Load maps using:
   ```csharp
   MapManager.LoadMap("mapId");
   ```

**Key Features:**
- Tile-based rendering
- Collision detection
- Map transitions
- Trigger zones

#### 3. **Game Events** (`Assets/Boomerang2DFramework/Framework/GameEvents/`)

Data-driven events that control game logic.

**Step-by-step to create Events:**

1. Define event JSON:
   ```json
   {
     "eventClass": "DialogEvent",
     "parameters": {
       "text": "Hello, adventurer!",
       "speaker": "NPC_Guard"
     }
   }
   ```

2. Trigger events:
   ```csharp
   GameEventBuilder.BuildEvent(eventJson);
   ```

**Available Event Types:**
- `DialogEvent` - Show conversation
- `MoveActorEvent` - Move character
- `ChangeMapEvent` - Transition to new map
- `ToggleGameFlagBool` - Set game flags
- And many more...

#### 4. **UI Management** (`Assets/Boomerang2DFramework/Framework/UiManagement/`)

Handles HUD, menus, and UI elements.

**Step-by-step for UI:**

1. Create HUD elements (health bars, inventory icons)
2. Define in JSON or inspector
3. Use `UiManager` to show/hide:
   ```csharp
   UiManager.ShowHudObject("healthBar");
   ```

**UI Elements:**
- `TextBox` - Dialog boxes
- `ChoiceBox` - Player choices
- `GraphicGrid` - Inventory/menu grids
- `SingleGraphic` - Icons and images

#### 5. **Game Flags** (`Assets/Boomerang2DFramework/Framework/GameFlagManagement/`)

Persistent game state (quest progress, switches, etc.).

**Usage:**
```csharp
// Set a flag
GameFlags.SetFlagValue("quest_started", true);

// Get a flag
bool questStarted = GameFlags.GetFlagBool("quest_started");
```

## 🎯 Creating Your First Adventure

### Step 1: Initialize the Framework

Add to your startup scene:

```csharp
using Boomerang2DFramework;

public class GameStarter : MonoBehaviour {
    void Start() {
        Boomerang2D.InitializeFramework();
    }
}
```

### Step 2: Create Your Player Actor

1. Define player actor JSON
2. Set up sprites and animations
3. Add player behavior script
4. Configure input controls

### Step 3: Build Your First Map

1. Create tileset assets
2. Design map layout in Unity
3. Add collision tiles
4. Set spawn points

### Step 4: Add NPCs and Interactions

1. Create NPC actors
2. Define dialog events
3. Set up trigger zones
4. Link events to triggers

### Step 5: Implement Game Logic

1. Set up game flags for progress
2. Create quest events
3. Add map transitions
4. Build UI elements

## 📁 Project Structure

```
Assets/
├── Boomerang2DFramework/          # Main framework code
│   └── Framework/
│       ├── Actors/                # Character system
│       ├── MapGeneration/         # Map and tile system
│       ├── GameEvents/            # Event system
│       ├── GameFlagManagement/    # Save state
│       ├── UiManagement/          # UI system
│       ├── Camera/                # Camera control
│       └── Utilities/             # Helper functions
├── menuscripts/                   # Game menu scripts
├── PART1.unity                    # Demo scene
└── TextMesh Pro/                  # Text rendering
```

## 🎮 Demo Game Controls

- **Arrow Keys / WASD**: Move player
- **Space / Enter**: Interact
- **ESC**: Pause menu

## 🛠️ Configuration

### GameProperties.cs

Core framework settings:
- Screen resolution
- Physics settings
- Input mappings
- Custom script namespaces

### BoomerangDatabase

Indexes all game content:
- Actors
- Maps
- Events
- UI elements

**To rebuild:** Tools → Boomerang2D → Build Reference Database

## 📖 Documentation

- `Assets/Boomerang2DFramework/Getting Started.txt` - Quick start guide
- `Assets/Boomerang2DFramework/Framework/MapTransitionSystemGuide.md` - Map transitions
- `Assets/Boomerang2DFramework/Framework/TownLevelTransitionsGuide.md` - Level design

## 🐛 Troubleshooting

### Common Issues

**Actors not spawning:**
- Check BoomerangDatabase is populated
- Verify actor JSON is valid
- Ensure sprites are assigned

**Maps not loading:**
- Rebuild reference database
- Check map JSON format
- Verify tileset assets exist

**Events not triggering:**
- Confirm event class names match
- Check GameFlags conditions
- Verify trigger collider setup

## 🤝 Contributing

This is a framework project. To extend:

1. Add new actor behaviors in `CustomScripts` namespace
2. Create custom event types
3. Build new UI elements
4. Design maps and content

## 📝 License

This project uses the Boomerang 2D Framework. Check individual asset licenses in their respective folders.

## 🙏 Credits

- **Framework**: Boomerang 2D Framework
- **TextMesh Pro**: Unity Technologies
- **Demo Assets**: Various (see attribution files)

## 🔗 Links

- GitHub Repository: https://github.com/melanie889r/MiProjecto
- Unity Asset Store: [Search for Boomerang 2D]

## 📧 Contact

For questions or support, open an issue on GitHub.

---

**Happy Game Making! 🎮✨**
