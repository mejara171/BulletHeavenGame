using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character data ", menuName = "Character data", order = 55)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private int hp = 2;
    public int HP
    {
        get
        {
            return hp;
        }
    }

    [SerializeField]
    private float movementSpeedMax = 3;

    public float MovementSpeedMax
    {
        get
        {
            return movementSpeedMax;
        }
    }

}
