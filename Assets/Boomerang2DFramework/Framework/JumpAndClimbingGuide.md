# Jump and Climbing System Guide

## 🚀 Jump Height Configuration

### Jump Strength Values Guide:
- `5.625` - Default/Normal jump
- `8.0` - Medium jump (1.4x higher)
- `12.0` - High jump (2x higher) 
- `15.0` - Super jump (2.7x higher)
- `20.0` - Ultra jump (3.5x higher)
- `25.0+` - Moon jump territory

### How to Modify Jump Height:

**Option 1: Using Actor Studio**
1. Tools > Boomerang2D > Actor Studio
2. Select your actor (e.g., `demoPlayer`)
3. Find "Jump" state
4. Modify `JumpStrength` value
5. Save

**Option 2: Direct JSON Edit**
Edit the actor's JSON file and find the Jump state:
```json
"Properties": "{\"JumpStrength\":{\"StartValue\":12.0,...}"
```

## 🧗 Climbing Systems

### 1. Ladder Climbing
- **Built-in**: Always available for player
- **Requirements**: Ladder tiles in tileset
- **Controls**: 
  - Up Arrow: Climb up
  - Down Arrow: Climb down
- **Auto-detected**: Player automatically enters ladder climbing when touching ladder tiles

### 2. Wall Climbing
- **Requires**: `canWallClimb` game flag = true
- **Also needs**: `canWallGrab` game flag = true (for wall grabbing)
- **Controls**:
  - Touch wall while jumping to grab
  - Up Arrow: Climb up wall
  - Down Arrow: Slide down wall
  - Jump: Jump off wall

### 3. Double Jump
- **Requires**: `canDoubleJump` game flag = true
- **Usage**: Press jump again while in air
- **Combines with**: Wall climbing for advanced movement

## 🎮 Quick Setup for Super Movement

### Place the Super Jump Item:
1. Open Map Editor
2. Place `demoSuperJumpItem` in your level
3. When player collects it, they get:
   - Higher jump (12.0 strength)
   - Wall climbing ability
   - Wall grabbing ability
   - Double jump ability

### Manual Flag Setup:
Add this to your initialization code:
```csharp
GameFlags.SetBoolFlag("canWallClimb", true);
GameFlags.SetBoolFlag("canWallGrab", true);
GameFlags.SetBoolFlag("canDoubleJump", true);
```

## 🏗️ Advanced Jump Configurations

### Variable Jump Height by Stat:
You can make jump height depend on actor stats:
```json
"JumpStrength": {
  "StartValue": 5.0,
  "Entries": [
    {
      "Operation": 0,
      "Value": 1,
      "UsesSubValueString": true,
      "SubValueString": "JumpPower"
    }
  ]
}
```

### Gravity Settings:
- `0.4375` - Default gravity
- `0.2` - Floaty/moon-like
- `0.8` - Heavy/fast falling
- `1.2+` - Very heavy

## 🎯 Common Jump/Climb Combinations

### Platform Game Setup:
- Jump Strength: `8.0`
- Gravity: `0.4375` 
- Enable double jump
- Enable wall grab

### Exploration Game Setup:
- Jump Strength: `12.0`
- Gravity: `0.3`
- Enable wall climbing
- Enable double jump

### Metroidvania Setup:
- Start with Jump Strength: `5.625`
- Collect items to upgrade to `12.0`
- Progressively unlock climbing abilities

## 🐛 Troubleshooting

### Jump Not Working:
- Check if actor has Jump state
- Verify Jump input is configured (usually Space or Up)
- Make sure actor can transition from current state to Jump

### Wall Climbing Not Working:
- Verify `canWallClimb` flag is true
- Check `canWallGrab` flag is also true
- Ensure walls have proper collision geometry
- Player must be touching wall geometry

### Ladder Climbing Not Working:
- Check if tileset has ladder tiles defined
- Verify ladder tiles are placed in collision layer
- Make sure player has ClimbingLadder state

This system gives you complete control over movement abilities and can create engaging progression systems!