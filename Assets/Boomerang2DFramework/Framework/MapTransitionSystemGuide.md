# Map Transition System Setup Guide

## Overview
This system allows you to create seamless transitions between different levels/maps in your Boomerang 2D Framework game.

## How to Set Up Level Transitions

### 1. Using the Level Transition Actor

The `demoLevelTransition` actor can be placed in any map to create doorways or exits to other levels.

**To add a transition:**
1. Open the Map Editor (Tools > Boomerang2D > Map Editor)
2. Select your map (e.g., demoForest)
3. Place the `demoLevelTransition` actor where you want the exit
4. In the Actor properties, set the `targetLevel` stat to your destination:
   - "demoTown" - Goes to the town
   - "demoForest" - Goes to the forest
   - "demoOverworld" - Goes to the overworld map
   - "demoShmup" - Goes to the shmup level

### 2. Game Over System

The `demoGameOver` actor can be used to create hazards or enemies that reset the player back to level 1.

**To add game over triggers:**
1. Place `demoGameOver` actors as spikes, pits, or enemy death triggers
2. When the player touches them, they'll be sent back to "demoTown"

### 3. Setting Up Your Starting Level

**Option A: Use StartDemoGame (Simple)**
- Edit `StartDemoGame.cs` 
- Change `MapManager.LoadMap("demoTown")` to your desired starting level

**Option B: Use StartDemoGameEnhanced (Advanced)**
- Add `StartDemoGameEnhanced` component to your initialization GameObject
- Configure starting level in the Inspector
- Set up multiple levels and game flag resets

## Example Setup: Forest to Overworld Transition

1. **In demoForest.json:**
   - Place `demoLevelTransition` actor at position (190, 15) (near the right edge)
   - Set its `targetLevel` stat to "demoOverworld"

2. **In demoOverworld.json:**
   - Place `demoLevelTransition` actor at position (2, 15) (near the left edge)  
   - Set its `targetLevel` stat to "demoForest"

3. **Add some challenge:**
   - Place `demoGameOver` actors as obstacles in both levels
   - When player dies, they return to "demoTown" (level 1)

## Map Containers and Level Flow

The framework automatically creates Map Containers for each level:
- `MapContainer (demoTown)` - Level 1 (starting area)
- `MapContainer (demoForest)` - Forest level with exploration
- `MapContainer (demoOverworld)` - World map connecting areas
- `MapContainer (demoShmup)` - Special shmup-style level

## Customizing the System

### Adding New Levels
1. Create your map in Map Editor
2. Save as .json in Content/Maps/
3. Add the name to `availableLevels` array in StartDemoGameEnhanced
4. Create transitions using `demoLevelTransition` actors

### Custom Transition Logic
You can modify the `demoLevelTransition` actor to:
- Require keys or items
- Play transition animations
- Save player progress
- Show loading screens

### Game Over Variations
Modify `demoGameOver` to:
- Show death animations
- Subtract lives/health
- Go to different levels based on progress
- Display game over screens

## Testing Your Setup

1. Start the game - you should begin in your configured starting level
2. Walk into transition areas - you should seamlessly move between levels  
3. Touch game over triggers - you should return to level 1
4. Verify the map containers are created properly in the Unity Hierarchy during play

This system gives you a solid foundation for creating interconnected levels with proper game flow!