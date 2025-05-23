using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public int cantidadCura = 1;
    public AudioClip pickupSound;       // 1) Arrastra aqu� el clip desde el Inspector
    private AudioSource audioSource;    // 2) Referencia al componente AudioSource

    private void Awake()
    {
        // 3) Obtenemos el AudioSource que a�adimos al prefab
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("Falta AudioSource en el Botiqu�n");
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

                // 6) Destruimos el botiqu�n tras un peque�o delay para que suene el clip
                Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
            }
        }
    }
}
