using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    public Camera fpsCam;
    public Transform gunHolder;
    public WeaponsUIscript weaponUI;

    public List<ShooterData> unlockedGuns = new List<ShooterData>();

    private ShooterData currentGunData;
    private GameObject currentGun;

    private float nextFireTime = 0f;

    private int currentAmmo;

    // Ammo per gun opslaan
    private Dictionary<ShooterData, int> ammoPerGun = new Dictionary<ShooterData, int>();

    private void Start()
    {
        if (unlockedGuns.Count > 0)
            EquipGun(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Shooting();

        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipGun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipGun(3);
        if (Input.GetKeyDown(KeyCode.Alpha9)) EquipGun(4);
    }

    public void AddGun(ShooterData newGun)
    {
        if (!unlockedGuns.Contains(newGun))
        {
            unlockedGuns.Add(newGun);

            // start ammo voor nieuw wapen
            ammoPerGun[newGun] = newGun.maxAmmo;

            EquipGun(unlockedGuns.Count - 1);
        }
    }

    void EquipGun(int index)
    {
        if (index < 0 || index >= unlockedGuns.Count)
            return;

        // sla ammo van huidig wapen op
        if (currentGunData != null)
        {
            ammoPerGun[currentGunData] = currentAmmo;
        }

        currentGunData = unlockedGuns[index];

        if (currentGun != null)
            Destroy(currentGun);

        currentGun = Instantiate(
            currentGunData.Gun,
            gunHolder.position,
            gunHolder.rotation,
            gunHolder
        );

        // laad ammo van dit wapen
        if (!ammoPerGun.ContainsKey(currentGunData))
            ammoPerGun[currentGunData] = currentGunData.maxAmmo;

        currentAmmo = ammoPerGun[currentGunData];

        if (weaponUI != null)
            weaponUI.UpdateIcon(currentGunData.weaponIcon);
    }

    public void Shooting()
    {
        if (currentGunData == null || currentGunData.firePoint == null)
            return;

        if (currentAmmo <= 0)
            return;

        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + currentGunData.ShootDelay;
        currentAmmo--;

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
