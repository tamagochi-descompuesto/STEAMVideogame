using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Controlador del menú de pausa
Autores: David Rodríguez Fragoso, Edna Jacqueline Zavala Ortega, 
Erick Alberto Bustos Cruz, Erick Hernández Silva, Israel Sánchez Miranda
*/

public class MenuPausa : MonoBehaviour
{
    //VARIABLES
    public bool estaPausado;         //Variable booleana que indica si el juego está en pausa o no
    public GameObject pantallaPausa; //Referencia al panel de pausa

    //MÉTODOS
    public void Pausa()
    {
        //Función que se ejecuta cuando el usuario quiere pausar el juego
        //Se cambia el estado de pasua a su valor contrario
        estaPausado = !estaPausado;
        //Prende o apaga la pantalla de pausa
        pantallaPausa.SetActive(estaPausado);
        //Se pausa o resume el juego dependiendo del estado del juego
        Time.timeScale = estaPausado ? 0 : 1;
    }

    void Update()
    {
        //Función que se ejecuta cada frame, verifica si el jugador ha presionado la tecla de pausa o no
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Si el jugador presiona la tecla de escape se ejecuta la función de pausar
            Pausa();
        }
    }

    public void RegresarMenu()
    {
        //Función que le otorgará la función de regresar al menú al botón al que se le otorgue la función
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
