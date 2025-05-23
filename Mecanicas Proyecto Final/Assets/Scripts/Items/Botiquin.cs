using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public int cantidadCura = 1;
    public AudioClip pickupSound;       // 1) Arrastra aquí el clip desde el Inspector
    private AudioSource audioSource;    // 2) Referencia al componente AudioSource

    private void Awake()
    {
        // 3) Obtenemos el AudioSource que añadimos al prefab
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("Falta AudioSource en el Botiquín");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vida = other.GetComponent<PlayerHealth>();
            if (vida != null)
            {
                // 4) Curamos al jugador
                vida.Heal(cantidadCura);

                // 5) Reproducimos el sonido de recogida
                if (pickupSound != null)
                    audioSource.PlayOneShot(pickupSound);

                // 6) Destruimos el botiquín tras un pequeño delay para que suene el clip
                Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
            }
        }
    }
}
