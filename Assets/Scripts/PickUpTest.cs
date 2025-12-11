using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public ShooterData gunData;

    private Shooter playerShooter;
    private bool playerInRange = false;

    void Update()
    {
        // Alleen oppakken als speler dichtbij is en E drukt
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerShooter.AddGun(gunData);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Shooter shooter = other.GetComponent<Shooter>();

        if (shooter != null)
        {
            playerShooter = shooter;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Shooter shooter = other.GetComponent<Shooter>();

        if (shooter != null)
        {
            playerShooter = null;
            playerInRange = false;
        }
    }
}
