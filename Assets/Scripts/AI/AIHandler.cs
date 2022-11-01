using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    Transform target = null;

    bool isDoingDamage = false;

    WaitForSeconds delayBetweenDamage = new WaitForSeconds(0.5f);

    //Other components
    CharacterMovementHandler characterMovementHandler;
    HPHandler playerHPHandler;

    private void Awake()
    {
        if(transform.CompareTag("Player"))
        {
            Destroy(this);
            return;
        }

        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

            if (playerGameObject != null)
            {
                target = playerGameObject.transform;
                playerHPHandler = playerGameObject.GetComponent<HPHandler>();
            }
        }
        else
        {
            Vector3 vectorToTarget = transform.position - target.position;

            float distanceToPlayer = vectorToTarget.magnitude;

            //Move close to the player but do not attempt to stand on the player
            if (distanceToPlayer < 0.9f)
            {
                //Check if we are close enough to the player to cause damage
                if (!isDoingDamage)
                    StartCoroutine(DoDamageCO());
            }
            else
            {
                vectorToTarget.Normalize();
                characterMovementHandler.SetInput(new Vector2(vectorToTarget.x, vectorToTarget.y) * -1);
            }

        }
    }

    IEnumerator DoDamageCO()
    {
        isDoingDamage = true;
        playerHPHandler.OnHit();

        yield return delayBetweenDamage;

        isDoingDamage = false;

    }
}
