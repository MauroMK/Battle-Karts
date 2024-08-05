using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreaksOnCollide : MonoBehaviour
{
    public GameObject EffectOnCollide;
    private string playerTag = "Player";
    public float EffectScale;
    private Vector3 Scale;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Scale = gameObject.GetComponent<Transform>().localScale * EffectScale;
            EffectOnCollide.transform.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
            Instantiate(EffectOnCollide, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
}