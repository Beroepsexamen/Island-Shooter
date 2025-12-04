using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewShooterData", menuName = "Shooter/Shooter Data")]
public class ShooterData : ScriptableObject
{
    public float range = 100f;
    public float Damage = 10f;
    public float ShootDelay = 0.5f;
    public int maxAmmo = 30;
    public Sprite WeaponIcon;

    public GameObject fireEffect;
    public GameObject hitEffect;

    public float effectLifetime = 1f;
    

    public GameObject Gun;

    public GameObject firePoint;   
}
