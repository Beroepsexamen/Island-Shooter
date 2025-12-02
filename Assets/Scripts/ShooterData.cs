using UnityEngine;

public class ShooterData : ScriptableObject
{
    public float range = 100f;

    public GameObject fireEffect;
    public GameObject hitEffect;

    public float effectLifetime = 1f;

    public GameObject Gun;

    public Transform firePoint;   
}
