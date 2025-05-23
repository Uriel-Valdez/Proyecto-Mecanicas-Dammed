using UnityEngine;

public class RenderingActivation : MonoBehaviour
{
    public Transform player; // Arrástralo en el Inspector
    public float activationDistance = 30f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Activa solo si está cerca
        if (distance < activationDistance)
        {
            SetActiveState(true);
        }
        else
        {
            SetActiveState(false);
        }
    }

    void SetActiveState(bool state)
    {
        if (gameObject.activeSelf != state)
        {
            gameObject.SetActive(state);
        }
    }
}
