using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour


{

    [Header("References")]
    public Camera MainCamera;

    [Header("Prefabs")]
    public WeaponController pistolPrefab;
    public GameObject droppedWeaponPrefab;

    [Header("Sockets")]
    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;

    [Header("Armas")]
    public List<WeaponController> startingWeapons = new List<WeaponController>();
    public int activeWeaponIndex { get; private set; }

    private WeaponController[] weaponSlots = new WeaponController[2];

    void Start()
    {
        activeWeaponIndex = -1;

        foreach (WeaponController startingWeapon in startingWeapons)
        {
            AddWeapon(startingWeapon);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.E) && activeWeaponIndex != -1)
        {
            DropEquippedWeapon();
        }
    }

    private void SwitchWeapon(int p_weaponIndex)
    {
        if (p_weaponIndex != activeWeaponIndex && p_weaponIndex >= 0)
        {
            weaponSlots[p_weaponIndex].gameObject.SetActive(true);
            activeWeaponIndex = p_weaponIndex;
            EventManagement.current.NewGunEvent.Invoke();
        }
    }

    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;

        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null)
            {
                WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
                weaponClone.gameObject.SetActive(false);

                Rigidbody rb = weaponClone.GetComponent<Rigidbody>();
                if (rb != null) Destroy(rb);

                Collider col = weaponClone.GetComponent<Collider>();
                if (col != null) Destroy(col);

                weaponSlots[i] = weaponClone;
                return;
            }
        }
    }

    public void DropEquippedWeapon()
    {
        if (activeWeaponIndex == -1 || weaponSlots[activeWeaponIndex] == null) return;

        WeaponController weaponInstance = weaponSlots[activeWeaponIndex];
        WeaponController weaponOriginalPrefab = pistolPrefab;

        GameObject drop = Instantiate(
            droppedWeaponPrefab,
            weaponParentSocket.position + transform.forward + Vector3.up * 0.1f,
            Quaternion.identity
        );

        WorldWeapon ww = drop.GetComponent<WorldWeapon>();
        if (ww != null)
        {
            ww.weaponToGive = weaponOriginalPrefab;
        }

        Destroy(weaponInstance.gameObject);
        FindFirstObjectByType<ManagerHud>()?.HideWeaponInfo();

        weaponSlots[activeWeaponIndex] = null;
        activeWeaponIndex = -1;
    }

    public void PickUpDroppedWeapon(WeaponController weapon)
    {
        AddWeapon(weapon);
        SwitchWeapon(0);
    }


    public void DamageEnemyInFront(float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, range))
        {
            Enemy1 enemy = hit.collider.GetComponentInParent<Enemy1>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }
    }

    public void AddAmmo(int amount)
    {
        if (activeWeaponIndex != -1 && weaponSlots[activeWeaponIndex] != null)
        {
            WeaponController currentWeapon = weaponSlots[activeWeaponIndex];
            currentWeapon.AddAmmo(amount);
        }
    }

}
