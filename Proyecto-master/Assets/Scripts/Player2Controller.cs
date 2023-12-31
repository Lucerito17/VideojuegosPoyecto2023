using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player2Controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Collider2D cl;
    public GameObject balita;
    public GameObject ataque;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_ATTACK = 1;
    const int ANIMATION_SHOOT = 2;
    const int ANIMATION_CORRER = 3;
    const int ANIMATION_JUMP = 4;
    const int ANIMATION_MORIR = 5;
    bool estado = true;
    bool gema = false;
    int velocity = 6;
    float VelocityJump = 9;
    GameManager gameManager;
    Player1Controller player1;
    public bool Petrificado;
    public GameObject petrificado;
    public float tiempoPetrificado;
    public bool salvado;
    void Start()
    {
        Debug.Log("Iniciando juego");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cl = GetComponent<Collider2D>();
        player1 = FindObjectOfType<Player1Controller>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.Vidita2() > 0)
        {
            if (!Petrificado) {
                Correr();
                GirarAnimacion();
                Ataque();
                Disparar();
                
                Saltar();
            }
            CheckGround();
        }
        else
        {
            Morir();
            Debug.Log("se murio");
        }
    }
    private void Correr()
    {


            if (Input.GetKey(KeyCode.D)| Input.GetAxis("Player2") > 0)
            {
                rb.velocity = new Vector2(velocity, rb.velocity.y);
                ChangeAnimation(ANIMATION_CORRER);
            }
            else if (Input.GetKey(KeyCode.A)| Input.GetAxis("Player2") < 0)
            {
                rb.velocity = new Vector2(-velocity, rb.velocity.y);
                ChangeAnimation(ANIMATION_CORRER);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                ChangeAnimation(ANIMATION_QUIETO);
            }
        
    }
    private void Saltar()
    {
        animator.SetFloat("VelocityJump", rb.velocity.y);
        if (!cl.IsTouchingLayers(LayerMask.GetMask("Plataforma"))) { return; }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Player2Saltar"))
        {
            rb.velocity = new Vector2(rb.velocity.x, VelocityJump);
            //audioSource.PlayOneShot(jumpSound);
        }
    }
    private void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Player2Atacar1"))
        {
            ChangeAnimation(ANIMATION_ATTACK);
            var AtaquePosition=transform.position;
            if (sr.flipX == true)
            {
                AtaquePosition = transform.position + new Vector3(-1.5f, 0, 0);
            }
            else if (sr.flipX == false)
            {
                AtaquePosition = transform.position + new Vector3(1.5f, 0, 0);
            }
            var gb = Instantiate(ataque, AtaquePosition, Quaternion.identity) as GameObject;
            Destroy(gb, 0.5f);
        }
    }
    private void Disparar()
    {
        if (gameManager.Balas() > 0)
        {
            if (Input.GetKeyUp(KeyCode.R)|| Input.GetButtonDown("Player2Atacar2"))
            {
                Bala(3, 0, 0);
                gameManager.MenosBalas(1);
            }
        }
    }
    public void Bala(int posX, int posY, int posZ)
    {
        if (sr.flipX == true)
        {//disparar hacia la izquierda  
            var BalasPosition = transform.position + new Vector3(-posX, posY, posZ);//negativo
            var gb = Instantiate(balita, BalasPosition, Quaternion.identity) as GameObject;
            //llamar bala, posicion bala , direcion bala
            var controller = gb.GetComponent<Bala>();
            controller.SetLeftDirection();
        }
        if (sr.flipX == false)
        {//disparar hacia la derecha
            var BalasPosition = transform.position + new Vector3(posX, posY, posZ);//positivo
            var gb = Instantiate(balita, BalasPosition, Quaternion.identity) as GameObject;
            //llamar bala, posicion bala , direcion bala
            var controller = gb.GetComponent<Bala>();
            controller.SetRightDirection();
        }
    }
    private void Morir()
    {
        estado = false;
        gameManager.RestaVida2();
        ChangeAnimation(ANIMATION_MORIR);
    }

    private void CheckGround()
    {
        if (cl.IsTouchingLayers(LayerMask.GetMask("Plataforma")))
        {
            animator.SetBool("isGround", true);
        }
        else
        {
            animator.SetBool("isGround", false);
            //aire = true;
        }
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    private void GirarAnimacion()
    {
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Portal1")
        {
            if(player1.paso==true)
            {
                SceneManager.LoadScene(1);
            }
        }
        if(other.gameObject.tag=="Gmorada")
        {
            gema=true;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag=="Portal2"&&gema&&gameManager.Cantidad()==8){
            if(player1.paso==true)
                {
                    SceneManager.LoadScene(3);
                }
            }
        if(other.gameObject.tag=="Portal3"&&gameManager.Cantidad()==6)
        {
            if(player1.paso==true)
            {
                SceneManager.LoadScene(4);
            }
        }
        if(other.gameObject.tag=="Portal4"&&gameManager.Gemas()==18)
        {
            if(player1.paso==true)
            {
                SceneManager.LoadScene(5);
            }
        }
        if(other.gameObject.tag=="Portal5")
        {
            if(player1.paso==true)
            {
                SceneManager.LoadScene(7);
            }
        }
        if(other.gameObject.tag=="Gmorada")
        {
            gameManager.SumarGemas();
        }
        if(other.gameObject.tag=="Enemy")
        {
            Morir();
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.collider.gameObject.tag == "player1")
        {
            petrificado.SetActive(false);
            gameManager.IgnorarJugadores();
            salvado = true;
            Petrificado = false;
            rb.mass = 1;
        }
    }
    public IEnumerator Petrificar()
    {
        Petrificado = true;
        salvado = false;
        petrificado.SetActive(true);
        rb.mass = 150;
        Debug.Log("Petrificado");
        gameManager.NoignorarJugadores();
        ChangeAnimation(ANIMATION_QUIETO);
        yield return new WaitForSeconds(tiempoPetrificado);
        Debug.Log("despetrificado");
        petrificado.SetActive(false);
        rb.mass = 1;
        if(!salvado)
           gameManager.RestaVida2();
        Petrificado = false;
        
    }
    public void Despetrificar()
    {
        petrificado.SetActive(false);
        rb.mass = 1;
        gameManager.RestaVida2();
    }
}
