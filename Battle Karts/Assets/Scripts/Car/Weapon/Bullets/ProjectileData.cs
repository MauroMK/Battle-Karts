using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Projectile Data/Base")]
public class ProjectileData : ScriptableObject
{
    public float speed;
    public int damage;
    public float lifetime;
}
