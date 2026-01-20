using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public ShooterData gunType;
    public int ammoAmount = 30;
    public GameObject pickupTextUI;

    private bool canPickup = false;
    private Ammo ammo;
    private Shooter shooter;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false);
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (ammo != null && gunType != null)
            {
                // ammo toevoegen
                ammo.AddAmmo(gunType, ammoAmount);

                // UI updaten
                if (shooter != null)
                    shooter.UpdateAmmoUI();
                pickupTextUI.SetActive(false);
                Destroy(gameObject);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammo = other.GetComponent<Ammo>();
            shooter = other.GetComponent<Shooter>();

            if (ammo != null)
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
            ammo = null;
            shooter = null;

            if (pickupTextUI != false)
                pickupTextUI.SetActive(false);
        }
    }

    
}
