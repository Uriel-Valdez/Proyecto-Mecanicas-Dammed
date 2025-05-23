using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [Header("Flash Damage")]
    public Image flashImage;               // Tu imagen full-screen
    public float flashDuration = 0.5f;     // Duración del fade para >1 vida
    public Color flashColor = new Color(1, 0, 0, 0.4f);

    // Color con alfa = 130/255 para el flash fijo (1 vida)
    private readonly Color permanentFlashColor = new Color(1f, 0f, 0f, 0.04f);

    private Color transparentColor;
    private float timer;
    private bool isFlashing = false;
    private bool permanentFlash = false;   // Indica que estamos en 1 vida

    private void Start()
    {
        transparentColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        flashImage.color = transparentColor;
    }

    private void Update()
    {
        if (isFlashing && !permanentFlash)
        {
            timer -= Time.deltaTime;
            flashImage.color = Color.Lerp(flashColor, transparentColor, 1 - (timer / flashDuration));

            if (timer <= 0f)
            {
                flashImage.color = transparentColor;
                isFlashing = false;
            }
        }
    }

    /// <summary>
    /// Llamar cada vez que cambie el número de vidas.
    /// currentLives = corazones que le quedan al jugador.
    /// </summary>
    public void HandleLifeChange(int currentLives)
    {
        // 1) Si quedan más de 1 vida: resetea el modo permanente y lanza un flash
        if (currentLives > 1)
        {
            permanentFlash = false;
            TriggerFlash();
        }
        // 2) En estado crítico: 1 vida => fija el flash con alfa = 130
        else if (currentLives == 1)
        {
            permanentFlash = true;
            isFlashing = false;
            flashImage.color = permanentFlashColor;
        }
        // 3) Muerto (0 o menos): quita todo
        else // currentLives <= 0
        {
            permanentFlash = false;
            isFlashing = false;
            flashImage.color = transparentColor;
        }
    }

    private void TriggerFlash()
    {
        flashImage.color = flashColor;
        timer = flashDuration;
        isFlashing = true;
    }
}
