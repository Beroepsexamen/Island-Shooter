using UnityEngine;

[CreateAssetMenu(fileName = "NewShooterData", menuName = "Shooter/Shooter Data")]
public class ShooterData : ScriptableObject
{
    public float range = 100f;

    public GameObject fireEffect;
    public GameObject hitEffect;

    public float effectLifetime = 1f;
}
