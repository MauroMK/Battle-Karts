using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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

    public void Update()
    {
        ProjectileDuration();
    }

    public void Initialize(ProjectileData data)
    {
        speed = data.speed;
        damage = data.damage;
        lifetime = data.lifetime;
    }

    public void ProjectileDuration()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void Move();
}
