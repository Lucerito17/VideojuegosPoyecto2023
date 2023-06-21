using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    public float velocity = 15;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float realVelocity;
    float reakVelocityY;

    public void SetUpDirection(float velocity)
    {
        realVelocity = velocity;
        reakVelocityY = 1;
    }

    public void SetDownDirection(float velocity)
    {
        realVelocity = velocity;
        reakVelocityY = -1;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 3);//eliminacion del objeto
    }

    void Update()
    {
        rb.velocity = new Vector2(realVelocity, reakVelocityY);
    }

    public void OnTriggerEnter2D(Collider2D other)//para chocar y eliminar
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);//se topa con el objeto 
            Destroy(other.gameObject);//destruye al objeto topado
        }
    }
}