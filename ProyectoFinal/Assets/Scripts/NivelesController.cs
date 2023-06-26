using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelesController : MonoBehaviour
{
    void Start(){

    }
    public void Niveluno()
    {
        SceneManager.LoadScene(2);
    }
    public void Niveldos()
    {
        SceneManager.LoadScene(3);
    }
    public void Niveltres()
    {
        SceneManager.LoadScene(4);
    }
}
