using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
* Permite manejar las transiciones de los paneles iniciales del juego.
* Se incluyen también sonidos para los botones que llaman a los métodos.
* Autor: Jacqueline Zavala.
*/


public class Transiciones : MonoBehaviour
{
    // VARIABLES
    public GameObject panelContexto1; // Referencia a los paneles iniciales.
    public GameObject panelContexto2;
    public GameObject panelContexto3;
    public GameObject panelContexto4;
    public GameObject panelAyuda;       // Referencia al panel de ayuda.
    public GameObject panelAyudaCopiar; // Referencia al panel de ayuda para saber cómo copiarlo.
    public AudioSource sonidoClick;     // Referencia al sonido de click
    private int panelActual;            // Variable para navegar en el arreglo de paneles.
    private GameObject[] paneles;       // Arreglo de paneles.
    public static Transiciones instance; // Variable de tipo Transiciones.

    void Start()
    {
        // Crea el arreglo de paneles
        paneles = new GameObject[]{panelContexto1, panelContexto2, panelContexto3, panelContexto4};
        panelActual = 0;
        // Cambia el timescale a 0 dado que se iniciarán los paneles y el tiempo no debe comenzar a correr.
        Time.timeScale = 0;   
    }
    
    void Awake()
    {
        // Crear una instancia de la clase.
        instance = this;
    }

    public void Siguiente()
    {
        // Función que permite navegar "hacia adelante" entre los paneles iniciales.
        // Se pone el efecto de click.
        sonidoClick.Play();
        if(panelActual + 1 == 4)
        {
            // Si es el último panel, desactivarlo y cambiar el valor del timescale a 1
            // para que el tiempo comience a correr porque el juego va a comenzar
            paneles[panelActual].SetActive(false);
            Time.timeScale = 1;
            return;
        }

        // Desactivamos el panel actual.
        paneles[panelActual].SetActive(false);
        // Si no es el último, simplemente le sumamos uno y activamos el siguiente.
        panelActual += 1;
        paneles[panelActual].SetActive(true);
    }

    public void Volver()
    {
        // Función que permite navegar "de regreso" entre los paneles del contexto
        // así como regresar al mapa principal.
        // Efecto de click
        sonidoClick.Play();
        if(panelActual == 0)
        {
            SceneManager.LoadScene("Mapa");
            Time.timeScale = 1;    
        }
        else
        {
            paneles[panelActual].SetActive(false);
            panelActual -= 1;
            paneles[panelActual].SetActive(true);
        }

    }

    public void AyudaGeneral()
    {
        // Función que despliega el panel de ayuda general
        sonidoClick.Play();
        panelAyuda.SetActive(true);
        Time.timeScale = 0;
    }

    public void AyudaCopiar()
    {
        // Función que despliega el panel de ayuda especialmente para la acción de copiar la matriz
        panelAyudaCopiar.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinuarJuego()
    {
        // Función que oculta los paneles de ayuda dependiendo del que se encuentre activ
        sonidoClick.Play();
        // Si se encuentra activo el panel de ayuda general, se oculta
        if (panelAyuda.activeSelf)
        {
            panelAyuda.SetActive(false);
        }
        else
        // de lo contrario, oculta el otro panel de ayuda específico
        // esto dado que ambos no pueden estar activos al mismo tiempo.
        {
            panelAyudaCopiar.SetActive(false);
        }
        Time.timeScale = 1;
    }
}
