using UnityEngine;

public class SecretPickup : MonoBehaviour
{


    public string achievementName;   // bv: "Secret1"
    public GameObject pickupTextUI;   // "Press E" UI

    private bool canPickup = false;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false);
    }

    // If the player collects the secret pickup by pressing E, unlock the achievement
    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (Achievements.instance != null)
            {
                Achievements.instance.UnlockAchievement(achievementName);
                pickupTextUI.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    // Show pickup text when player is in range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;

            if (pickupTextUI != null)
                pickupTextUI.SetActive(true);
            
              
        }
    }

    // Hide pickup text when player leaves range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;

            if (pickupTextUI != null)
                pickupTextUI.SetActive(false);
        }
    }
}
