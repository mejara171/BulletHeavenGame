using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    Vector2 inputVector;

    float moveSpeed = 40;
    float maxSpeed = 3f;
    float idleFriction = 0.5f;

    bool isPlayer = false;

    //Other components
    Rigidbody2D rigidbody2D_;
    CharacterDataHandler characterDataHandler;

    private void Awake()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        characterDataHandler = GetComponent<CharacterDataHandler>();

        maxSpeed = characterDataHandler.characterData.MovementSpeedMax;

        isPlayer = CompareTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputVector != Vector2.zero)
        {
            rigidbody2D_.velocity = Vector2.ClampMagnitude(rigidbody2D_.velocity + (inputVector * moveSpeed * Time.fixedDeltaTime), maxSpeed);

            if (inputVector.x > 0)
                spriteRenderer.flipX = false;
            else if(inputVector.x < 0)
                spriteRenderer.flipX = true;


        }
        else
        {
            //Reduce movement speed when no buttons are pressed. 
            rigidbody2D_.velocity = Vector2.Lerp(rigidbody2D_.velocity, Vector2.zero, idleFriction);
        }


        /*
        rigidbody2D_.AddForce(inputVector * accelerationSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        rigidbody2D_.velocity = Vector2.ClampMagnitude(rigidbody2D_.velocity, maxSpeed);
        */
    }

    public void SetInput(Vector2 newInput)
    {
        inputVector = newInput;

    }

    public Vector2 GetLastInput()
    {
        return inputVector;
    }

    public void OnUpgrade(float newMovementSpeed)
    {
        maxSpeed = newMovementSpeed;
    }

    //Events
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (!isPlayer) 
            return;

        if(otherCollider.CompareTag("BlueBottle"))
        {
            Transform bottleRoot = otherCollider.transform.root;

            bottleRoot.GetComponent<BottleHandler>().PullBottleToPlayer(transform);

            //Disable the collider
            otherCollider.enabled = false;
        }
    }
}
