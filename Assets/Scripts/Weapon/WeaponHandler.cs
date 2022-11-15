using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform weaponFirePointTransform;


    WaitForSeconds projectileFireDelay;

    Vector3 fireDirection = new Vector3(1, 0, 0);


    CharacterMovementHandler characterMovementHandler;
    ProjectilePoolHandler projectilePoolHandler;
    SFXHandler sfxHandler;

    private void Awake()
    {
        projectilePoolHandler = Camera.main.GetComponent<ProjectilePoolHandler>();

        characterMovementHandler = GetComponent<CharacterMovementHandler>();

        sfxHandler = GetComponent<SFXHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        projectileFireDelay = new WaitForSeconds(1);

        StartCoroutine(FireProjectilesCO());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDirection = characterMovementHandler.GetLastInput();

        if (inputDirection != Vector2.zero)
        {
            fireDirection = inputDirection;
            fireDirection.Normalize();
        }
    }

    IEnumerator FireProjectilesCO()
    {
        yield return projectileFireDelay;

        while (true)
        {
            sfxHandler.PlayThrowKnifeSFX();
            projectilePoolHandler.FireProjectile(weaponFirePointTransform.position + fireDirection * 0.9f, fireDirection);
            yield return projectileFireDelay;
        }

    }

    public void OnUpgrade(float newfireDelay)
    {
        projectileFireDelay = new WaitForSeconds(newfireDelay);
    }
}
