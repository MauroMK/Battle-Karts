using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    protected BoxCollider boxCollider;
    protected MeshRenderer meshRenderer;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Debug.Log("This object collided with " + this.name);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered the " + this.name);
    }
}
  