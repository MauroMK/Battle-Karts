using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    private float explosionRadius;

    public new void Start()
    {
        base.Start();
        Move();
    }

    public new void Update()
    {
        base.Update();
    }

    public void Initialize(RocketData data)
    {
        base.Initialize(data);
        explosionRadius = data.explosionRadius;
    }

    protected override void Move()
    {
        projectileRb.velocity = transform.forward * speed;
    }
}
