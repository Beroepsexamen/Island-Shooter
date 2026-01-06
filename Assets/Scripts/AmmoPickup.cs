using UnityEngine;
using UnityEngine.UI;

public class AmmoPickup : MonoBehaviour
{
    public ShooterData gunType;
    public int ammoAmount = 30;
    public GameObject pickupTextUI;

    private bool canPickup = false;
    private Ammo ammo;

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
                ammo.AddAmmo(gunType, ammoAmount);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ammo = other.GetComponent<Ammo>();
        if (ammo != null)
        {
            canPickup = true;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ammo>() != null)
        {
            canPickup = false;
            ammo = null;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(false);
        }
    }
}
