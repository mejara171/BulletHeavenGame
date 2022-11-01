using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHandler : MonoBehaviour
{
    WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PullBottleToPlayer(Transform playerTransform)
    {
        StartCoroutine(PullBottleTowardsPlayerCO(playerTransform));
    }

    IEnumerator PullBottleTowardsPlayerCO(Transform playerTransform)
    {
        while (true)
        {
            Vector3 directionTowardsPlayer = transform.position - playerTransform.position;

            float distanceToPlayer = directionTowardsPlayer.magnitude;

            directionTowardsPlayer.Normalize();

            transform.position -= directionTowardsPlayer * Time.deltaTime * 7;

            if (distanceToPlayer < 0.25f)
            {
                playerTransform.GetComponent<StatsHandler>().OnCollectXP();
                Destroy(gameObject);
            }

            yield return null;
        }

    }

}
