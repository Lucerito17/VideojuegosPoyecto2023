using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    float cont = 0;
    float contadorenemy = 0;
    void Start()
    {
        
    }

    void Update()
    {
        cont += Time.deltaTime;
        if(cont > 5)
        {
            if(contadorenemy>=0 && contadorenemy<4)
            {
                GenerarEnemy();
                cont = 0;
            }
            
        }
    }

    private void GenerarEnemy()
    {
        var EnemyPosition = transform.position + new Vector3(-2,0,0);
        var gb = Instantiate(enemy, EnemyPosition, Quaternion.identity) as GameObject;
        var controller = gb.GetComponent<EnemyController>();

        contadorenemy++;
    }
}
