using Boomerang2DFramework.Framework.BoomerangDatabaseManagement;
using Boomerang2DFramework.Framework.GlobalTimeManagement;
using Boomerang2DFramework.Framework.MapGeneration.PropertyClasses;
using UnityEngine;

namespace Boomerang2DFramework.Framework.MapGeneration {
	/// <summary>
	/// The MonoBehavior for tiles
	/// </summary>
	public class TileBehavior : MonoBehaviour {
		public TileProperties Properties;
		private Sprite[] _tileSetFrames;

		private SpriteRenderer _spriteRenderer;

		private float _totalAnimationTime;
		private string _shader;

		private void Start() {
			GameObject sprite = transform.Find("Sprite").gameObject;
			_spriteRenderer = sprite.GetComponent<SpriteRenderer>();

			PrepareAnimation();
		}

		private void Update() {
			if (Properties != null && Properties.AnimationFrames != null && Properties.AnimationFrames.Count > 1 && _tileSetFrames != null) {
				UpdateAnimationFrame();
			} else if (Properties == null && _spriteRenderer != null && _spriteRenderer.sprite != null) {
				_spriteRenderer.sprite = null;
			}
		}

		private void PrepareAnimation() {
			_totalAnimationTime = 0;

			if (Properties != null && Properties.AnimationFramesSpeeds != null && Properties.AnimationFrames != null && _spriteRenderer != null && _tileSetFrames != null) {
				foreach (float frameSpeed in Properties.AnimationFramesSpeeds) {
					_totalAnimationTime += frameSpeed;
				}

				if (Properties.AnimationFrames.Count > 0 && Properties.AnimationFrames[0] < _tileSetFrames.Length) {
					_spriteRenderer.sprite = _tileSetFrames[Properties.AnimationFrames[0]];
				}
			}
		}

		private void UpdateAnimationFrame() {
			if (Properties == null || Properties.AnimationFramesSpeeds == null || Properties.AnimationFrames == null || _tileSetFrames == null) {
				return;
			}

			float totalCycles = GlobalTimeManager.TotalTime / _totalAnimationTime;
			float progressionIntoCycle = totalCycles - Mathf.Floor(totalCycles);
			float currentCycleFrame = progressionIntoCycle * _totalAnimationTime;
			float frameMax = 0;
			int destinationFrame = 0;


			for (int i = 0; i < Properties.AnimationFramesSpeeds.Count; i++) {
				frameMax += Properties.AnimationFramesSpeeds[i];

				if (currentCycleFrame > frameMax) {
					continue;
				}

				destinationFrame = i;
				break;
			}

			if (destinationFrame < Properties.AnimationFrames.Count && Properties.AnimationFrames[destinationFrame] < _tileSetFrames.Length) {
				_spriteRenderer.sprite = _tileSetFrames[Properties.AnimationFrames[destinationFrame]];
			}
		}

		public void SetProperties(TileProperties properties, Sprite[] tilesetFrames) {
			Properties = properties;
			_tileSetFrames = tilesetFrames;

			PrepareAnimation();
		}

		public void SetSpriteShader(string shader) {
			_spriteRenderer.material.shader = BoomerangDatabase.ShaderDatabaseEntries[shader];
		}
	}
}