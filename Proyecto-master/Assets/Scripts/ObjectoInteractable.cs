using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectoInteractable : MonoBehaviour
{
    public Textos textos;
    private void OnMouseDown() 
    {
        
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
}
