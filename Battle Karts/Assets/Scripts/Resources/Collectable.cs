using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    [SerializeField] private int itemCd = 10;
    public GameObject[] weaponPrefabs;
    public Weapon[] weaponDataList;

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
        if (other.gameObject.CompareTag("Player"))
        {
            CarWeaponManager carWeaponManager = other.GetComponentInChildren<CarWeaponManager>();
            if (carWeaponManager != null)
            {
                int randomIndex = Random.Range(0, weaponPrefabs.Length);

                // Verifica se há um ponto de montagem disponível para equipar a arma
                int availableMountPointIndex = -1;
                for (int i = 0; i < carWeaponManager.equippedWeapons.Length; i++)
                {
                    if (carWeaponManager.equippedWeapons[i] == null)
                    {
                        availableMountPointIndex = i;
                        break;
                    }
                }

                if (availableMountPointIndex != -1)
                {
                    // Equipa a arma no ponto de montagem disponível
                    carWeaponManager.EquipWeapon(weaponPrefabs[randomIndex], weaponDataList[randomIndex]);

                    // Desativa o GameObject do item por um tempo e depois reativa
                    StartCoroutine(PickPowerup(itemCd));
                }
                else
                {
                    Debug.Log("No available mount points for weapons.");
                }
            }
        }
    }

    protected virtual void Collect()
    {
        Debug.Log("Aqui ta errado");
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
