using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // Recarga la escena actual
    public void ReloadLevel()
    {
        Debug.Log("ChangeScene: ReloadLevel called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Carga una escena según su índice en Build Settings
    public void PlayIndex(int index)
    {
        Debug.Log($"ChangeScene: PlayIndex called with index={index}");
        SceneManager.LoadScene(index);
    }

    // Carga una escena por nombre
    public void LoadSceneByName(string sceneName)
    {
        Debug.Log($"ChangeScene: LoadSceneByName called with name='{sceneName}'");
        SceneManager.LoadScene(sceneName);
    }

    // Cierra el juego (no funciona en el editor)
    public void ExitGame()
    {
        Debug.Log("ChangeScene: ExitGame called - quitting application (in build).\nIf running in Editor, use EditorApplication.isPlaying = false to stop play mode."); // Solo visible en el editor
        Application.Quit();
    }
}