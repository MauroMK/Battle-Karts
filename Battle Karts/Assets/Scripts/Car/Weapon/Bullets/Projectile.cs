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

    private Fighter fighterScript;

    public void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        fighterScript = FindObjectOfType<Fighter>();
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

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fighterScript.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }

    protected abstract void Move();
}
