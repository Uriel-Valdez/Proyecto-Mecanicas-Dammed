using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowFactor = 0.5f;       // Multiplicador de velocidad (ej. 0.5 = 50% m�s lento)
    public float slowDuration = 3f;       // Cu�nto dura el efecto en segundos

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySlow(slowFactor, slowDuration);
            }
        }
    }
}
