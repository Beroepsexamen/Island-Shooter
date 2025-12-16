using UnityEngine;
using System.Collections.Generic;

public class Ammo : MonoBehaviour
{
    private class AmmoData
    {
        public int reserve;
        public int clip;
    }

    private Dictionary<ShooterData, AmmoData> ammoPerGun =
        new Dictionary<ShooterData, AmmoData>();

    private AmmoData GetData(ShooterData gun)
    {
        if (!ammoPerGun.ContainsKey(gun))
        {
            AmmoData data = new AmmoData();
            data.reserve = gun.maxAmmo;
            data.clip = gun.clipSize;
            ammoPerGun.Add(gun, data);
        }

        return ammoPerGun[gun];
    }

    public bool HasAmmoInClip(ShooterData gun)
    {
        return GetData(gun).clip > 0;
    }

    public void UseBullet(ShooterData gun)
    {
        AmmoData data = GetData(gun);
        data.clip = Mathf.Max(0, data.clip - 1);
    }

    public void Reload(ShooterData gun)
    {
        AmmoData data = GetData(gun);

        if (data.clip == gun.clipSize)
            return;

        if (data.reserve <= 0)
            return;

        int needed = gun.clipSize - data.clip;
        int taken = Mathf.Min(needed, data.reserve);

        data.reserve -= taken;
        data.clip += taken;
    }

    public int GetClip(ShooterData gun)
    {
        return GetData(gun).clip;
    }

    public int GetReserve(ShooterData gun)
    {
        return GetData(gun).reserve;
    }

    public void AddReserveAmmo(ShooterData gun, int amount)
    {
        AmmoData data = GetData(gun);
        data.reserve = Mathf.Min(gun.maxAmmo, data.reserve + amount);
    }
}
