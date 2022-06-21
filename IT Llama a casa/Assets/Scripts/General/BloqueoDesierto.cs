using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueoDesierto : MonoBehaviour
{
    public GameObject muro;
    public GameObject collider;
    public GameObject textoInteractuar;

    //Cuando el jugador entra al collider
    private void OnTriggerEnter2D(Collider2D other) {
        textoInteractuar.SetActive(true);
    }
    ///Cuando el jugador sale dle collider
    private void OnTriggerExit2D(Collider2D other)
    {
        textoInteractuar.SetActive(false);
    }
    //Busca si el jugador ya tiene la pieza de IBT y activa o desactiva el muro
    private void Update()
    {
        if(CargarJugador.piezaIBT){
            muro.SetActive(false);
            collider.SetActive(false);
        }
        else{
            collider.SetActive(true);
            muro.SetActive(true);
        }
    }
}
