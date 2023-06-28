using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedusaController : MonoBehaviour
{
    [SerializeField] float distanciaJugador1;
    [SerializeField] float distanciaJugador2;
    [SerializeField] float anguloJugador1;
    [SerializeField] float anguloJugador2;
    [SerializeField] Player1Controller player1;
    [SerializeField] Player2Controller player2;
    [SerializeField] GameObject Emoji;

    public float velocidadBala;

    public bool atacando;
    public bool comienzeaatacar;
    public bool petrificando;
    public float distanciaMaxima; 
    public float distanciaMaximaAtacar;
    public float comienzeaatacarTiempo;
    public float tiempoentreataque;
    [SerializeField]bool isAttack = true;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Collider2D cl;
    bool choco = true;
    bool estado = true;
    float velocity = 5;
    public GameObject portal;
    public GameObject ataque;
    GameManager gameManager;

    const int ANIMATION_CORRER = 0;
    const int ANIMATION_ATTACK = 1;
    const int ANIMATION_HURT = 2;
    const int ANIMATION_MORIR = 3;

    // Tiempo en segundos para reiniciar la animación después de recibir daño
    public float tiempoReinicioAnimacion = 0.25f;
    private bool reiniciandoAnimacion = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cl = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!comienzeaatacar)
        {
            StartCoroutine(comienzaaatacar());
            return;
        }

        if (gameManager.Medusa() <= 0)
        {
            Morir();
        }
        else
        {
            distanciaJugador1 = Vector2.Distance(transform.position, player1.transform.position);
            distanciaJugador2 = Vector2.Distance(transform.position, player2.transform.position);
            anguloJugador1 = angulo(player1.transform);
            anguloJugador2 = angulo(player2.transform);

            if (!petrificando)
            {
                rb.velocity = new Vector2(-velocity, rb.velocity.y);
                Vuelta();
            }

            if (distanciaJugador1 < distanciaJugador2 && distanciaJugador1 < distanciaMaxima && !player1.Petrificado && gameManager.Vidita() > 0 && anguloJugador1 > 130 && anguloJugador1 < 180 && !petrificando && isAttack)
            {
                StartCoroutine(Petrificar(player1.transform));
                StartCoroutine(CoultDown());
            }
            else if (distanciaJugador2 < distanciaMaxima && !player2.Petrificado && gameManager.Vidita2() > 0 && anguloJugador2 > 130 && anguloJugador2 < 180 && !petrificando && isAttack)
            {
                StartCoroutine(Petrificar(player2.transform));
                StartCoroutine(CoultDown());
            }
            if (player1.Petrificado)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player1.GetComponent<BoxCollider2D>(), true);
            }
            else
            {
               Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player1.GetComponent<BoxCollider2D>(), false);
            }
            if (player2.Petrificado)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player2.GetComponent<BoxCollider2D>(), true);
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player2.GetComponent<BoxCollider2D>(), false);
            }
          
        }
    }
    IEnumerator CoultDown(){
        isAttack =false;
        yield return new WaitForSeconds(tiempoentreataque);
        isAttack = true;
    }
    IEnumerator comienzaaatacar()
    {
        comienzeaatacar = false;
        yield return new WaitForSeconds(comienzeaatacarTiempo);
        comienzeaatacar = true;
    }

    private IEnumerator Petrificar(Transform jugador)
    {
        ChangeAnimation(ANIMATION_ATTACK);
        petrificando = true;
        Emoji.SetActive(true);
        var AtaquePosition = transform.position;

        if (sr.flipX == true)
        {
            AtaquePosition = transform.position + new Vector3(-1.5f, 0, 0);
        }
        else if (sr.flipX == false)
        {
            AtaquePosition = transform.position + new Vector3(1.5f, 0, 0);
        }

        DispararRayo(jugador);
        yield return new WaitForSeconds(3);
        petrificando = false;
        Emoji.SetActive(false);
        ChangeAnimation(ANIMATION_CORRER);
    }

    void DispararRayo(Transform jugador)
    {
        GameObject bala = Instantiate(ataque, transform.position, Quaternion.identity);
        Vector3 direccion = (jugador.position - transform.position).normalized;
        bala.GetComponent<Rigidbody2D>().velocity = direccion * velocidadBala;
    }

    private void Vuelta()
    {
        if (choco == true)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = false;
        }
        else
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = true;
        }
    }

    private void Morir()
    {
        estado = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        portal.SetActive(true);
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);

        if (animation != ANIMATION_HURT)
        {
            // Si la animación no es ANIMATION_HURT, se cancela cualquier reinicio de animación pendiente
            CancelInvoke("ReiniciarAnimacion");
            reiniciandoAnimacion = false;
        }
    }

    private void ReiniciarAnimacion()
    {
        reiniciandoAnimacion = false;
        if (estado)
        {
            ChangeAnimation(ANIMATION_CORRER);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "fire1" || other.gameObject.tag == "fire2" || other.gameObject.tag == "golpe")
        {
            gameManager.RestarVidaMedusa(1);

            if (gameManager.Medusa() > 0)
            {
                ChangeAnimation(ANIMATION_HURT);
            }

            if (gameManager.Medusa() <= 0)
            {
                ChangeAnimation(ANIMATION_MORIR);
                cl.enabled = false;
            }

            Destroy(other.gameObject);

            if (!reiniciandoAnimacion)
            {
                // Inicia el reinicio de animación después del tiempo especificado
                Invoke("ReiniciarAnimacion", tiempoReinicioAnimacion);
                reiniciandoAnimacion = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "tope")
        {
            choco = false;
        }
        if (other.gameObject.name == "tope2")
        {
            choco = true;
        }
    }

    private float angulo(Transform jugador)
    {
        Vector3 direccion = jugador.position - transform.position;

        if (sr.flipX)
        {
            return Vector3.Angle(direccion, transform.right * -1);
        }
        else
        {
            return Vector3.Angle(direccion, transform.right);
        }
    }
}