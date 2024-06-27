using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    [SerializeField] private int itemCd = 10;
    
    protected bool collected;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Collect();
    }

    protected virtual void Collect()
    {
        collected = true;

        StartCoroutine(PickPowerup(itemCd));
    }

    IEnumerator PickPowerup(float cooldown)
    {
        Debug.Log("Start Pickup");
        boxCollider.enabled = false;
        meshRenderer.enabled = false;

        yield return new WaitForSeconds(cooldown);

        Debug.Log("Finish");
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }


}
