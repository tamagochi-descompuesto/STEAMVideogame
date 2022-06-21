using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que el el robot seguidor sea destruido al entrar al nivel de nieve
*de tal forma que no pueda seguir al jugador
Autor: Erick Hernández Silva
*/
public class EnemigoDestructor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta en cuanto el personaje hace contacto con otro Collider, indicando que está en el piso
        if(other.gameObject.tag == "Enemigo")   //Si el Collider no es de una pieza entonces el personaje está en el piso
        {
            Destroy(other.gameObject);
        }
    }
}
