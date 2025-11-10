using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Boomerang2DFramework.CustomScripts.UI {
    /// <summary>
    /// 3D Menu system that can load different scenes.
    /// Attach this to a GameObject in your menu scene.
    /// </summary>
    public class Menu3D : MonoBehaviour {
        [Header("Scene Names")]
        public string gameSceneName = "Boomerang2D";  // Changed to Boomerang scene
        public string settingsSceneName = "SettingsScene";
        public string creditsSceneName = "CreditsScene";
        public string mainMenuSceneName = "MainMenu";
        
        [Header("Menu Buttons (Optional)")]
        public Button newGameButton;  // Added New Game button
        public Button playButton;
        public Button settingsButton;
        public Button creditsButton;
        public Button quitButton;
        public Button backButton;
        
        [Header("3D Menu Objects")]
        public GameObject[] menuObjects;
        public Transform cameraTransform;
        public float rotationSpeed = 10f;
        public bool autoRotateMenu = true;
        
        [Header("Audio (Optional)")]
        public AudioSource audioSource;
        public AudioClip buttonClickSound;
        public AudioClip backgroundMusic;

        private void Start() {
            SetupButtons();
            PlayBackgroundMusic();
        }

        private void Update() {
            HandleInput();
            if (autoRotateMenu && menuObjects.Length > 0) {
                RotateMenuObjects();
            }
        }

        private void SetupButtons() {
            if (newGameButton != null) {
                newGameButton.onClick.AddListener(() => LoadScene(gameSceneName));
            }
            
            if (playButton != null) {
                playButton.onClick.AddListener(() => LoadScene(gameSceneName));
            }
            
            if (settingsButton != null) {
                settingsButton.onClick.AddListener(() => LoadScene(settingsSceneName));
            }
            
            if (creditsButton != null) {
                creditsButton.onClick.AddListener(() => LoadScene(creditsSceneName));
            }
            
            if (quitButton != null) {
                quitButton.onClick.AddListener(QuitGame);
            }
            
            if (backButton != null) {
                backButton.onClick.AddListener(() => LoadScene(mainMenuSceneName));
            }
        }

        private void HandleInput() {
            // Keyboard shortcuts
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                LoadScene(gameSceneName);
            }
            
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (SceneManager.GetActiveScene().name == mainMenuSceneName) {
                    QuitGame();
                } else {
                    LoadScene(mainMenuSceneName);
                }
            }
            
            // Number key shortcuts
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                LoadScene(gameSceneName);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                LoadScene(settingsSceneName);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                LoadScene(creditsSceneName);
            }
        }

        private void RotateMenuObjects() {
            foreach (GameObject obj in menuObjects) {
                if (obj != null) {
                    obj.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                }
            }
        }

        public void LoadScene(string sceneName) {
            if (string.IsNullOrEmpty(sceneName)) {
                Debug.LogWarning("Scene name is empty!");
                return;
            }
            
            PlayButtonSound();
            
            // Check if scene exists in build settings
            if (Application.CanStreamedLevelBeLoaded(sceneName)) {
                Debug.Log($"Loading scene: {sceneName}");
                SceneManager.LoadScene(sceneName);
            } else {
                Debug.LogError($"Scene '{sceneName}' not found in build settings!");
                // Show error message to user
                ShowErrorMessage($"Scene '{sceneName}' not found!");
            }
        }

        public void LoadSceneAsync(string sceneName) {
            if (string.IsNullOrEmpty(sceneName)) {
                Debug.LogWarning("Scene name is empty!");
                return;
            }
            
            PlayButtonSound();
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }

        private System.Collections.IEnumerator LoadSceneAsyncCoroutine(string sceneName) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            
            while (!asyncLoad.isDone) {
                // You can show a loading bar here
                float progress = asyncLoad.progress;
                Debug.Log($"Loading progress: {progress * 100}%");
                yield return null;
            }
        }

        public void QuitGame() {
            PlayButtonSound();
            Debug.Log("Quitting game...");
            
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void ReloadCurrentScene() {
            PlayButtonSound();
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        private void PlayButtonSound() {
            if (audioSource != null && buttonClickSound != null) {
                audioSource.PlayOneShot(buttonClickSound);
            }
        }

        private void PlayBackgroundMusic() {
            if (audioSource != null && backgroundMusic != null) {
                audioSource.clip = backgroundMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        private void ShowErrorMessage(string message) {
            // Simple error display - you can improve this with a proper UI panel
            Debug.LogError(message);
            
            // Create a temporary text object to show the error
            GameObject errorObj = new GameObject("ErrorMessage");
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas != null) {
                errorObj.transform.SetParent(canvas.transform, false);
                Text errorText = errorObj.AddComponent<Text>();
                errorText.text = message;
                errorText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                errorText.fontSize = 24;
                errorText.color = Color.red;
                errorText.alignment = TextAnchor.MiddleCenter;
                
                RectTransform rect = errorText.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 0.5f);
                rect.anchorMax = new Vector2(1, 0.5f);
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                
                // Destroy after 3 seconds
                Destroy(errorObj, 3f);
            }
        }

        // Public methods for UI buttons
        public void OnNewGameButtonClick() {
            LoadScene(gameSceneName);
        }

        public void OnPlayButtonClick() {
            LoadScene(gameSceneName);
        }

        public void OnSettingsButtonClick() {
            LoadScene(settingsSceneName);
        }

        public void OnCreditsButtonClick() {
            LoadScene(creditsSceneName);
        }

        public void OnBackButtonClick() {
            LoadScene(mainMenuSceneName);
        }

        public void OnQuitButtonClick() {
            QuitGame();
        }
    }
}