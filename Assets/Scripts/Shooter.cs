using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera fpsCam;
    public Transform firePoint;

    public ShooterData shooterData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }
    }        

   

    public void Shooting()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out hit,
            shooterData.range))
        {
            Debug.DrawRay(
                fpsCam.transform.position,
                fpsCam.transform.forward * hit.distance,
                Color.yellow
            );

            GameObject fire = Instantiate(
                shooterData.fireEffect,
                firePoint.position,
                Quaternion.identity
            );

            GameObject hitFX = Instantiate(
                shooterData.hitEffect,
                hit.point,
                Quaternion.identity
            );

            Destroy(fire, shooterData.effectLifetime);
            Destroy(hitFX, shooterData.effectLifetime);
        }
    }
}
