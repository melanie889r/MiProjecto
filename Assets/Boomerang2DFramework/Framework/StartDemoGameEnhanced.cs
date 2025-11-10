using Boomerang2DFramework.Framework.MapGeneration;
using UnityEngine;

namespace Boomerang2DFramework.Framework {
	/// <summary>
	/// Enhanced Start Demo Game script with multi-level support.
	/// Configure which level to start from in the Unity Inspector.
	/// </summary>
	public class StartDemoGameEnhanced : MonoBehaviour {
		[SerializeField] private string startingLevel = "demoForest";
		[SerializeField] private bool resetGameFlagsOnStart = true;
		
		[Header("Available Levels")]
		[SerializeField] private string[] availableLevels = {
			"demoTown",
			"demoForest", 
			"demoOverworld",
			"demoShmup"
		};

		private void Start() {
			Boomerang2D.InitializeFramework();
			
			if (resetGameFlagsOnStart) {
				// Reset game flags to default state
				ResetGameFlags();
			}
			
			// Load the starting level
			if (!string.IsNullOrEmpty(startingLevel)) {
				MapManager.LoadMap(startingLevel);
			} else {
				Debug.LogWarning("No starting level specified, loading default town.");
				MapManager.LoadMap("demoTown");
			}
		}
		
		private void ResetGameFlags() {
			// You can add specific flag resets here
			// For example: GameFlags.SetBoolFlag("canThrowBombs", false);
		}
		
		/// <summary>
		/// Call this method to change levels from code
		/// </summary>
		public void ChangeLevel(string levelName) {
			if (System.Array.Exists(availableLevels, level => level == levelName)) {
				MapManager.LoadMap(levelName);
			} else {
				Debug.LogError($"Level '{levelName}' not found in available levels list.");
			}
		}
		
		/// <summary>
		/// Call this to restart the current level
		/// </summary>
		public void RestartLevel() {
			if (MapManager.ActiveMapProperties != null) {
				MapManager.LoadMap(MapManager.ActiveMapProperties.Name);
			}
		}
		
		/// <summary>
		/// Call this to go back to the starting level (like level 1)
		/// </summary>
		public void ReturnToStartingLevel() {
			MapManager.LoadMap(startingLevel);
		}
	}
}