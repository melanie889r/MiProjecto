# Demo Town Level Transitions Guide

## How to Add Doors/Portals to Demo Town

The demo town is your central hub (Level 1). Here's how to add doorways to other levels:

### 🚪 **Door Types Available:**
- `demoForestDoor` - Takes player to demoForest
- `demoOverworldDoor` - Takes player to demoOverworld  
- `demoTownDoor` - Takes player back to demoTown (use in other levels)
- `demoLevelTransition` - Universal door (configurable target)

### 🏗️ **Adding Doors to Demo Town:**

#### **Method 1: Using Map Editor (Recommended)**
1. Open **Map Editor** (Tools > Boomerang2D > Map Editor)
2. Load `demoTown` map
3. Switch to the **Actors** layer
4. Place door actors at desired locations:

**Suggested Placements:**
- **Forest Door**: Position (400, 300) - Near town center
- **Overworld Door**: Position (800, 200) - Right side of town
- **Return Door**: Position (300, 350) - Near spawn point

#### **Method 2: Direct JSON Edit**
Add to the Actors array in `demoTown.json`:

```json
{
    "Actor": "demoForestDoor",
    "MapId": "",
    "Position": {
        "x": 400,
        "y": 300
    },
    "ActorInstanceProperties": {
        "FacingDirection": 3,
        "LinkedActors": []
    },
    "ActorDefaultStatsFloats": [],
    "ActorDefaultStatsBools": [],
    "ActorDefaultStatsStrings": []
},
{
    "Actor": "demoOverworldDoor", 
    "MapId": "",
    "Position": {
        "x": 800,
        "y": 200
    },
    "ActorInstanceProperties": {
        "FacingDirection": 3,
        "LinkedActors": []
    },
    "ActorDefaultStatsFloats": [],
    "ActorDefaultStatsBools": [],
    "ActorDefaultStatsStrings": []
}
```

### 🔄 **Setting Up Return Doors in Other Levels:**

#### **In demoForest.json:**
Add near the entrance:
```json
{
    "Actor": "demoTownDoor",
    "Position": {"x": 50, "y": 400}
}
```

#### **In demoOverworld.json:**
Add at entry point:
```json
{
    "Actor": "demoTownDoor", 
    "Position": {"x": 100, "y": 450}
}
```

### 🎮 **Level Flow Setup:**

**Town (Level 1) → Other Levels:**
- Walk into `demoForestDoor` → Go to Forest
- Walk into `demoOverworldDoor` → Go to Overworld

**Other Levels → Town (Level 1):**
- Walk into `demoTownDoor` → Return to Town
- Touch `demoGameOver` actor → Return to Town (game over)

### 🗺️ **Complete Level Network:**

```
┌─────────────┐    ┌──────────────┐    ┌─────────────┐
│  demoTown   │────│ demoForest   │────│demoOverworld│
│  (Level 1)  │    │              │    │ (World Map) │
│  - Hub      │    │ - Adventure  │    │ - Connects  │
│  - Safe     │    │ - Dangers    │    │   Areas     │
│  - NPCs     │    │ - Items      │    │ - Overview  │
└─────────────┘    └──────────────┘    └─────────────┘
       │                    │                   │
       └────────────────────┼───────────────────┘
                           │
                    ┌──────────────┐
                    │   demoShmup  │
                    │ (Special)    │
                    │ - Mini-game  │
                    └──────────────┘
```

### 🔧 **Visual Indicators:**

**Make doors visible by:**
1. **Use Tile Graphics**: Place door tiles from tileset
2. **Custom Sprites**: Assign door sprites to actors
3. **Signs**: Add text signs near doors ("To Forest", "To World Map")
4. **NPCs**: Place NPCs near doors to explain transitions

### 🚩 **Important Notes:**

- **Player Spawn**: Each level needs a spawn point for the player
- **Consistent Position**: Place return doors near level entrances  
- **Game Flow**: Town should be the central safe hub
- **Visual Clarity**: Make doors obvious to players
- **Testing**: Test all transitions work in both directions

### 📍 **Current Town Layout:**
- **Player Spawn**: (336, 336) - `demoTownPlayerSpawner`
- **Existing Doors**: (144, 432) and (1008, 112) - `demoDoor`
- **NPCs**: (608, 224) and (288, 272) - Dialog characters

**Add your level transition doors near these existing elements for a cohesive town layout!**

This system creates a proper hub-and-spoke level design where the town serves as the central safe area that connects to all other levels.