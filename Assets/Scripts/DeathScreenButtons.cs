using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
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
