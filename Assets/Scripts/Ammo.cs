using UnityEngine;
using System.Collections.Generic;

public class Ammo : MonoBehaviour
{
    
    private Dictionary<ShooterData, int> ammoClips = new Dictionary<ShooterData, int>();
    
    private Dictionary<ShooterData, int> ammoReserve = new Dictionary<ShooterData, int>();

    
    public void GetClip(ShooterData gunData)
    {
        if (!ammoClips.ContainsKey(gunData))
            ammoClips[gunData] = gunData.clipSize;

        if (!ammoReserve.ContainsKey(gunData))
            ammoReserve[gunData] = gunData.maxAmmo - ammoClips[gunData];
    }

    public int GetClipAmount(ShooterData gunData)
    {
        if (!ammoClips.ContainsKey(gunData)) return 0;
        return ammoClips[gunData];
    }

    public int GetReserveAmount(ShooterData gunData)
    {
        if (!ammoReserve.ContainsKey(gunData)) return 0;
        return ammoReserve[gunData];
    }

    public bool HasAmmoInClip(ShooterData gunData)
    {
        return GetClipAmount(gunData) > 0;
    }

    public void UseBullet(ShooterData gunData)
    {
        if (!ammoClips.ContainsKey(gunData)) return;
        if (ammoClips[gunData] <= 0) return;

        ammoClips[gunData]--;
    }

    public void Reload(ShooterData gunData)
    {
        if (!ammoClips.ContainsKey(gunData)) return;
        if (!ammoReserve.ContainsKey(gunData)) return;

        int needed = gunData.clipSize - ammoClips[gunData];
        int toReload = Mathf.Min(needed, ammoReserve[gunData]);

        ammoClips[gunData] += toReload;
        ammoReserve[gunData] -= toReload;
    }

   
    public void AddAmmo(ShooterData gunData, int amount)
    {
        if (gunData == null) return;

        if (!ammoClips.ContainsKey(gunData))
            ammoClips[gunData] = 0;

        if (!ammoReserve.ContainsKey(gunData))
            ammoReserve[gunData] = 0;

        ammoReserve[gunData] += amount;

        if (ammoReserve[gunData] > gunData.maxAmmo)
            ammoReserve[gunData] = gunData.maxAmmo;
    }
}
