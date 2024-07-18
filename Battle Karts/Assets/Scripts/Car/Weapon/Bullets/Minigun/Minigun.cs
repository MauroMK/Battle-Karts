using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Projectile
{
    private ParticleSystem _particleSystem;

    public new void Start()
    {
        base.Start();
        Move();
    }

    public new void Update()
    {
        base.Update();
    }

    public void Initialize(MinigunData data)
    {
        base.Initialize(data);
        _particleSystem = data.muzzleFlashParticle;
    }

    protected override void Move()
    {
        projectileRb.velocity = transform.forward * speed;
    }
}
