using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Permite controlar una animación del panel de ayuda para clarificar
* la mecánica del juego
* Autor: Jacqueline Zavala.
*/

public class PanelAyuda : MonoBehaviour
{
    // VARIABLES
    public GameObject botonMatriz;
    public GameObject botonVerde;

    void Awake()
    {
        // Se planea que el método se llame en cuanto se active el panel
        AnimacionPanelAyuda();
    }

    public IEnumerator AnimacionPanelAyuda()
    {
        // Método que permite completar una animación de un menú de ayuda
        yield return new WaitForSeconds(0.01f); 
        botonMatriz.SetActive(false);
        botonVerde.SetActive(true);
    }
    
}
