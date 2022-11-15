using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    bool isProjectileActive = false;

    float speed = 10;

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

        if(hitTransform !=null)
        {
            HPHandler hpHandler = hitTransform.GetComponent<HPHandler>();

            if (hpHandler != null)
                hpHandler.OnHit();

            isProjectileActive = false;

            gameObject.SetActive(false);
        }

        transform.position += transform.up * Time.deltaTime * speed;
    }

    Transform HitCheck()
    {
        Vector2 currentHitCheckPosition = new Vector2(hitCheckTransform.position.x, hitCheckTransform.position.y);

        float distanceTravelled = (lastHitCheckPosition - currentHitCheckPosition).magnitude;

        int numberOfHits = Physics2D.CircleCastNonAlloc(lastHitCheckPosition, hitRadius, transform.forward, raycastHit2Ds, distanceTravelled);

        lastHitCheckPosition = currentHitCheckPosition;

        if (numberOfHits > 0)
        {
            for(int i=0;i< numberOfHits;i++)
            {
                if (raycastHit2Ds[0].transform.CompareTag("Player"))
                    continue;

                if (raycastHit2Ds[0].transform.CompareTag("BlueBottle"))
                    continue;

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

        gameObject.SetActive(true);


    }
}
