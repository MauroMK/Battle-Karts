using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeaponManager : MonoBehaviour
{
    [Header("Weapon spots")]
    public Transform rocketLauncherMountPoint;
    public Transform minigunMountPoint;
    public Transform mortarMountPoint;

    [Header("Grabbed items")]
    public GameObject[] equippedWeapons;

    [HideInInspector] public Weapon[] weaponScripts;

    private int selectedWeaponIndex = -1;

    private Dictionary<string, Transform> weaponMountMapping;

    private void Start()
    {
        // Inicializa os arrays de armas equipadas e scripts de armas
        equippedWeapons = new GameObject[3];
        weaponScripts = new Weapon[3];

        // Inicia o mapeamento das armas para seus pontos de montagem
        weaponMountMapping = new Dictionary<string, Transform>
        {
            { "RocketLauncher", rocketLauncherMountPoint },
            { "Minigun", minigunMountPoint },
            { "Mortar", mortarMountPoint },
            // Adicione outros tipos de arma e seus pontos de montagem conforme necessário
        };
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

    public void EquipWeapon(GameObject weaponPrefab, Weapon weaponData)
    {
        // Verifica se o tipo de arma tem um ponto de montagem específico
        if (!weaponMountMapping.TryGetValue(weaponData.weaponName, out Transform mountPoint))
        {
            Debug.LogError("Tipo de arma não mapeado para ponto de montagem: " + weaponData.weaponName);
            return;
        }

        int mountPointIndex = -1;

        // Encontra o índice correto no array de armas equipadas
        switch (weaponData.weaponName)
        {
            case "RocketLauncher":
                mountPointIndex = 0;
                break;
            case "Minigun":
                mountPointIndex = 1;
                break;
            case "Mortar":
                mountPointIndex = 2;
                break;
            // Adicione outros tipos de arma conforme necessário
        }

        if (mountPointIndex == -1)
        {
            Debug.LogError("Tipo de arma não reconhecido: " + weaponData.weaponName);
            return;
        }

        // Remove a arma existente no ponto de montagem, se houver
        if (equippedWeapons[mountPointIndex] != null)
        {
            Destroy(equippedWeapons[mountPointIndex]);
        }

        // Instancia a nova arma no ponto de montagem
        equippedWeapons[mountPointIndex] = Instantiate(weaponPrefab, mountPoint.position, mountPoint.rotation, mountPoint);
        equippedWeapons[mountPointIndex].transform.SetParent(mountPoint);

        // Pega o script da arma
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
        if (selectedWeaponIndex == -1)
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
                if (weaponScripts[selectedWeaponIndex].maxAmmo <= 0)
                {
                    Destroy(equippedWeapons[selectedWeaponIndex]);
                }
            }
            else
            {
                Debug.Log("Arma não equipada no índice selecionado.");
            }
        }
        else
        {
            Debug.Log("Nenhuma arma selecionada.");
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
            else
            {
                Debug.Log("Nenhuma arma equipada no índice: " + index);
            }
        }
    }
}
