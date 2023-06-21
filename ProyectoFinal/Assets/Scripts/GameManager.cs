using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    //public TMP_Text MonedasTxt ;
    //public TMP_Text VidaTxt ;
    //public TMP_Text BalasTxt;
    //public TMP_Text ZombieTxt;
    //public TMP_Text LlaveTxt;
    Player1Controller player1;
    int cont;
    int vidita;
    int vidita2;
    int balas;
    int cant;
    int llave;
    public int vidas;
    void Start()
    {
        player1 = FindObjectOfType<Player1Controller>();
        cont = 0;
        vidita = 1;
        vidita2 = 1;
        balas = 50;
        cant = 0;
        vidas = 1;
        llave = 0;
        //LoadGame();
        TextVista();
    }

    /*public void SaveGame()
    {
        if(player1.cambio)
        {
            var filePath = Application.persistentDataPath + "/guardar2.dat";
            FileStream file;

            if(File.Exists(filePath))
                file = File.OpenWrite(filePath);
            else
                file = File.Create(filePath);

            GameData data = new GameData();
            data.Vida = vidita;
            data.ZombiesCantidad = cant;
            //data.Player = tempx;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            var filePath = Application.persistentDataPath + "/guardar.dat";
            FileStream file;

            if(File.Exists(filePath))
                file = File.OpenWrite(filePath);
            else
                file = File.Create(filePath);

            GameData data = new GameData();
            data.Vida = vidita;
            data.ZombiesCantidad = cant;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        
    }*/

    /*public void SaveGame2()
    {
        var filePath = Application.persistentDataPath + "/guardar2.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Vida = 3;
        data.ZombiesCantidad = 0;
        //data.Player = tempx;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }*/

    /*public void LoadGame()
    {
        if(!player1.cambio)
        {
        var filePath = Application.persistentDataPath + "/guardar2.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenRead(filePath);
        else{
            Debug.LogError("No se encontro archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        //usar datos guardados
        cant = data.ZombiesCantidad;
        vidita = data.Vida;
        }
        else
        {
            var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenRead(filePath);
        else{
            Debug.LogError("No se encontro archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        //usar datos guardados
        cant = data.ZombiesCantidad;
        }
    }*/

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
    public int Llave()
    {
        return llave;
    }

    public void SumaMonedas()
    {
        cont++;
        TextVista();
    }

    public void RestaVida()
    {
        vidita--;
        TextVista();
    }
    public void RestaVida2()
    {
        vidita2--;
        TextVista();
    }
    public void MenosBalas(int resta)
    {
        balas-=resta;
        TextVista();
    }
    public void MasBalas(int suma)
    {
        balas += suma;
        TextVista();
    }
    public void CantZombie(int canti)
    {
        cant++;
        TextVista();
    }
    public void RestaVidaZombie(int menos)
    {
        vidas-=menos;
    }
    public void SumaLlave()
    {
        llave++;
        TextVista();
    }
    public void TextVista()
    {
        //MonedasTxt.text = "Monedas : " + cont;
        //VidaTxt.text ="Vida : " + vidita;
        //BalasTxt.text ="Balas : " + balas;
        //ZombieTxt.text="Puntos : " + cant;
        //LlaveTxt.text="Llave: "+ llave;
    }
}
