using UnityEngine;
using UnityEngine.SceneManagement;

public class Fail_State : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}