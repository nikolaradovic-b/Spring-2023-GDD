using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    private Gun currentGun;

    private void Start() {
        currentGun = new Minigun(firingOrigin, bulletPrefab);
    }

    private void Update() {
        currentGun.Update();
        Debug.Log(currentGun.ammoCount());
    }

    public void Reload() {
        currentGun.Reload();
    }

    public void switchMinigun() {
        currentGun = new Minigun(firingOrigin, bulletPrefab);
    }

    public void switchPistol() {
        currentGun = new Pistol(firingOrigin, bulletPrefab);
    }

    public int ammoCount() {
        return currentGun.ammoCount();
    }
}
