using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewShooterData", menuName = "Shooter/Shooter Data")]
public class ShooterData : ScriptableObject
{
    [Header("Stats")]
    public float range = 100f;
    public float damage = 10f;
    public float ShootDelay = 0.5f;
    public float reloadTime = 2f;

    [Header("Ammo")]
    public int maxAmmo = 120;     
    public int startReserveAmmo = 20;
    public int clipSize = 30;     // magazijn grootte

    [Header("UI")]
    public Sprite weaponIcon;

    [Header("Effects")]
    public GameObject fireEffect;
    public GameObject hitEffect;
    public float effectLifetime = 1f;

    [Header("Gun Prefabs")]
    public GameObject Gun;        // The gun prefab itself

    [Header("Gun Transform")]
    public Vector3 gunScale = Vector3.one;  // Scale of the gun when held

    [Header("Audio Clips")]
    public AudioClip shootSound;

}
