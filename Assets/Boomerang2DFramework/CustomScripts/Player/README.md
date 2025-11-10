# Player Controller with Kick, Projectile & Score System

## What I Added

### 1. **Projectile System** (`ProjectileBehavior.cs`)
- Throwable objects that fly in a direction
- Can return to the player like a boomerang (set `ReturnsToOwner = true`)
- Configurable lifetime, speed, and return timing
- Automatically rotates to face movement direction

### 2. **Status Effects** (`StatusEffect.cs`, `StatusEffectManager.cs`)
- Base `StatusEffect` class for creating timed effects
- `BleedEffect` that damages actors over time
- `StatusEffectManager` component manages active effects on an actor
- Effects auto-expire after duration

### 3. **Player Controller** (`PlayerController.cs`)
- Complete input handling for movement, jump, kick, and throw
- **Kick (K key)**: Pushes nearby actors and applies bleed effect
- **Throw (F key)**: Spawns a returning projectile
- **Pickup Detection**: Automatically collects actors with "pickup" or "coin" flags and adds score

### 4. **Score System** (Already added)
- `ScoreManager` - static manager with save/load
- `ScoreDisplay` - on-screen display component

## How to Use

### Quick Setup (Unity Editor)
1. **Add Score Display**:
   - Select the `Boomerang2D` GameObject
   - Add Component → `ScoreDisplay`

2. **Add Player Controller**:
   - Select the `Boomerang2D` GameObject
   - Add Component → `PlayerController`
   - Configure speeds in Inspector

3. **Test in Play Mode**:
   - Arrow Keys = Move
   - Space = Jump
   - K = Kick (pushes & bleeds nearby enemies)
   - F = Throw projectile

### Creating Pickups
Make an Actor with a bounding box that has the flag "pickup" or "coin". When the player overlaps it, they'll collect it for +10 score.

### Customizing
- **Projectile sprite**: Assign `projectileSprite` in PlayerController Inspector
- **Bleed damage**: Edit `BleedEffect` constructor in `HandleKick()` (currently 5 damage/sec for 5 seconds)
- **Score per pickup**: Edit `ScoreManager.Add(10)` in `HandlePickups()`

## Code Locations
```
Assets/Boomerang2DFramework/
├── Framework/
│   ├── Actors/
│   │   ├── Projectiles/
│   │   │   └── ProjectileBehavior.cs
│   │   └── StatusEffects/
│   │       ├── StatusEffect.cs
│   │       └── StatusEffectManager.cs
│   ├── ScoreManagement/
│   │   ├── ScoreManager.cs
│   │   └── ScoreDisplay.cs
│   └── GameEvents/Events/
│       ├── AddScore.cs
│       └── AddScoreProperties.cs
└── CustomScripts/
    └── Player/
        └── PlayerController.cs
```

## Advanced: Using Weapons for Kick
If you want a more sophisticated kick:
1. Create a Weapon in the Weapon Studio named "Kick"
2. Add it to your player's Actor JSON
3. The controller will automatically use `GetWeaponBehavior("Kick").Attack()` instead of the fallback push

## Notes
- Projectiles use the "Weapon" layer for collision detection
- Bleed looks for a stat named "health" or "hp" in the actor's float stats
- Score persists to PlayerPrefs automatically on each collection
