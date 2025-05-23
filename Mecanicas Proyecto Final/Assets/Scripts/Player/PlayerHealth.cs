using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHits = 3;
    public float knockbackForce = 4f;
    public float knockbackDuration = 0.2f;
    public float immunityTime = 1.5f;

    private int currentHits = 0;
    private bool isImmune = false;
    private float immuneTimer = 0f;
    private float knockbackTimer = 0f;
    private Vector3 knockbackDirection;

    private CharacterController characterController;
    private DamageFlash damageFlash;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        damageFlash = Object.FindFirstObjectByType<DamageFlash>();
        if (damageFlash == null)
            Debug.LogWarning("No se encontró ningún DamageFlash en la escena.");
    }

    private void Update()
    {
        if (isImmune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0f)
                isImmune = false;
        }

        if (knockbackTimer > 0f)
        {
            characterController.Move(knockbackDirection * knockbackForce * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;
        }
    }

    public void TakeHit(Vector3 hitSourcePosition)
    {
        if (isImmune) return;

        currentHits++;
        int remainingLives = maxHits - currentHits;
        Debug.Log($"El jugador fue golpeado. Vidas restantes: {remainingLives}");

        if (damageFlash != null)
            damageFlash.HandleLifeChange(remainingLives); // ✅ Activamos el flash porque hubo daño

        knockbackDirection = (transform.position - hitSourcePosition).normalized;
        knockbackDirection.y = 0f;
        knockbackTimer = knockbackDuration;

        isImmune = true;
        immuneTimer = immunityTime;

        if (remainingLives <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        int previousHits = currentHits;
        currentHits -= amount;
        if (currentHits < 0) currentHits = 0;

        int remainingLives = maxHits - currentHits;
        Debug.Log($"El jugador se curó. Vidas restantes: {remainingLives}");

        // ✅ Solo llamamos al flash si la curación fue desde 1 vida
        if (damageFlash != null)
        {
            int oldRemainingLives = maxHits - previousHits;

            // Solo actualizamos el flash si estaba en modo permanente y ya no lo está
            if (oldRemainingLives <= 1 && remainingLives > 1)
            {
                damageFlash.HandleLifeChange(remainingLives); // para quitar el flash fijo
            }
        }
    }

    private void Die()
    {
        Debug.Log("GAME OVER - El jugador ha muerto.");
        SceneManager.LoadScene("Fail_State");
    }
}
