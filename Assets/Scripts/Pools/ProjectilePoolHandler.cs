using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolHandler : MonoBehaviour
{
    public GameObject projectilePrefab;

    const int poolSize = 20;

    ProjectileHandler[] objectPool;
    int poolIndex = 0;

    private void Awake()
    {
        //Allocate space for the pool
        objectPool = new ProjectileHandler[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Instantiate object off screen
            GameObject instantiatedObject = Instantiate(projectilePrefab, new Vector3(10000, 10000, 0), Quaternion.identity);

            instantiatedObject.SetActive(false);

            objectPool[i] = instantiatedObject.GetComponent<ProjectileHandler>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void FireProjectile(Vector3 postion, Vector3 forwardDirection)
    {
        //Grab a projectile from the pool and fire it 
        objectPool[poolIndex].FireProjectile(postion, forwardDirection);

        poolIndex++;

        if (poolIndex > objectPool.Length - 1)
            poolIndex = 0;
    }
   
}
