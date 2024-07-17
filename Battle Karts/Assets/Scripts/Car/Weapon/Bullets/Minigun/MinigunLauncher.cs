using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunLauncher : Weapon
{
    protected override void Shoot()
    {
        GameObject minigun = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Minigun minigunScript = minigun.GetComponent<Minigun>();
        minigunScript.Initialize((MinigunData)projectileData);
    }
}
