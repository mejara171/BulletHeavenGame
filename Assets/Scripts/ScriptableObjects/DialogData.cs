using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New dialog data ", menuName = "Dialog data", order = 55)]
public class DialogData : ScriptableObject
{
    [SerializeField]
    private bool isPerson1Speaking;

    public bool IsPerson1Speaking
    {
        get
        {
            return isPerson1Speaking;
        }
    }

    [SerializeField]
    private string dialogString;

    public string DialogString
    {
        get
        {
            return dialogString;
        }
    }

}
