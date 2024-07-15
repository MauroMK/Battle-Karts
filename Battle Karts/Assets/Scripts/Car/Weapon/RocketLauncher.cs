using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    protected override void Shoot()
    {
        GameObject rocket = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rocket rocketScript = rocket.GetComponent<Rocket>();
        rocketScript.Initialize((RocketData)projectileData);
    }
}
