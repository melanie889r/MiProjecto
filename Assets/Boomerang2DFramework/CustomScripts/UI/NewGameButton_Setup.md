# How to Set Up "New Game" Button

## 🎮 Quick Setup Guide

### Step 1: Create the Button in Unity
1. **Right-click in Hierarchy** → UI → Button - TextMeshPro
2. **Rename** the button to "NewGameButton"
3. **Change the text** to "New Game"
4. **Position** the button where you want it

### Step 2: Connect to Menu3D
1. **Select** the GameObject with the **Menu3D** script
2. In the **Inspector**, find **Menu Buttons** section
3. **Drag** your "NewGameButton" to the **New Game Button** field

### Step 3: Set Scene Name (Already Done!)
- The script is already set to load **"Boomerang2D"** scene
- This matches your Boomerang scene name

### Step 4: Alternative Setup (Using Button Inspector)
If you prefer to set it up manually:

1. **Select** your "New Game" button
2. **Scroll down** to **Button (Script)** component
3. **Click the + button** in **On Click ()** section
4. **Drag** the GameObject with Menu3D script to the object field
5. **Select** `Menu3D.OnNewGameButtonClick()` from dropdown

## ⌨️ Keyboard Shortcut
- **Enter** or **Space** = Also starts new game
- **1** = Quick start new game

## 🔧 Current Settings:
```
Game Scene Name: "Boomerang2D"
New Game Button: Drag your button here
```

## ✅ Test It:
1. **Press Play** in Unity
2. **Click "New Game"** button
3. Should load the **Boomerang2D** scene

## 🚨 Important:
Make sure **"Boomerang2D"** scene is added to **Build Settings**:
- File → Build Settings
- Add Open Scenes (or drag scene files)

That's it! Your New Game button will now load the Boomerang scene when clicked! 🎉