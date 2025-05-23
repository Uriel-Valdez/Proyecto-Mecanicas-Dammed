using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject puertaADestruir; // arrastra aquí el objeto que quieres que desaparezca

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (puertaADestruir != null)
            {
                puertaADestruir.SetActive(false); // también puedes usar Destroy(puertaADestruir);
                Debug.Log("🔑 Llave recogida. Puerta desactivada.");
            }

            Destroy(gameObject); // destruye la llave
        }
    }
}
