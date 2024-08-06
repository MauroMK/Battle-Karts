using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    [Header("Weapon Tweaks")]
    [SerializeField] private int itemCd = 10;
    public GameObject[] weaponPrefabs;
    public Weapon[] weaponDataList;

    [Header("Particle")]
    [SerializeField] private GameObject collectParticle;
    [SerializeField] private float EffectScale;
    private Vector3 Scale;

    [Header("Animation Speed")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float heightGoal;
    [SerializeField] private float heightTime;
    private Vector3 initialPosition;
    private LTDescr moveTween;
    private LTDescr rotateTween;

    private string playerTag = "Player";

    protected override void Awake()
    {
        base.Awake();
        initialPosition = transform.position;
    }

    private void Start()
    {
        // Inicia a animação de movimento vertical
        moveTween = LeanTween.moveLocalY(gameObject, heightGoal, heightTime)
            .setEaseInOutSine()
            .setLoopPingPong();

        // Inicia a animação de rotação
        rotateTween = LeanTween.rotateAround(gameObject, Vector3.up, 360f, rotateSpeed)
            .setLoopClamp();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        //TODO fazer algo aqui
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            CarWeaponManager carWeaponManager = other.GetComponentInChildren<CarWeaponManager>();
            if (carWeaponManager != null)
            {
                int randomIndex = Random.Range(0, weaponPrefabs.Length);

                // Equipa a arma no primeiro ponto de montagem disponível
                carWeaponManager.EquipWeapon(weaponPrefabs[randomIndex], weaponDataList[randomIndex]);

                // Desativa o GameObject do item por um tempo e depois reativa
                StartCoroutine(PickPowerup(itemCd));
                HandleParticle();
            }
        }
    }

    protected virtual void Collect()
    {
        //TODO fazer algo
    }

    protected virtual void HandleParticle()
    {
        if (collectParticle != null)
        {
            Scale = transform.localScale * EffectScale;
            GameObject temporaryParticle = Instantiate(collectParticle, transform.position, Quaternion.identity);
            temporaryParticle.transform.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
            Destroy(temporaryParticle, 2.0f); // Destroi a partícula após 2 segundos
        }
        else
        {
            Debug.LogWarning("CollectParticle não está definido.");
        }
    }

    protected virtual IEnumerator PickPowerup(float cooldown)
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
