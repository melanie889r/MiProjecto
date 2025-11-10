using UnityEngine;
using Boomerang2DFramework.Framework.GlobalTimeManagement;

namespace Boomerang2DFramework.Framework.Actors.Projectiles {
    /// <summary>
    /// Projectile that flies in a direction and optionally returns to the shooter.
    /// Attach to a GameObject spawned by an Actor attack.
    /// </summary>
    public class ProjectileBehavior : MonoBehaviour {
        public Actor Owner;
        public Vector2 Velocity;
        public float Lifetime = 5f;
        public bool ReturnsToOwner = false;
        public float ReturnSpeed = 8f;
        public float ReturnStartTime = 1f; // When to start returning
        public int Damage = 10;
        public string[] HitFlags = new string[0]; // Bounding box flags this projectile hits
        
        private float _timeAlive = 0f;
        private bool _isReturning = false;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;

        public void Initialize(Actor owner, Vector2 velocity, Sprite sprite = null) {
            Owner = owner;
            Velocity = velocity;
            
            if (_spriteRenderer == null) {
                _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
            
            if (sprite != null) {
                _spriteRenderer.sprite = sprite;
            }
            
            if (_collider == null) {
                _collider = gameObject.AddComponent<BoxCollider2D>();
                _collider.isTrigger = true;
            }
            
            gameObject.layer = LayerMask.NameToLayer("Weapon");
        }

        private void Update() {
            float dt = GlobalTimeManager.DeltaTime;
            _timeAlive += dt;

            if (_timeAlive >= Lifetime) {
                Destroy(gameObject);
                return;
            }

            // Start returning after a delay
            if (ReturnsToOwner && !_isReturning && _timeAlive >= ReturnStartTime) {
                _isReturning = true;
            }

            if (_isReturning && Owner != null) {
                // Move towards owner
                Vector2 toOwner = (Vector2)Owner.RealPosition - (Vector2)transform.position;
                if (toOwner.magnitude < 0.5f) {
                    // Caught by owner
                    Destroy(gameObject);
                    return;
                }
                Velocity = toOwner.normalized * ReturnSpeed;
            }

            transform.position += (Vector3)Velocity * dt;
            
            // Rotate to face direction
            if (Velocity.magnitude > 0.1f) {
                float angle = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            // Check if we hit an actor
            ActorBehavior hitActor = other.GetComponent<ActorBehavior>();
            if (hitActor != null && hitActor.Actor != Owner) {
                // Apply damage or status effects here
                // For now just destroy the projectile on hit
                if (!ReturnsToOwner) {
                    Destroy(gameObject);
                }
            }
        }
    }
}
