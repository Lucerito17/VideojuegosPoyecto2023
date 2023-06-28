using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // TEXTOS
    public Text txtTutorial;
    public TextMeshProUGUI txtFinal;

    // TXT ATRIBUTOS
    public float tiempoEnPantalla = 3f;
    public float tiempoEnEscena = 0f;
    public float tiempoDesvanecimiento = 1f;

    string[] nombresEscenas = { "SceneNivel1", "SceneNivel2", "SceneNivel3" };

    int indiceEscenaActual = 0;

    string[] textos = new string[]{
        "Movimiento del personaje Rocío: ",
        "Flechas direccionales",
        "Vidas: 1",
        "Ataque: N° 1, N°2",
        "Puntos de mana para disparos: 50",
        "Movimiento del personaje Nicole: ",
        "A y D para la dirección. W para saltar",
        "Ataque: \nGolpe: E\nDisparos: R",
        "Vidas: 1",
        "Puntos de mana para disparos: 50"
    };

    // VARIABLES DINÁMICAS
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
    private string tiempoTranscurridoPath = "Assets/TiempoTranscurrido.txt"; // Ruta del archivo de texto

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
        IgnorarJugadores();
        CargarTiempoTranscurrido(); // Cargar el tiempo al iniciar la escena
        StartCoroutine(MostrarTextoRoutine(textos));
    }

    void OnDestroy()
    {
        GuardarTiempoTranscurrido();
    }

    void Update()
    {
        tiempoEnEscena += Time.deltaTime;
        //Debug.Log("Tiempo transcurrido: " + tiempoEnEscena);

        // Actualizar el campo de texto txtFinal en cada frame
        ActualizarTxtFinal();
    }

    void GuardarTiempoTranscurrido()
    {
        // Crea o abre el archivo de texto
        StreamWriter writer = new StreamWriter(tiempoTranscurridoPath, true);

        // Escribe el tiempo transcurrido en el archivo
        writer.WriteLine(tiempoEnEscena.ToString());

        // Cierra el archivo
        writer.Close();
    }
    
    public void IgnorarJugadores(){
      Physics2D.IgnoreCollision(player2.GetComponent<BoxCollider2D>() ,player1.GetComponent<BoxCollider2D>(), true);
    }

     public void NoignorarJugadores(){
      Physics2D.IgnoreCollision(player2.GetComponent<BoxCollider2D>() ,player1.GetComponent<BoxCollider2D>(), false);
    }
    public void CargarTiempoTranscurrido()
    {
        // Verifica si el archivo existe
        if (File.Exists(tiempoTranscurridoPath))
        {
            // Lee el contenido del archivo
            string[] tiemposTranscurridosString = File.ReadAllLines(tiempoTranscurridoPath);

            // Verificar si hay suficientes tiempos guardados en el archivo
            if (tiemposTranscurridosString.Length >= nombresEscenas.Length)
            {
                float tiempoTotal = 0f;

                // Sumar los tiempos guardados de las escenas anteriores
                for (int i = 0; i < indiceEscenaActual; i++)
                {
                    if (float.TryParse(tiemposTranscurridosString[i], out float tiempoEscena))
                    {
                        tiempoTotal += tiempoEscena;
                    }
                    else
                    {
                        Debug.LogWarning("No se pudo convertir el tiempo transcurrido guardado en un valor flotante.");
                    }
                }

                // Asignar el tiempo total al tiempo en escena
                tiempoEnEscena = tiempoTotal;
                Debug.Log("Tiempo total cargado: " + tiempoEnEscena);
            }
            else
            {
                Debug.LogWarning("No hay suficientes tiempos transcurridos guardados en el archivo.");
            }
        }
        else
        {
            Debug.Log("No se encontró el archivo de tiempo transcurrido.");
        }
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
        mividita -= menos;
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
        balas -= resta;
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
        vidas -= menos;
    }
    public void SumarGemas()
    {
        gemas++;
    }

    IEnumerator MostrarTextoRoutine(string[] txt)
    {
        foreach (string texto in txt)
        {
            if(txtTutorial != null){
            txtTutorial.text = texto;
            txtTutorial.CrossFadeAlpha(1f, tiempoDesvanecimiento, false);
            }
            yield return new WaitForSeconds(tiempoEnPantalla);
            if(txtTutorial != null)
            txtTutorial.CrossFadeAlpha(0f, tiempoDesvanecimiento / 2f, false);

            yield return new WaitForSeconds(tiempoDesvanecimiento / 2f);
        }

        // Guardar el tiempo transcurrido al final de la secuencia de texto
        CambiarEscena();
    }

    void CambiarEscena()
    {
        // Guardar el tiempo transcurrido de la escena actual
        GuardarTiempoTranscurrido();

        // Cargar la siguiente escena
        indiceEscenaActual++;
        if (indiceEscenaActual < nombresEscenas.Length)
        {
            SceneManager.LoadScene(nombresEscenas[indiceEscenaActual]);
        }
        else
        {
            Debug.Log("Se han recorrido todas las escenas.");
        }
    }

    void ActualizarTxtFinal()
    {
        // Verificar si el archivo existe
        if (File.Exists(tiempoTranscurridoPath))
        {
            // Leer el contenido del archivo
            string[] tiemposTranscurridosString = File.ReadAllLines(tiempoTranscurridoPath);

            if (tiemposTranscurridosString.Length > 0)
            {
                // Obtener la última línea
                string ultimaLinea = tiemposTranscurridosString[tiemposTranscurridosString.Length - 1];

                // Actualizar el campo de texto txtFinal
                if(txtFinal != null)
                 txtFinal.text = "Tiempo Final: " + ultimaLinea;
            }
            else
            {
                Debug.LogWarning("El archivo de tiempo transcurrido está vacío.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de tiempo transcurrido.");
        }
    }
}