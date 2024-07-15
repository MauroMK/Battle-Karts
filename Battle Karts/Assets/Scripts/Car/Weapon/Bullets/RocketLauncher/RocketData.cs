using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rocket Data", menuName = "Projectile Data/Rocket")]
public class RocketData : ProjectileData
{
    public float explosionRadius;
    public ParticleSystem explosionParticles;
}
