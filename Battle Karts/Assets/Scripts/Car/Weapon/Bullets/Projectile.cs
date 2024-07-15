using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float speed;
    protected int damage;
    protected float lifetime;
    protected Rigidbody projectileRb;

    public void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    public void Initialize(ProjectileData data)
    {
        speed = data.speed;
        damage = data.damage;
        lifetime = data.lifetime;
    }

    protected abstract void Move();
}
