using UnityEngine;

public class Llave : MonoBehaviour
{
    public GameObject puerta; // arrástrala en el Inspector
    public float distanciaInteraccion = 3f; // qué tan cerca debe estar el jugador
    public Transform jugador; // también lo asignas en el Inspector

    private bool yaActivado = false; // para evitar múltiples activaciones

    void Update()
    {
        if (yaActivado) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);

        if (distancia <= distanciaInteraccion)
        {
            if (puerta != null)
            {
                puerta.SetActive(false); // desaparece la puerta
            }

            gameObject.SetActive(false); // desaparece la llave
            yaActivado = true;
        }
    }
}
