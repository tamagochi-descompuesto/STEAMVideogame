using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Prueba si el collider está dentro o fuera de una plataforma para poder cambiar su animación de salto
Autores: David Rodríguez Fragoso, Edna Jacqueline Zavala Ortega,
Erick Alberto Bustos Cruz, Erick Hernández Silva, Israel Sánchez Miranda
*/

public class PruebaPiso : MonoBehaviour
{
    //VARIABLES
    public static bool estaEnPiso = false;    //Variable booleana que indica si el personaje está o no en el piso

    //MÉTODOS
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta en cuanto el personaje hace contacto con otro Collider, indicando que está en el piso
        if(other.gameObject.tag == "Mapa")   //Si el Collider no es de una pieza entonces el personaje está en el piso
        {
            estaEnPiso = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Función que se ejecuta en cuanto el personaje sale de otro Collider, indicando que no está en el piso
        if(other.gameObject.tag == "Mapa")   //Si el Collider no es de una pieza entonces el personaje está en el piso
        {
            estaEnPiso = false;
        }
        
    }
}
