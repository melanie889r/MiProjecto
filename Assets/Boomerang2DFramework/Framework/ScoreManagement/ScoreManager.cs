using System;
using UnityEngine;

namespace Boomerang2DFramework.Framework.ScoreManagement {
    /// <summary>
    /// Simple static score manager. Stores score in PlayerPrefs under key "B2D_Score".
    /// Provides events for UI to subscribe to.
    /// </summary>
    public static class ScoreManager {
        private const string PlayerPrefsKey = "B2D_Score";
        private static int _score;

        /// <summary>
        /// Fired when the score changes. Parameter is the new score.
        /// </summary>
        public static event Action<int> OnScoreChanged;

        static ScoreManager() {
            Load();
        }

        public static int GetScore() {
            return _score;
        }

        public static void Add(int amount, bool save = true) {
            if (amount == 0) return;
            _score += amount;
            OnScoreChanged?.Invoke(_score);
            if (save) Save();
        }

        public static void Reset(bool save = true) {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
            if (save) Save();
        }

        public static void Save() {
            try {
                PlayerPrefs.SetInt(PlayerPrefsKey, _score);
                PlayerPrefs.Save();
            } catch (Exception e) {
                Debug.LogWarning("ScoreManager: Failed to save score: " + e.Message);
            }
        }

        public static void Load() {
            _score = PlayerPrefs.GetInt(PlayerPrefsKey, 0);
            OnScoreChanged?.Invoke(_score);
        }
    }
}
