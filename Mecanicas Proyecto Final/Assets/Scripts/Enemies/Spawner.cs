
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 10;

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 pos = new Vector3(i * 2, 0, 0); // separarlos un poco
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
