using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarWeaponManager : MonoBehaviour
{
    [Header("Weapon spots")]
    public Transform[] weaponMountPoints;

    [Header("Grabbed items")]
    public GameObject[] equippedWeapons;
    
    
    [HideInInspector] public Weapon[] weaponScripts;

    private int[] currentAmmoArray;
    
    private int selectedWeaponIndex = 0;

    private void Start()
    {
        // Inicializa os arrays de armas equipadas, dados e munição
        equippedWeapons = new GameObject[weaponMountPoints.Length];
        weaponScripts = new Weapon[weaponMountPoints.Length];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireSelectedWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
    }

    public void EquipWeapon(GameObject weaponPrefab, Weapon weaponData, int  mountPointIndex)
    {
        if (mountPointIndex < 0 || mountPointIndex >= weaponMountPoints.Length)
        {
            Debug.LogError("Índice de ponto de montagem inválido.");
            return;
        }

        // Remove a arma existente no ponto de montagem, se houver
        if (equippedWeapons[mountPointIndex] != null)
        {
            Destroy(equippedWeapons[mountPointIndex]);
        }

        //* Instancia a nova arma no ponto de montagem
        equippedWeapons[mountPointIndex] = Instantiate(weaponPrefab, weaponMountPoints[mountPointIndex].position, weaponMountPoints[mountPointIndex].rotation, weaponMountPoints[mountPointIndex]);
        equippedWeapons[mountPointIndex].transform.SetParent(weaponMountPoints[mountPointIndex]);
        
        //* Pega o script da arma
        weaponScripts[mountPointIndex] = equippedWeapons[mountPointIndex].GetComponent<Weapon>();
        if (weaponScripts[mountPointIndex] == null)
        {
            Debug.LogError("O prefab da arma não contém um componente Weapon.");
            return;
        }

        // Associa os dados da arma
        weaponScripts[mountPointIndex].weaponName = weaponData.weaponName;
        weaponScripts[mountPointIndex].fireRate = weaponData.fireRate;
        weaponScripts[mountPointIndex].maxAmmo = weaponData.maxAmmo;

        // Seleciona a arma equipada se for a primeira
        if (mountPointIndex == 0 && selectedWeaponIndex == -1)
        {
            selectedWeaponIndex = mountPointIndex;
        }
    }

    public void FireSelectedWeapon()
    {
        if (selectedWeaponIndex >= 0 && selectedWeaponIndex < equippedWeapons.Length)
        {
            if (equippedWeapons[selectedWeaponIndex] != null)
            {
                weaponScripts[selectedWeaponIndex].Fire();
            }

            if (equippedWeapons[selectedWeaponIndex] == null)
            {
                Debug.Log("não pegou a arma");
            }
        }
    }
    
    private void SelectWeapon(int index)
    {
        if (index >= 0 && index < equippedWeapons.Length)
        {
            if (equippedWeapons[index] != null)
            {
                selectedWeaponIndex = index;
                Debug.Log("Selected weapon: " + index);
            }
        }
    }
}
