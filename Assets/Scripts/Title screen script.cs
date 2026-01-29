using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit requested");
#if UNITY_EDITOR
        // Stop play mode when testing inside the Editor
        EditorApplication.isPlaying = false;
#else
        // Quit the application in a built player
        Application.Quit();
#endif
    }
}
