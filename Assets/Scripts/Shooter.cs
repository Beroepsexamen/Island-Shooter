using UnityEngine;
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
    private AudioSource gunAudio;
    

    private float nextFireTime;

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

        if (unlockedGuns.Count >= 4) Achievements.instance.UnlockAchievement("VindAlleGuns");
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

        gunAudio = currentGun.GetComponent<AudioSource>();
        currentFirePoint = currentGun.transform.GetChild(0);

        ammo.GetClip(currentGunData);
        UpdateAmmoUI();

        if (weaponUI != null)
            weaponUI.UpdateIcon(currentGunData.weaponIcon);
    }

   public void UpdateAmmoUI()
    {
        if (ammo == null || ammoUI == null || currentGunData == null) return;

        ammoUI.UpdateAmmo(
            ammo.GetClipAmount(currentGunData),
            ammo.GetReserveAmount(currentGunData)
        );
    }

    void Reload()
    {
        if (currentGunData == null) return;

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

        if (!Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out hit,
            currentGunData.range))
        {
            return;
        }

        nextFireTime = Time.time + currentGunData.ShootDelay;

        ammo.UseBullet(currentGunData);
        UpdateAmmoUI();

        if (gunAudio != null && currentGunData.shootSound != null)
            gunAudio.PlayOneShot(currentGunData.shootSound);

        EnemyController enemy = hit.collider.GetComponent<EnemyController>();
        if (enemy != null)
            enemy.TakeDamage((int)currentGunData.damage);

        GameObject fire = Instantiate(
            currentGunData.fireEffect,
            currentFirePoint.position,
            currentFirePoint.rotation,
            currentFirePoint
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
