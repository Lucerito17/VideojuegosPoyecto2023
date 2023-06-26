using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedusaController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Collider2D cl;
    bool choco = true;
    bool estado = true;
    bool personajedetectado = false;
    float velocity = 5;
    public GameObject portal;
    GameManager gameManager;
    const int ANIMATION_CORRER = 0;
    const int ANIMATION_ATTACK = 1;
    const int ANIMATION_HURT = 2;
    const int ANIMATION_MORIR = 3;
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
        if(gameManager.Medusa() <= 0)
        {
            Morir();
        }
        else
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            Vuelta();
        }
    }
    private void AtacarPersonaje()
    {
        ChangeAnimation(ANIMATION_ATTACK);
    }
    private void Vuelta()
    {
        if(choco==true)
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
        Debug.Log("SE DETIENE ENEMIGO");
        estado = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        portal.SetActive(true);
    }
    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "tope")
        {
            choco = false;
        }
        if(other.gameObject.name == "tope2")
        {
            choco = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "fire1" || other.gameObject.tag == "fire2"||other.gameObject.tag=="golpe"){
            gameManager.RestarVidaMedusa(1);
            if(gameManager.Medusa()<=0){
                ChangeAnimation(ANIMATION_MORIR);
                cl.enabled = false;
            }
            Destroy(other.gameObject);
        }
    } 
}
