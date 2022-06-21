using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script que cambia la música de una escena
Autor: David Rodríguez Fragoso
*/

public class CambiarMusica : MonoBehaviour
{
    //VARIABLES
    public AudioClip audioNuevo;         //Audio nuevo a reproducir
    public AudioSource audioFondo;       //Audio Source en el que se reproducirá el nuevo audio
    void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta cada que un collider entra al collider del objeto
        //En este contexto cambia la música de fondo cada que se entra al collider
        if(other.CompareTag("Player"))
        {
            //Si el jugador entra al collider se cambia el audio de fondo y se reproduce el nuevo
           audioFondo.clip = audioNuevo;
           audioFondo.Play();
        }
    }
}
