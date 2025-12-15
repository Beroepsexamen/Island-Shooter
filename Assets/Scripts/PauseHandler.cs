using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [Header("Death Screen")]
    public PlayerHealth PlayerHealth;
    public Canvas DeathCanvas;
    public Animator PlayerAnimator;
    public PlayerCam PlayerCam;
    public bool IsDead = false;

    void Update()
    {
        DeathUI();
    }

    private void DeathUI()
    {
        if (PlayerHealth.Health <= 0 && !IsDead) // Check if player is dead
        {
            StartCoroutine(Die());
        }

        if (IsDead) // If player is dead show death screen
        {
            DeathCanvas.enabled = true;
            PlayerCam.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private IEnumerator Die()
    {
        PlayerAnimator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2f);
        IsDead = true;
    }
}
