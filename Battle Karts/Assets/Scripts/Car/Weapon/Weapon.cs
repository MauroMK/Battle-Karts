using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public float fireRate;
    public int maxAmmo;
    
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform barrel;

    public ProjectileData projectileData;

    public void Fire()
    {
        maxAmmo -= 1;
        Shoot();
    }

    protected abstract void Shoot();
}
