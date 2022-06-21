using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    *Permite movilizar diagonalmente al enemigo 
    *Sin necesidad de ser controlado
    *Autor: David Rodriguez, Erick Bustos
*/

public class MoverEnemigoDiagonal : MonoBehaviour
{
    //VARIABLES
    public float maxVelocidadY = -5;  //Movimiento Vertical
    public float maxVelocidadX = -5;  //Movimiento Horizontal
    public float xInicial;
    public float xFinal;
    


    
    private Rigidbody2D rb2D;  //Para fisica

    
    void Start()
    {
        //Inicializar variables
        rb2D = GetComponent<Rigidbody2D>(); 
    }

    
    void Update()
    {
        if (transform.position.x <= xFinal)
        {
            //Desplaza al personaje en el eje +x +y
            maxVelocidadY = 5;
            maxVelocidadX = 5;

            
        }
        else if(transform.position.x >= xInicial)
        {
            //Desplaza al personaje en el eje -x -y
            maxVelocidadY = -5;
            maxVelocidadX = -5;

        }
        
        rb2D.velocity = new Vector2(maxVelocidadX, maxVelocidadY);

    }
}