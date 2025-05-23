using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public EnemyStun enemigo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemigo.Activar();
            Destroy(gameObject); // opcional: se destruye después de activarlo
        }
    }
}
