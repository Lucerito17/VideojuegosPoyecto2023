using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        
    }
    public void NivelPrueba()
    {
        SceneManager.LoadScene(0);
    }
    public void Regresar()
    {
        SceneManager.LoadScene(6);
    }
}
