using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    private Gun currentGun;

    private void Start() {
        currentGun = new Pistol(firingOrigin, bulletPrefab);
    }

    private void Update() {
        currentGun.Update();
    }

    public void Reload() {
        currentGun.ammo = currentGun.Reload(currentGun.ammo, currentGun.reloadAmount, currentGun.maxAmmo);
    }

    public void Switch(Gun newGun) {
        currentGun = newGun;
    }
}
