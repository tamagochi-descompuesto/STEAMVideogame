using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Permite que los botónes suenen al ser presionados
 * Autor: Erick Bustos
 */
public class SonidoBotones : MonoBehaviour
{ 
    // Referencia al Audiosource
    public AudioSource boton;
    
    // Reproducir el sonido del botón
    public void SonidoBoton()
    {
        boton.Play();
    }
 
}
