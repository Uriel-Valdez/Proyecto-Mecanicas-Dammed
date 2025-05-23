using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 3f;
    public int maxHits = 3;

    private int currentHits = 0;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit(transform.position);
            }

            
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
