namespace Boomerang2DFramework.Framework.GameEvents.Events {
    [System.Serializable]
    public class AddScoreProperties : GameEventProperties {
        public int Amount = 0;
        /// <summary>
        /// Whether to persist the score to PlayerPrefs when adding.
        /// </summary>
        public bool Save = true;
    }
}
