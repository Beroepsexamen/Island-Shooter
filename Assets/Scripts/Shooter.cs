using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera fpsCam;

    public Transform gunHolder;

    public ShooterData[] allGuns;

    public Vector3 spawnOffset = new Vector3(0f, 0f, 0f); // naast de speler

    private ShooterData currentGunData;
    private GameObject currentGun;

    
    private float nextFireTime = 0f;
    

    private void Start()
    {
        
        //GameObject holderInstance = Instantiate(
        //    gunHolderPrefab,
        //    transform.position + spawnOffset,
        //    transform.rotation,
        //    transform       
        //);

        //gunHolder = holderInstance.transform;

        EquipGun(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shooting();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipGun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipGun(3);
        if (Input.GetKeyDown(KeyCode.Alpha9)) EquipGun(4);
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

        //if (weaponUI != null)
        //{
        //    weaponUI.UpdateIcon(currentGunData.weaponIcon);
        //}
    }

    public void Shooting()
    {
        if (currentGunData == null || currentGunData.firePoint == null) return;

        // cooldown check
        if (Time.time < nextFireTime) return;

        // set next allowed fire time using ShootDelay from the current gun data
        nextFireTime = Time.time + currentGunData.ShootDelay;

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
