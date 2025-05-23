using UnityEngine;

public class DesaparecerAlPresionar : MonoBehaviour
{
    public GameObject objetoExtra; // El otro objeto que también desaparecerá

    public void Desaparecer()
    {
        // Desaparece este objeto
        gameObject.SetActive(false);

        // Desaparece el objeto extra (si fue asignado)
        if (objetoExtra != null)
        {
            objetoExtra.SetActive(false);
        }
    }
}

