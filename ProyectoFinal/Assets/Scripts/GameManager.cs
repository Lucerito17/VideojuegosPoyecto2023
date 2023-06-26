using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    Player1Controller player1;
    Player2Controller player2;
    int cont;
    int vidita;
    int vidita2;
    int balas;
    int cant;
    int gemas;
    int mividita;
    public int vidas;
    
    void Start()
    {
        player1 = FindObjectOfType<Player1Controller>();
        player2 = FindObjectOfType<Player2Controller>();
        cont = 0;
        vidita = 1;
        vidita2 = 1;
        balas = 50;
        cant = 0;
        vidas = 1;
        gemas = 0;
        mividita = 10;
        TextVista();
    }
    void Update()
    {
        //Debug.Log(Time.deltaTime);
    }

    public int Cont()
    {
        return cont;
    }
    public int Vidita()
    {
        return vidita;
    }
    public int Vidita2()
    {
        return vidita2;
    }
    public int Balas()
    {
        return balas;
    }
    public int Cantidad()
    {
        return cant;
    }
    public int Vidas()
    {
        return vidas;
    }
    public int Gemas()
    {
        return gemas;
    }
    public int Medusa()
    {
        return mividita;
    }
    public void SumaMonedas()
    {
        cont++;
    }
    public void RestarVidaMedusa(int menos)
    {
        mividita-=menos;
    }
    public void RestaVida()
    {
        vidita--;
    }
    public void RestaVida2()
    {
        vidita2--;
    }
    public void MenosBalas(int resta)
    {
        balas-=resta;
    }
    public void MasBalas(int suma)
    {
        balas += suma;
    }
    public void CantZombie()
    {
        cant++;
    }
    public void RestaVidaZombie(int menos)
    {
        vidas-=menos;
    }
    public void SumarGemas()
    {
        gemas++;
    }
    public void TextVista()
    {
    }
}
