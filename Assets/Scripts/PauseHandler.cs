using NUnit.Framework;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [Header("Death Screen")]
    public PlayerHealth PlayerHealth;
    public Canvas DeathCanvas;
    public bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        DeathUI();
    }

    private void DeathUI()
    {
        if (PlayerHealth.Health <= 0 && !isDead)
        {
            isDead = true;
        }

        if (isDead)
        {
            DeathCanvas.enabled = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!isDead)
        {
            DeathCanvas.enabled = false;
            Time.timeScale = 1f;
        }
    }
}
