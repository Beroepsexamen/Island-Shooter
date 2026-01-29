using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlescreenscript : MonoBehaviour
{
    public Canvas TitleScreen;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() // Quit game application
    {
        Application.Quit();
    }
}
