using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float range;
    public float fireRate;
    public int maxAmmo;

    public void Fire()
    {
        Shoot();
    }

    protected abstract void Shoot();

    protected virtual void MeshSpawn()
    {
        //TODO when pickup the weapon, do an animation equipping it
    }

}
