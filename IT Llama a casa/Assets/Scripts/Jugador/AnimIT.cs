using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Permite modificar los parámetros del Animator para cambiar las animaciones de IT, el personaje principal del juego
Autores: David Rodríguez Fragoso, Edna Jacqueline Zavala Ortega, 
Erick Alberto Bustos Cruz, Erick Hernández Silva, Israel Sánchez Miranda
*/

public class AnimIT : MonoBehaviour
{
    //VARIABLES
    private Rigidbody2D rb2d;             //Rigidbody del personaje, físicas
    private Animator anim;                //Animator del personaje, animaciones
    private SpriteRenderer sprRenderer;   //Sprite Renderer del personaje, orientación
    
    //MÉTODOS
    void Start()
    {
        //Función que se manda a llamar antes del primer frame, se inicializan las variables
        //Se obtiene cada uno de los componentes enlistados para enlazarlos con el script:
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();   
    }
    //Desaparece temporalmente al jugador cuando es atacado
    
    void Update()
    {
        //Función que se manda a llamar cada frame, obtiene la velocidad y el estado del personaje para actualizar parámetros en el Animator
        //Velocidad:
        float velocidad = Mathf.Abs(rb2d.velocity.x); //Se obtiene el valor de la velocidad del personaje
        anim.SetFloat("velocidad", velocidad);        //Se actualiza el parámetro velocidad del Animator

        //Orientiación del personaje:
        if(rb2d.velocity.x > 0)       //Si la velocidad del personaje es mayor a cero este mirará a la derecha
        {
            sprRenderer.flipX = false;
        }
        else if(rb2d.velocity.x < 0)  //De lo contrario mirará a la izquierda
        {
            sprRenderer.flipX = true;
        }

        //Salto:
        anim.SetBool("enpiso", PruebaPiso.estaEnPiso);
    }
}
