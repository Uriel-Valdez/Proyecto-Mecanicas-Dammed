using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class EnemyStun : MonoBehaviour
{
    [Header("Movement Settings")]
    public float detectionRadius = 10f;
    public float speed = 3.5f;

    [Header("Stun Settings")]
    public int hitsToStun = 3;
    public float stunDuration = 5f;

    private int currentHits = 0;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private bool isActive = false;

    private Transform player;
    private NavMeshAgent agent;
    private Collider enemyCollider;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

        agent.speed = speed;
        agent.isStopped = true; // empieza inactivo

        audioSource.Stop(); // asegurar que el sonido no esté sonando al principio
    }

    private void Update()
    {
        if (!isActive) return;

        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                currentHits = 0;
                agent.isStopped = false;
                enemyCollider.isTrigger = false; // vuelve a colisionar normalmente
            }
            return;
        }

        // Perseguir jugador si está cerca
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    public void TakeDamage()
    {
        if (!isActive || isStunned) return;

        currentHits++;
        if (currentHits >= hitsToStun)
        {
            EnterStun();
        }
    }

    private void EnterStun()
    {
        isStunned = true;
        stunTimer = stunDuration;
        agent.isStopped = true;
        enemyCollider.isTrigger = true; // se puede atravesar
        Debug.Log("🛑 Enemigo aturdido por " + stunDuration + " segundos");
    }

    public void Activar()
    {
        isActive = true;
        agent.isStopped = false;

        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // 🔊 inicia sonido del enemigo
        }

        Debug.Log("✅ Enemigo activado");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive || isStunned) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit(transform.position);
            }
        }
    }
}
