using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlescreenscript : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        
#if UNITY_EDITOR
        // Stop play mode when testing inside the Editor
        EditorApplication.isPlaying = false;
#else
        // Quit the application in a built player
        Application.Quit();
#endif
    }
}
