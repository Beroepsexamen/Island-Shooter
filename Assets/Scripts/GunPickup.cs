using UnityEngine;
using UnityEngine.UI; 

public class GunPickup : MonoBehaviour
{
    public ShooterData gunData;
    public GameObject pickupTextUI; // Zet hier het UI prefab of object (Text)

    private bool canPickup = false;
    private Shooter shooter;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false); // start onzichtbaar
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (shooter != null && gunData != null)
            {
                shooter.AddGun(gunData);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        shooter = other.GetComponent<Shooter>();
        if (shooter != null)
        {
            canPickup = true;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Shooter>() != null)
        {
            canPickup = false;
            shooter = null;
            if (pickupTextUI != null)
                pickupTextUI.SetActive(false);
        }
    }
}
