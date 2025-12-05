using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public ShooterData gunData;

    private void OnTriggerEnter(Collider other)
    {
        Shooter shooter = other.GetComponent<Shooter>();

        if (shooter != null)
        {
            shooter.AddGun(gunData);
            Destroy(gameObject);
        }
    }
}
