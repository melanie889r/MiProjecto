using Boomerang2DFramework.Framework.ScoreManagement;

namespace Boomerang2DFramework.Framework.GameEvents.Events {
    public class AddScore : GameEvent {
        private AddScoreProperties Props => (AddScoreProperties) Properties;

        public AddScore(GameEventProperties properties) {
            Properties = properties;
        }

        public override void ApplyOutcome() {
            if (Props == null) return;
            ScoreManager.Add(Props.Amount, Props.Save);
        }
    }
}
