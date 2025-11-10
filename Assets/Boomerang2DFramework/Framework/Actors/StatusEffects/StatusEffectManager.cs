using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Boomerang2DFramework.Framework.GlobalTimeManagement;

namespace Boomerang2DFramework.Framework.Actors.StatusEffects {
    /// <summary>
    /// Manages status effects for an Actor. Attach to Actor GameObject or manage via Actor extension.
    /// </summary>
    public class StatusEffectManager : MonoBehaviour {
        private readonly List<StatusEffect> _activeEffects = new List<StatusEffect>();

        public void AddEffect(StatusEffect effect) {
            // Check if same type already exists
            var existing = _activeEffects.FirstOrDefault(e => e.GetType() == effect.GetType());
            if (existing != null) {
                // Refresh duration
                existing.TimeRemaining = effect.Duration;
                return;
            }

            effect.Apply();
            _activeEffects.Add(effect);
        }

        public void RemoveEffect(StatusEffect effect) {
            if (_activeEffects.Contains(effect)) {
                effect.Remove();
                _activeEffects.Remove(effect);
            }
        }

        public bool HasEffect<T>() where T : StatusEffect {
            return _activeEffects.Any(e => e is T);
        }

        private void Update() {
            float dt = GlobalTimeManager.DeltaTime;
            
            for (int i = _activeEffects.Count - 1; i >= 0; i--) {
                StatusEffect effect = _activeEffects[i];
                effect.Update(dt);
                
                if (effect.IsExpired()) {
                    effect.Remove();
                    _activeEffects.RemoveAt(i);
                }
            }
        }
    }
}
