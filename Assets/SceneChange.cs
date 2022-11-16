using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public int Nivel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarEscena(Nivel);
        }
    }

    public void CambiarEscena(int Nivel1)
    {
        SceneManager.LoadScene(Nivel1);
    }
}
