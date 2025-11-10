using UnityEngine;
using Boomerang2DFramework.Framework.ScoreManagement;

namespace Boomerang2DFramework.Framework.ScoreManagement {
    /// <summary>
    /// Simple on-screen score display for quick testing. Uses OnGUI so no UI setup is required.
    /// Drop this on any active GameObject (for example the Boomerang2D GameObject).
    /// </summary>
    public class ScoreDisplay : MonoBehaviour {
        public Vector2 Position = new Vector2(10, 10);
        public int FontSize = 18;

        private int _score;

        private void OnEnable() {
            ScoreManager.OnScoreChanged += OnScoreChanged;
            _score = ScoreManager.GetScore();
        }

        private void OnDisable() {
            ScoreManager.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int newScore) {
            _score = newScore;
        }

        private void OnGUI() {
            GUIStyle s = new GUIStyle(GUI.skin.label) {
                fontSize = FontSize,
                normal = {textColor = Color.white}
            };

            GUI.Label(new Rect(Position.x, Position.y, 300, 30), "Score: " + _score, s);
        }
    }
}
