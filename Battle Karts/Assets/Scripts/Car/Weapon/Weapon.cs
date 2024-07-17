using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public float fireRate;
    public int currentAmmo;
    public int maxAmmo;
    
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform barrel;

    public ProjectileData projectileData;

    public void Start()
    {
        currentAmmo = maxAmmo;
    }

    public void Fire()
    {
        currentAmmo -= 1;
        Shoot();
    }

    protected abstract void Shoot();
}
