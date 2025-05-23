using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🎉 Jugador ganó. Cargando escena Win_State...");
            SceneManager.LoadScene("Win_State");
        }
    }
}
