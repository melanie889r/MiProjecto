# Menu3D Scene Management System

## 🎮 What is Menu3D?

A complete 3D menu system that can load different scenes in your Unity project. Perfect for main menus, settings screens, and scene transitions.

## 🚀 Quick Setup

### Step 1: Add to Scene
1. Create an empty GameObject in your menu scene
2. Add Component → `Menu3D`
3. Configure the scene names in the Inspector

### Step 2: Set Scene Names
In the Menu3D inspector, set these fields:
- **Game Scene Name**: "GameScene" (your main game scene)
- **Settings Scene Name**: "SettingsScene" 
- **Credits Scene Name**: "CreditsScene"
- **Main Menu Scene Name**: "MainMenu"

### Step 3: Add to Build Settings
1. Go to File → Build Settings
2. Add all your scenes to "Scenes in Build"
3. Make sure scene names match exactly

## 🎛️ Controls

### Keyboard Shortcuts:
- **Enter/Space** = Start Game
- **Escape** = Quit (from main menu) or Back (from other scenes)
- **1** = Load Game Scene
- **2** = Load Settings Scene  
- **3** = Load Credits Scene

### UI Buttons (Optional):
If you have UI buttons, drag them to these fields:
- `playButton` → Loads game scene
- `settingsButton` → Loads settings scene
- `creditsButton` → Loads credits scene
- `quitButton` → Quits application
- `backButton` → Returns to main menu

## 🎨 3D Menu Features

### Auto-Rotating Objects:
- Drag 3D objects to `menuObjects` array
- They'll automatically rotate around Y-axis
- Adjust `rotationSpeed` to control speed
- Uncheck `autoRotateMenu` to disable

### Audio Support:
- Drag AudioSource to `audioSource` field
- Set `buttonClickSound` for button feedback
- Set `backgroundMusic` for menu music

## 📝 Code Examples

### Load Scene from Code:
```csharp
Menu3D menu = FindObjectOfType<Menu3D>();
menu.LoadScene("MyScene");
```

### Async Loading with Progress:
```csharp
menu.LoadSceneAsync("MyScene");
```

### Reload Current Scene:
```csharp
menu.ReloadCurrentScene();
```

## 🔧 Advanced Usage

### Button Events in Inspector:
1. Create UI Button
2. In Button's OnClick event
3. Drag the GameObject with Menu3D
4. Select function like `Menu3D.OnPlayButtonClick()`

### Custom Scene Loading:
```csharp
public class MyCustomMenu : MonoBehaviour {
    public Menu3D menu3D;
    
    public void LoadCustomScene() {
        menu3D.LoadScene("MyCustomScene");
    }
}
```

## 🛠️ Error Handling

- Shows error message if scene not found
- Checks if scene exists in build settings
- Graceful fallback for missing audio/UI elements
- Debug logs for troubleshooting

## 💡 Tips

1. **Scene Names**: Must match exactly (case-sensitive)
2. **Build Settings**: Always add scenes to build settings first
3. **Testing**: Use keyboard shortcuts for quick testing
4. **Audio**: AudioSource can be on same GameObject or child
5. **UI**: Works with both 3D objects and UI Canvas

## 🎯 Common Use Cases

- **Main Menu**: Navigate to game, settings, credits
- **Pause Menu**: Return to main menu or restart level
- **Game Over**: Restart or return to menu
- **Level Select**: Load specific game levels
- **Settings Menu**: Return to previous scene

Ready to use! Just attach to a GameObject and configure the scene names. 🚀