using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    [Header("Death Screen")]
    public Canvas deathCanvas;
    public Animator playerAnimator;
    public PlayerCam playerCam;
    public Canvas playerHUD;
    public Canvas pauseScreen;

    public bool isDead = false;
    public bool isPaused = false;

    private void Update()
    {
        DeathUI();
        PauseUI();
    }

    private void DeathUI()
    {
        if (PlayerHealth.instance.health <= 0 && !isDead) // Check if player is dead
        {
            StartCoroutine(Die());
        }

        if (isDead) // Show death screen when player is dead
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            deathCanvas.enabled = true;
            playerCam.enabled = false;
            if (playerHUD != null) playerHUD.enabled = false;
            PlayerManager.instance.player.SetActive(false);
        }
    }

    private void PauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead) isPaused = !isPaused;

        if (isPaused && !isDead) // Show pause screen when game is paused
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PlayerManager.instance.player.GetComponentInChildren<Shooter>().enabled = false;
            pauseScreen.enabled = true;
            playerCam.enabled = false;
            if (playerHUD != null) playerHUD.enabled = false;
            Time.timeScale = 0f; // Freeze game
        }
        
        else if (!isPaused && !isDead) // Hide pause screen when game is unpaused
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PlayerManager.instance.player.GetComponentInChildren<Shooter>().enabled = true;
            pauseScreen.enabled = false;
            playerCam.enabled = true;
            if (playerHUD != null) playerHUD.enabled = true;
            Time.timeScale = 1f; // Unfreeze game
        }
    }

    private IEnumerator Die() // When player dies, play death animation and trigger death screen
    {
        playerAnimator.SetBool("IsDead", true);
        PlayerManager.instance.player.GetComponentInChildren<Shooter>().enabled = false;
        yield return new WaitForSeconds(2f);
        isDead = true;
    }

    public void RestartGame() // Restart the current game scene
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame() // Resume game from pause
    {
        isPaused = false;
    }

    public void QuitGame() // Quit game application
    {
        Application.Quit();
    }
}
