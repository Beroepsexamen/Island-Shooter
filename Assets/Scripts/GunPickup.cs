using UnityEngine;
using UnityEngine.UI;

public class GunPickup : MonoBehaviour
{
    public ShooterData gunData;
    public GameObject pressEUI;

    private Shooter playerShooter;
    private bool playerInRange;

    void Start()
    {
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerShooter.AddGun(gunData);
            if (pressEUI != null)
                pressEUI.SetActive(false);
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
            if (pressEUI != null)
                pressEUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Shooter shooter = other.GetComponent<Shooter>();
        if (shooter != null)
        {
            playerShooter = null;
            playerInRange = false;
            if (pressEUI != null)
                pressEUI.SetActive(false);
        }
    }
}
