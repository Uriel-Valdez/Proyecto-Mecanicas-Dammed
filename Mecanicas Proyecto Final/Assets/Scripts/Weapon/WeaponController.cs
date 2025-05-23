using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("References")]
    public Transform weaponMuzzle;

    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletholeprefab;

    [Header("Shoot Parameters")]
    public float fireRange = 200;
    public float recoildForce = 4f;
    public float fireRate = 0.5f;
    public int maxAmmo = 8;

    [Header("Reload Parameters")]
    public float reloadTime = 1.0f;

    public int currentAmmo { get; private set; }

    private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header("Sounds & Visuals")]
    public GameObject flashEffect;
    public AudioSource shootSound; // 🔊 NUEVO: arrástralo en el Inspector

    private Transform cameraPlayerTransform;
    private Vector3 initialLocalPos;

    private void Awake()
    {
        currentAmmo = 8;
        ammoReserve = 0;
        EventManagement.current.updateBulletsEvent.Invoke(currentAmmo, ammoReserve);
    }

    private void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        initialLocalPos = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPos, Time.deltaTime * 5f);
    }

    private bool TryShoot()
    {
        if (lastTimeShoot + fireRate < Time.time && currentAmmo >= 1)
        {
            HandleShoot();
            currentAmmo--;
            EventManagement.current.updateBulletsEvent.Invoke(currentAmmo, ammoReserve);
            lastTimeShoot = Time.time;
            return true;
        }
        return false;
    }

    private void HandleShoot()
    {
        // 🔊 Reproducir sonido de disparo
        if (shootSound != null)
        {
            shootSound.Play();
        }

        if (flashEffect != null && weaponMuzzle != null)
        {
            GameObject flashClone = Instantiate(flashEffect, weaponMuzzle.position, Quaternion.identity, transform);
            Destroy(flashClone, 1f);
        }

        AddRecoil();

        RaycastHit hit;
        if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, fireRange, hittableLayers))
        {
            if (bulletholeprefab != null)
            {
                GameObject bulletHoleClone = Instantiate(
                    bulletholeprefab,
                    hit.point + hit.normal * 0.001f,
                    Quaternion.LookRotation(hit.normal),
                    hit.transform
                );
                bulletHoleClone.transform.localScale = Vector3.one * 0.4f;
                Destroy(bulletHoleClone, 4f);
            }

            if (hit.collider.TryGetComponent<EnemyStun>(out EnemyStun enemyStun))
            {
                enemyStun.TakeDamage();
            }
            else if (hit.collider.TryGetComponent<Enemy1>(out Enemy1 enemy))
            {
                enemy.TakeDamage();
            }
        }
    }

    private void AddRecoil()
    {
        transform.localPosition -= new Vector3(0f, 0f, recoildForce / 100f);
    }

    private IEnumerator Reload()
    {
        if (currentAmmo == maxAmmo || ammoReserve == 0)
            yield break;

        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(neededAmmo, ammoReserve);

        currentAmmo += ammoToReload;
        ammoReserve -= ammoToReload;

        EventManagement.current.updateBulletsEvent.Invoke(currentAmmo, ammoReserve);
        Debug.Log("Recargado");
    }

    public void AddAmmo(int amount)
    {
        ammoReserve += amount;
        EventManagement.current.updateBulletsEvent.Invoke(currentAmmo, ammoReserve);
    }

    public int ammoReserve { get; private set; } = 0;
}
