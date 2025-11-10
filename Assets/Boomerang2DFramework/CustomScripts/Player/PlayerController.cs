using UnityEngine;
using Boomerang2DFramework.Framework;
using Boomerang2DFramework.Framework.Actors;
using Boomerang2DFramework.Framework.Actors.Projectiles;
using Boomerang2DFramework.Framework.Actors.StatusEffects;
using Boomerang2DFramework.Framework.ScoreManagement;

namespace Boomerang2DFramework.CustomScripts.Player {
    /// <summary>
    /// Extended player controller with kick/projectile attacks and score pickup handling.
    /// Drop this on the Boomerang2D GameObject or any active GameObject in the scene.
    /// </summary>
    public class PlayerController : MonoBehaviour {
        [Header("Movement")]
        public float runSpeed = 5f;
        public float jumpSpeed = 8f;
        public float gravity = 20f;
        
        [Header("Input")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode kickKey = KeyCode.K;
        public KeyCode throwKey = KeyCode.F;
        
        [Header("Projectile")]
        public GameObject projectilePrefab;
        public float projectileSpeed = 10f;
        public bool projectileReturns = true;
        public Sprite projectileSprite;
        
        private Actor player => Boomerang2D.Player;

        void Start() {
            if (player != null) {
                player.SetCanBeGrounded(true);
                player.SetCollidesWithGeometry(true);
                player.SetOverlapsOtherActors(true);
                player.SetOverlapsWeapons(true);
            }
        }

        void Update() {
            if (player == null || player.ActorProperties == null) return;

            // Only handle our custom actions, let the built-in system handle movement
            // HandleMovement();  // Commented out to avoid conflict
            // HandleJump();      // Commented out to avoid conflict
            HandleKick();
            HandleThrow();
            HandlePickups();
        }

        private void HandleMovement() {
            float h = Input.GetAxisRaw("Horizontal");
            player.SetVelocityX(h * runSpeed);

            if (h < 0) player.FacingDirection = Directions.Left;
            if (h > 0) player.FacingDirection = Directions.Right;

            // Apply gravity if not grounded
            if (!player.IsGrounded) {
                player.OffsetVelocityY(-gravity * Time.deltaTime);
            }
        }

        private void HandleJump() {
            if (Input.GetKeyDown(jumpKey) && player.IsGrounded) {
                player.SetVelocityY(jumpSpeed);
            }
        }

        private void HandleKick() {
            if (Input.GetKeyDown(kickKey)) {
                // Try using weapon system first
                var weapon = player.GetWeaponBehavior("Kick");
                if (weapon != null) {
                    weapon.Attack();
                } else {
                    // Fallback: push overlapping actors and apply bleed
                    foreach (var kv in player.OverlappingActorFlags) {
                        foreach (var oc in kv.Value) {
                            if (oc.Actor != null && oc.Actor != player) {
                                float push = (player.FacingDirection == Directions.Right) ? 6f : -6f;
                                oc.Actor.OffsetVelocity(push, 2f);
                                
                                // Apply bleed effect
                                var statusMgr = oc.Actor.GameObject.GetComponent<StatusEffectManager>();
                                if (statusMgr == null) {
                                    statusMgr = oc.Actor.GameObject.AddComponent<StatusEffectManager>();
                                }
                                statusMgr.AddEffect(new BleedEffect(oc.Actor, 5f, 5));
                                
                                Debug.Log($"Kicked {oc.Actor.ActorProperties.Name} - applied bleed!");
                            }
                        }
                    }
                }
            }
        }

        private void HandleThrow() {
            if (Input.GetKeyDown(throwKey)) {
                SpawnProjectile();
            }
        }

        private void SpawnProjectile() {
            Vector2 dir = player.FacingDirection == Directions.Right 
                ? Vector2.right 
                : Vector2.left;
            
            Vector3 spawnPos = player.RealPosition + (Vector3)(dir * 1f);
            
            GameObject proj = new GameObject("Projectile");
            proj.transform.position = spawnPos;
            
            var pb = proj.AddComponent<ProjectileBehavior>();
            pb.Initialize(player, dir * projectileSpeed, projectileSprite);
            pb.ReturnsToOwner = projectileReturns;
            pb.ReturnStartTime = 1.5f;
            pb.Lifetime = 5f;
            
            Debug.Log("Threw projectile!");
        }

        private void HandlePickups() {
            // Check for overlapping actors with "Pickup" or "Coin" flags
            if (player.OverlappingActorFlags.ContainsKey("pickup") || 
                player.OverlappingActorFlags.ContainsKey("coin")) {
                
                var pickups = player.OverlappingActorFlags.ContainsKey("pickup") 
                    ? player.OverlappingActorFlags["pickup"] 
                    : player.OverlappingActorFlags["coin"];
                
                foreach (var oc in pickups.ToArray()) {
                    if (oc.Actor != null) {
                        // Add score
                        ScoreManager.Add(10);
                        
                        // Remove pickup actor
                        Boomerang2DFramework.Framework.MapGeneration.MapManager.RemoveActor(oc.Actor);
                        
                        Debug.Log("Collected pickup! +10 score");
                    }
                }
            }
        }
    }
}
