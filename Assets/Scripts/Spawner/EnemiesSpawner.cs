using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyCO());
    }

    IEnumerator SpawnEnemyCO()
    {
        WaitForSeconds waitTimeSpawnEnemies = new WaitForSeconds(5);

        yield return waitTimeSpawnEnemies;

        while (true)
        {
            Vector2 randomDirection = Random.insideUnitCircle;

            Instantiate(enemyPrefab, transform.position + (Vector3.one * 0.5f + new Vector3(randomDirection.x, randomDirection.y,transform.position.z)), Quaternion.identity);

            yield return waitTimeSpawnEnemies;
        }

    }

}
