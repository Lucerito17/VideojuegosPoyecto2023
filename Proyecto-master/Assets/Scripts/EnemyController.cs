using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float velocity = 2;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    bool estado = true;
    bool choco = true;
    GameManager gameManager;
    const int ANIMATION_QUIETO = 1;
    const int ANIMATION_CORRER = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if(gameManager.Vidita() <= 0|| gameManager.Vidita2() <= 0)
        {
            Morir();
        }
        else
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            Vuelta();
        }
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

        if(other.gameObject.name == "golpe")
        {
            Debug.Log("Chocando golpe");
            gameManager.RestaVidaZombie(1);
            if(gameManager.Vidas()==0){
                Destroy(this.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == "fire1" || other.gameObject.name == "fire2"){
            gameManager.RestaVidaZombie(1);
            if(gameManager.Vidas()==0){
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }

        
    }
}
