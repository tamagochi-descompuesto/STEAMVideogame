using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    *Permite movilizar en el eje 'y' al personaje 
    *Sin necesidad de ser controlado
    *Autor: David Rodriguez, Erick Bustos
*/

public class MoverEnemigo : MonoBehaviour
{
    //VARIABLES
    public float maxVelocidadY = -5;  //Movimiento Horizontal
    private Rigidbody2D rb2D;  //Para fisica
    
    

    void Start()
    {
        //Inicializar variables
        rb2D = GetComponent<Rigidbody2D>();  
    }

   
    void Update()
    {
        if (transform.position.y <= -4.35)
        {
            //Desplaza al personaje en el eje -y
            maxVelocidadY = 5;

            
        }else if(transform.position.y >= 4.54)
        {
            //Desplaza al personaje en el eje y
            maxVelocidadY = -5;

        }
        
        rb2D.velocity = new Vector2(0, maxVelocidadY);

    }
}