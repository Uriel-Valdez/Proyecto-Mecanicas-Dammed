using UnityEngine;

public class Float : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.5f;  
    public float floatFrequency = 1f;    

    [Header("Rotation Settings")]
    public float rotationSpeed = 45f;    

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
