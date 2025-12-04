using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera fpsCam;

    public GameObject gunHolderPrefab;
    private Transform gunHolder;

    public ShooterData[] allGuns;

    public Vector3 spawnOffset = new Vector3(1f, 0f, 0f); // naast de speler

    private ShooterData currentGunData;
    private GameObject currentGun;

    private void Start()
    {
        
        GameObject holderInstance = Instantiate(
            gunHolderPrefab,
            transform.position + spawnOffset,
            transform.rotation,
            transform       
        );

        gunHolder = holderInstance.transform;

        EquipGun(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipGun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipGun(3);
    }

    void EquipGun(int index)
    {
        if (index < 0 || index >= allGuns.Length) return;

        currentGunData = allGuns[index];

        if (currentGun != null)
            Destroy(currentGun);

        currentGun = Instantiate(
            currentGunData.Gun,
            gunHolder.position,
            gunHolder.rotation,
            gunHolder
        );
    }

    public void Shooting()
    {
        if (currentGunData == null || currentGunData.firePoint == null) return;

        RaycastHit hit;

        if (Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out hit,
            currentGunData.range))
        {
            GameObject fire = Instantiate(
                currentGunData.fireEffect,
                currentGunData.firePoint.transform.position,
                currentGunData.firePoint.transform.rotation
            );

            GameObject hitFX = Instantiate(
                currentGunData.hitEffect,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            Destroy(fire, currentGunData.effectLifetime);
            Destroy(hitFX, currentGunData.effectLifetime);
        }
    }
}
