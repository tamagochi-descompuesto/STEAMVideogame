using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Da funcionalidad a los botónes de las instrucciones del minijuego
 * Autor: Erick Bustos
 */
public class BotonesInstrucciones : MonoBehaviour
{
    // Referencia a los paneles actual, anterior y siguiente
    public GameObject panelactual;
    public GameObject panelsiguiente;
    public GameObject panelanterior;

    // Cambia al panel siguiente
    public void CambiaPanel()
    {
        panelactual.SetActive(false);
        panelsiguiente.SetActive(true);
    }

    // Cambia al panel anterior
    public void RegresaPanel()
    {
        panelactual.SetActive(false);
        panelanterior.SetActive(true);
    }
    
    // Oculta el último panel de las instrucciones para desplegar el juego
    public void IniciaJuego()
    {
        panelactual.SetActive(false);
    }
}
