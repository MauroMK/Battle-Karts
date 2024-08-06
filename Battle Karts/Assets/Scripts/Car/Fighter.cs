using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private float totalHp;
    [SerializeField] private float currentHp;

    private void Start()
    {
        currentHp = totalHp;
    }

    private void Update()
    {
        Die();
    }

    private void Die()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float bulletDamage)
    {
        Debug.Log(gameObject + " took " + bulletDamage + " damage.");
        currentHp -= bulletDamage;
    }
}
