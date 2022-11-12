using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo;
    public int reloadAmount;
    public int maxAmmo;

    // Update is called once per frame
    public virtual void Update()
    {
        //to be overwritten
    }

    public int Reload(int ammo, int amount, int maxAmmo)
    {
        return Mathf.Min(ammo + amount, maxAmmo);
    }
}
