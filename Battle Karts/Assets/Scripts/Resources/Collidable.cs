using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private BoxCollider boxCollider;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Debug.Log("This object collided with " + this.name);
    }
}
