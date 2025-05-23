using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource;           
    public float flickerInterval = 0.5f; 

    private float timer;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>(); 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flickerInterval)
        {
            lightSource.enabled = !lightSource.enabled; 
            timer = 0f;
        }
    }
}
