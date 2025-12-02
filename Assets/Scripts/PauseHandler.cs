using NUnit.Framework;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [Header("Death Screen")]
    public PlayerHealth PlayerHealth;
    public Canvas DeathCanvas;
    public bool isDead = false;

    void Update()
    {
        DeathUI();
    }

    private void DeathUI()
    {
        if (PlayerHealth.Health <= 0 && !isDead) // Check if player is dead
        {
            isDead = true;
        }

        if (isDead) // If player is dead show death screen
        {
            DeathCanvas.enabled = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!isDead) // If player is alive hide death screen
        {
            DeathCanvas.enabled = false;
            Time.timeScale = 1f;
        }
    }
}
