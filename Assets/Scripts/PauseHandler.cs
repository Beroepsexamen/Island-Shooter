using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [Header("Death Screen")]
    public Canvas deathCanvas;
    public Animator playerAnimator;
    public PlayerCam playerCam;
    public Canvas playerHUD;
    private GameObject player;
    public bool isDead = false;

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        DeathUI();
    }

    private void DeathUI()
    {
        if (PlayerHealth.instance.health <= 0 && !isDead) // Check if player is dead
        {
            StartCoroutine(Die());
        }

        if (isDead) // If player is dead show death screen
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            deathCanvas.enabled = true;
            playerCam.enabled = false;
            if (playerHUD != null) playerHUD.enabled = false;
            player.SetActive(false);
        }
    }

    private IEnumerator Die()
    {
        playerAnimator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2f);
        isDead = true;
    }
}
