using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public float interactionDistance = 3f; // distancia para poder interactuar

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Lanza un rayo desde la cámara hacia adelante
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // Verifica si el objeto tocado tiene el script "DesaparecerAlPresionarE"
                DesaparecerAlPresionar target = hit.collider.GetComponent<DesaparecerAlPresionar>();
                if (target != null)
                {
                    target.Desaparecer();
                }
            }
        }
    }
}
