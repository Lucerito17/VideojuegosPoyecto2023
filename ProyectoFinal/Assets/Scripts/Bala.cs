using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocity = 4;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float realVelocity;
    GameManager gameManager;
    public GameObject balitas;

    public void SetRightDirection()
    {
        realVelocity = velocity;
    }

    public void SetLeftDirection()
    {
        realVelocity = -velocity;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        Destroy(this.gameObject, 3);//eliminacion del objeto
    }

    void Update()
    {
        rb.velocity = new Vector2(realVelocity, 0);
        if (gameManager.Balas() > 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                gameManager.MenosBalas(2);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)//para chocar y eliminar
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Destroy(this.gameObject);//se topa con el objeto 
            //Destroy(other.gameObject);//destruye al objeto topado
            gameManager.CantZombie();
            gameManager.RestaVidaZombie(1);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
