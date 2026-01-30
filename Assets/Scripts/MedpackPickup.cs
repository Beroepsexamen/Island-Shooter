using UnityEngine;

public class MedpackPickup : MonoBehaviour
{
    public int healAmount = 1;           // How much health this medpack gives
    public GameObject pickupTextUI;      // UI Text object that shows "Press E"

    private bool canPickup = false;
    private PlayerHealth health;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false); // Start invisible
    }

    // If the player picks up the medpack by pressing E, heal the player
    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (health != null)
            {
                health.health += healAmount;
                if (health.health > health.maxHealth)
                    health.health = health.maxHealth;

                health.hearts.UpdateHP(health.health);
                pickupTextUI.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            // Get PlayerHealth from GameManager
            health = PlayerHealth.instance;
            if (health != null)
            {
                canPickup = true;
                if (pickupTextUI != null)
                    pickupTextUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide pickup text when player leaves range
            canPickup = false;
            health = null;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(false);
        }
    }
}
