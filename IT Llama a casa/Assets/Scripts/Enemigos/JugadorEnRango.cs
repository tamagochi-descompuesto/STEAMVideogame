using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que el enemigo detecte al jugador en rango
Autor: Erick Hernández Silva
*/
public class JugadorEnRango : MonoBehaviour
{
    public static bool estaEnRango = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta en cuanto el personaje hace contacto con otro Collider, indicando que está en el piso
        if(other.gameObject.tag == "Player")   //Si el Collider no es de una pieza entonces el personaje está en el piso
        {
            estaEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Función que se ejecuta en cuanto el personaje sale de otro Collider, indicando que no está en el piso
        if(other.gameObject.tag == "Player")   //Si el Collider no es de una pieza entonces el personaje está en el piso
        {
            estaEnRango = false;
        }
        
    }
}
