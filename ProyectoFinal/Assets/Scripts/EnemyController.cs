using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float velocity = 3;
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
        if(gameManager.Vidita() > 0)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            Vuelta();
        }
        else
        Morir();
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
        //ChangeAnimation(ANIMATION_QUIETO);
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
        if(other.gameObject.name == "fire1" || other.gameObject.name == "fire2"){
            if(gameManager.Vidas()==0){
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }
    } 
}
