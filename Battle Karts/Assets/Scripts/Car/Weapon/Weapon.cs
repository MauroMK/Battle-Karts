using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    public Transform[] weaponAttachLocal;

    protected virtual void WeaponShoot()
    {
        
    }

    protected virtual void WeaponMeshSpawn()
    {
        //TODO when pickup the weapon, do an animation equipping it
    }

}
