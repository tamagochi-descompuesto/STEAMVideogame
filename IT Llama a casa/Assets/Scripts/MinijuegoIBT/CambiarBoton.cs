using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Código que permite que los botones de la matriz del jugador cambien de color
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class CambiarBoton : MonoBehaviour
{
    //VARIABLES
    public Sprite noColor;               //Imágenes que reemplazaran el source image de los botones
    public Sprite rojo;
    public Sprite amarillo;
    public Sprite verde; 
    private static bool esRojo;          //Variables booleanas que permiten saber el color escogido de la "paleta"
    private static bool esAmarillo;
    private static bool esVerde;


    //MÉTODOS
    public void CambiarColor(Button button)
    {
        //Función que cambia el color del botón basándose en el color escocigido de la "paleta"
        //Efecto de click.
        Transiciones.instance.sonidoClick.Play();
        //Parámetros: button - es el botón al que la función está asociada
        if(esRojo && !esAmarillo && !esVerde)     //Dependiendo del color que se haya escogido de la "paleta" el source image del botón cambiará
        {
            button.image.sprite = rojo;
        }
        else if(!esRojo && esAmarillo && !esVerde)
        {
            button.image.sprite = amarillo;
        }
        else if(!esRojo && !esAmarillo && esVerde)
        {
            button.image.sprite = verde;
        }
        else
        {
            //Cualquier otro caso hace que el botón no cambie de color
            button.image.sprite = noColor;
        }
    }

    public void queColor(string color)
    {
        Transiciones.instance.sonidoClick.Play();
        //Función otorgada a los tres botones de la paleta que, basándose en el string de entrada les perimte asignar colores a los demás botones
        //Parámetros: color - es un string que se envía desde Unity, tiene que ver con el color al que está asociado el botón
        if(color == "rojo") //Si el string color recibido es rojo
        {
            //Las variables booleanas se modificarán en función del string
            esRojo = true;
            esAmarillo = false;
            esVerde = false;
        }
        else if(color == "amarillo") //Se aplica la misma lógica para cada botón
        {
            esRojo = false;
            esAmarillo = true;
            esVerde = false;
        }
        else if(color == "verde")
        {
            esRojo= false;
            esAmarillo = false;
            esVerde = true;
        }
        //Estas variables permiten que si se escoge el color verde en la paleta todos los botones que se presionen cambien a verde y así sucesivamente
    }
}
