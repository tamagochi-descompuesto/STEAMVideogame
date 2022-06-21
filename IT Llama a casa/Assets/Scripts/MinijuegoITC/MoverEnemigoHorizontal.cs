using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
/*
    *Permite movilizar horizontalmente al enemigo 
    *Sin necesidad de ser controlado
    *Autor: David Rodriguez, Erick Bustos
*/
public class MoverEnemigoHorizontal : MonoBehaviour
{
    //VARIABLES
    public float maxVelocidadX = 5;  //Movimiento Horizontal
    private Rigidbody2D rigidBody;  //Para fisica
    private SpriteRenderer spriterenderer;
    
    
    void Start()
    {
        //Inicializar variables
        rigidBody = GetComponent<Rigidbody2D>();  
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (transform.position.x <= -6.54)
        {
            //Desplaza al personaje en el eje x
            maxVelocidadX = 5;
            spriterenderer.flipX=false;

            
        }
        else if(transform.position.x >= 1.59)
        {
            //Desplaza al personaje en el eje -x
            maxVelocidadX = -5;
            spriterenderer.flipX=true;

        }
        
        rigidBody.velocity = new Vector2(maxVelocidadX, 0);

    }
}