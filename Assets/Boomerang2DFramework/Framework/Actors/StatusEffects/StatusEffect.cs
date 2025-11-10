using System.Collections.Generic;
using UnityEngine;
using Boomerang2DFramework.Framework.GlobalTimeManagement;

namespace Boomerang2DFramework.Framework.Actors.StatusEffects {
    /// <summary>
    /// Base class for status effects that can be applied to Actors.
    /// </summary>
    public abstract class StatusEffect {
        public Actor Target;
        public float Duration;
        public float TimeRemaining;
        
        public abstract void Apply();
        public abstract void Update(float deltaTime);
        public abstract void Remove();
        
        public bool IsExpired() => TimeRemaining <= 0;
    }

    /// <summary>
    /// Status effect that damages the target over time.
    /// </summary>
    public class BleedEffect : StatusEffect {
        public int DamagePerTick = 5;
        public float TickInterval = 1f; // Damage every second
        private float _nextTickTime = 0f;

        public BleedEffect(Actor target, float duration, int damagePerTick = 5) {
            Target = target;
            Duration = duration;
            TimeRemaining = duration;
            DamagePerTick = damagePerTick;
        }

        public override void Apply() {
            Debug.Log($"{Target.ActorProperties?.Name ?? "Actor"} is bleeding!");
            _nextTickTime = TickInterval;
        }

        public override void Update(float deltaTime) {
            TimeRemaining -= deltaTime;
            _nextTickTime -= deltaTime;

            if (_nextTickTime <= 0) {
                // Apply damage tick
                if (Target != null && Target.ActorProperties != null) {
                    // Find health stat and reduce it
                    var healthStat = Target.ActorProperties.StatsFloats.Find(s => 
                        s.Name.ToLower() == "health" || s.Name.ToLower() == "hp"
                    );
                    
                    if (healthStat != null) {
                        healthStat.Value -= DamagePerTick;
                        Debug.Log($"{Target.ActorProperties.Name} takes {DamagePerTick} bleed damage! Health: {healthStat.Value}");
                    }
                }
                
                _nextTickTime = TickInterval;
            }
        }

        public override void Remove() {
            Debug.Log($"{Target.ActorProperties?.Name ?? "Actor"} stopped bleeding.");
        }
    }
}
