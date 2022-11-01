using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcaterInputHandler : MonoBehaviour
{
    CharacterMovementHandler characterMovementHandler;

    private void Awake()
    {
        if (!transform.CompareTag("Player"))
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
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        characterMovementHandler.SetInput(inputVector);
    }
}
