using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 5;  // ✅ Cantidad de balas que dará este cubo

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponController weaponController = other.GetComponent<PlayerWeaponController>();
            if (weaponController != null)
            {
                weaponController.AddAmmo(ammoAmount);
                Destroy(gameObject);  // ✅ Destruye el cubo después de recogerlo
            }
        }
    }
}
