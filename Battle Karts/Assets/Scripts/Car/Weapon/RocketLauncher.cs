using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    protected override void Shoot()
    {
        Debug.Log("Shooting rockets");
    }
}
