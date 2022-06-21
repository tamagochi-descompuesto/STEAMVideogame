using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script que le permite a los botones del tilemap desactivar los rayos del último nivel
Autor: Israel Sánchez Miranda
*/

public class BotonesInteractuar : MonoBehaviour
{
    //VARIABLES
    public GameObject textoInteractuar;
    public GameObject rayo;
    private bool estaPrendido = true;

    //MÉTODOS
    void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejcuta cuando el jugador entra en el collider de los botones
        if(other.CompareTag("Player"))
        {
            //Si el jugador entra al collider se le indicará que puede interactuar con el objeto
            textoInteractuar.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Fución que se ejecuta cuando el jugador sale del collider de los botones
        if(other.CompareTag("Player"))
        {
            //Si el jugador sale del collider se desactivará el texto de interactuar
            textoInteractuar.SetActive(false);
        }
    }

    void Update()
    {
        //Función que se ejecuta durante cada frame
        if(textoInteractuar.activeSelf && Input.GetButtonDown("Fire3")) //Si el jugador está dentro del collider y está presionando el botón de interactuar
        {
            estaPrendido = !estaPrendido;
            rayo.SetActive(estaPrendido);
        }
    }
}
