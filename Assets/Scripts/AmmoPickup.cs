using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public ShooterData gunType;          // Voor welk wapen deze ammo is
    public int ammoAmount = 30;          // Hoeveel kogels deze pickup geeft
    public GameObject pickupTextUI;      // UI Text object dat "Press E" toont

    private bool canPickup = false;
    private Ammo ammo;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false); // start onzichtbaar
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (ammo != null && gunType != null)
            {
                ammo.AddAmmo(gunType, ammoAmount);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Alleen de speler mag de ammo oppakken
        if (other.CompareTag("Player"))
        {
            // Pak Ammo component van de speler
            ammo = other.GetComponent<Ammo>();
            if (ammo != null)
            {
                canPickup = true;
                if (pickupTextUI != null)
                    pickupTextUI.SetActive(true); // toon UI
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            ammo = null;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(false); // verberg UI
        }
    }
}
