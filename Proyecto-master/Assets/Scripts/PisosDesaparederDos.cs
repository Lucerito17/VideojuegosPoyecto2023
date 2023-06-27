using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisosDesaparederDos : MonoBehaviour
{
    public GameObject[] objetos;
    public float tiempoDesaparicion = 6f;

    private void Start()
    {
        InvokeRepeating("AlternarEstadoObjetos", tiempoDesaparicion, tiempoDesaparicion);
    }

    private void AlternarEstadoObjetos()
    {
        foreach (GameObject objeto in objetos)
        {
            bool estadoActual = objeto.activeSelf;
            objeto.SetActive(!estadoActual);
        }
    }
}
