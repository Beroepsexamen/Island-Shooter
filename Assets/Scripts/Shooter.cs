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
    private Transform currentFirePoint;

    private float nextFireTime = 0f;

    private Ammo ammo;
    public AmmoUI ammoUI;

    private void Start()
    {
        ammo = GetComponent<Ammo>();

        if (unlockedGuns.Count > 0)
            EquipGun(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Shooting();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();

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
            EquipGun(unlockedGuns.Count - 1);
        }
    }

    void EquipGun(int index)
    {
        if (index < 0 || index >= unlockedGuns.Count)
            return;

        currentGunData = unlockedGuns[index];

        if (currentGun != null)
            Destroy(currentGun);

        currentGun = Instantiate(
            currentGunData.Gun,
            gunHolder.position,
            gunHolder.rotation,
            gunHolder
        );

        currentGun.transform.localScale = currentGunData.gunScale;

        UpdateAmmoUI();

        currentFirePoint = currentGun.transform.GetChild(0);

        ammo.GetClip(currentGunData);

        if (weaponUI != null)
            weaponUI.UpdateIcon(currentGunData.weaponIcon);
    }

    void UpdateAmmoUI()
    {
        if (ammo == null || ammoUI == null || currentGunData == null) return;

        ammoUI.UpdateAmmo(
            ammo.GetClipAmount(currentGunData),
            ammo.GetReserveAmount(currentGunData)
        );
    }

    void Reload()
    {
        if (currentGunData == null)
            return;

        ammo.Reload(currentGunData);
        UpdateAmmoUI();
    }

    public void Shooting()
    {
        if (currentGunData == null || currentFirePoint == null)
            return;

        if (!ammo.HasAmmoInClip(currentGunData))
            return;

        if (Time.time < nextFireTime)
            return;

        RaycastHit hit;

        // Doe eerst de raycast
        if (!Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out hit,
            currentGunData.range))
        {
            return; //  niks geraakt = geen kogel gebruiken
        }

        // Pas NU schieten
        nextFireTime = Time.time + currentGunData.ShootDelay;

        ammo.UseBullet(currentGunData);
        UpdateAmmoUI();

        // Damage
        EnemyController enemy = hit.collider.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)currentGunData.damage);
        }

        // Muzzle flash
        GameObject fire = Instantiate(
            currentGunData.fireEffect,
            currentFirePoint.position,
            currentFirePoint.rotation,
            currentFirePoint
        );

        // Hit effect
        GameObject hitFX = Instantiate(
            currentGunData.hitEffect,
            hit.point,
            Quaternion.LookRotation(hit.normal)
        );

        Destroy(fire, currentGunData.effectLifetime);
        Destroy(hitFX, currentGunData.effectLifetime);
    }

}
