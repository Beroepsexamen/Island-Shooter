using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewShooterData", menuName = "Shooter/Shooter Data")]
public class ShooterData : ScriptableObject
{
    [Header("Stats")]
    public float range = 100f;
    public float damage = 10f;
    public float ShootDelay = 0.5f;
    public int maxAmmo = 30;
    public float reloadTime = 2f;

    [Header("UI")]
    public Sprite weaponIcon;

    [Header("Effects")]
    public GameObject fireEffect;
    public GameObject hitEffect;
    public float effectLifetime = 1f;

    [Header("Gun Prefabs")]
    public GameObject Gun;        // The gun prefab itself
    public GameObject firePoint;  // FirePoint INSIDE the gun prefab
}
