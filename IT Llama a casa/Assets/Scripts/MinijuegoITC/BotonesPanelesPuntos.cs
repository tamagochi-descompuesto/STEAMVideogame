using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Da funcionalidad a los botónes de los paneles nivel completado y minijuego completado,
 * oculta el respectivo panel al presionar el botón
 * Autor: Erick Bustos
 */
public class BotonesPanelesPuntos : MonoBehaviour
{
    // Referencia al panel de Nivel completado
    public GameObject panelNivelCompletado;
    // Referencia al panel de Minijuego completado
    public GameObject panelMinijuegoCompletado;

    public void SiguienteNivel()
    {
        panelNivelCompletado.SetActive(false);
    }

    public void EscenaFinal()
    {
        // Agregar que el jugador reaparezca en la misma posición
        SceneManager.LoadScene("Mapa");
    }
}
