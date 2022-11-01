using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    //State 
    bool isProjectileActive = false;

    //Speed for projectile
    float speed = 10;

    //Hit check
    float hitRadius = 0.3f;
    Transform hitCheckTransform;

    Vector2 lastHitCheckPosition;

    RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[4];

    private void Awake()
    {
        CircleCollider2D tempCircleCollider = GetComponentInChildren<CircleCollider2D>();

        hitRadius = tempCircleCollider.radius;
        hitCheckTransform = tempCircleCollider.transform;

        lastHitCheckPosition = hitCheckTransform.position;

        //We only get the size of the collider so we can use it later on for overlapp checks
        Destroy(tempCircleCollider);

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isProjectileActive)
            return;

        Transform hitTransform = HitCheck();

        //Check if something was hit
        if(hitTransform !=null)
        {
            //Check if it has a HP handler
            HPHandler hpHandler = hitTransform.GetComponent<HPHandler>();

            if (hpHandler != null)
                hpHandler.OnHit();

            //We have hit something so the projectile should no longer be active. 
            isProjectileActive = false;

            //Disable the game object
            gameObject.SetActive(false);
        }

        //Move forward
        transform.position += transform.up * Time.deltaTime * speed;
    }

    Transform HitCheck()
    {
        Vector2 currentHitCheckPosition = new Vector2(hitCheckTransform.position.x, hitCheckTransform.position.y);

        float distanceTravelled = (lastHitCheckPosition - currentHitCheckPosition).magnitude;

        //Check if we have hit something along the path
        int numberOfHits = Physics2D.CircleCastNonAlloc(lastHitCheckPosition, hitRadius, transform.forward, raycastHit2Ds, distanceTravelled);

        //Update the position so we can perform a check from the new position
        lastHitCheckPosition = currentHitCheckPosition;

        if (numberOfHits > 0)
        {
            for(int i=0;i< numberOfHits;i++)
            {
                //Skip the player, knives cannot hit the player
                if (raycastHit2Ds[0].transform.CompareTag("Player"))
                    continue;

                //Skip bottles
                if (raycastHit2Ds[0].transform.CompareTag("BlueBottle"))
                    continue;

                //Return the hit
                return (raycastHit2Ds[i].transform);
            }
        }
        
        return null;
    }

    public void FireProjectile(Vector3 position, Vector3 forward)
    {
        transform.position = position;
        transform.up = forward;
        lastHitCheckPosition = hitCheckTransform.position;

        isProjectileActive = true;

        //Activate game object.
        gameObject.SetActive(true);

       // CGUtils.DebugLog($"Fired projectile");

    }
}
