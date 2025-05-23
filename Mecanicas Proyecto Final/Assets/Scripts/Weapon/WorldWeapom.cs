using UnityEngine;

public class WorldWeapon : MonoBehaviour
{
    public WeaponController weaponToGive;

    private bool playerInRange = false;
    private PlayerWeaponController player;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (player != null)
            {
                player.PickUpDroppedWeapon(weaponToGive);
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerWeaponController>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}
