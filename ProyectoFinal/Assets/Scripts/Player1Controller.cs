using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
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
    public bool paso = false;
    int velocity = 6;
    float VelocityJump = 9;
    GameManager gameManager;
    void Start()
    {
        Debug.Log("Iniciando juego");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cl = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.Vidita() > 0)
        {
            Correr();
            GirarAnimacion();
            Ataque();
            Saltar();
            Disparar();
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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, VelocityJump);
            //audioSource.PlayOneShot(jumpSound);
        }
    }
    private void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
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
            if (Input.GetKeyUp(KeyCode.Keypad2))
            {
                Bala(3, 0, 0);
                gameManager.MenosBalas(1);
                //audioSource.PlayOneShot(bulletSound);
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
        gameManager.RestaVida();
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
        if(other.gameObject.tag=="Portal")
        {
            paso = true;
        }
        if(other.gameObject.tag=="Groja")
        {
            gema=true;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag=="Portal11"&& gema&&gameManager.Cantidad()==8)
        {
            paso = true;
        }
        if(other.gameObject.tag=="Portal22"&&gameManager.Cantidad()==6)
        {
            paso = true;
        }
        if(other.gameObject.tag=="Portal33"&&gameManager.Gemas()==18)
        {
            paso = true;
        }
        if(other.gameObject.tag=="Portal44")
        {
            paso = true;
        }
        if(other.gameObject.tag=="Groja")
        {
            gameManager.SumarGemas();
        }
        if(other.gameObject.tag=="Enemy")
        {
            Morir();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        
    }
}
