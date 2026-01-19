using UnityEngine;

public class MedpackPickup : MonoBehaviour
{
    public int healAmount = 1;           // Hoeveel health deze medpack geeft
    public GameObject pickupTextUI;      // UI Text object dat "Press E" toont

    private bool canPickup = false;
    private PlayerHealth health;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false); // start onzichtbaar
    }

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
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check of de collider van de speler is
        if (other.CompareTag("Player"))
        {
            // Pak PlayerHealth van GameManager
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
            canPickup = false;
            health = null;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(false);
        }
    }
}
