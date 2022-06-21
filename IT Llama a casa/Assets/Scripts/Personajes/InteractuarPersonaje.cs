using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Script que se encarga de la interacción del jugador con los personajes no jugables (diálogos, hablar con ellox, etc.)
Autor: Israel Sánchez Miranda
*/

public class InteractuarPersonaje : MonoBehaviour
{
    //VARIABLES
    public GameObject textoInteractuar;
    public GameObject dialogo;
    public GameObject panelAyudar;
    public Text textoDialogo;
    public Text textoPanelAyudar;
    private int contadorDialogo = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta cuando el jugador entra al collider de interacción del personaje, activa el texto que indica que se puede interactuar
        if(other.CompareTag("Player"))
        {
            textoInteractuar.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Función que se ejecuta cuando el jugador sale del collider de interacción del personaje, desactiva el texto que inidica que se puede interactuar
       if(other.CompareTag("Player"))
       {
           textoInteractuar.SetActive(false);
       }
    }

    void Update()
    {
        //Función que se ejecuta cada frame, en este caso verifica que el jugador esté dentro del área de interacción y presione la tecla de interactuar
        if(textoInteractuar.activeSelf && Input.GetButtonDown("Fire3"))
        {
            //Cuando se presiona la tecla de interactuar el juego se detiene y comienza el diálogo del personaje
            Time.timeScale = 0;
            //Si ya se recolectó la pieza, se cambia el texto estándar
            if(CargarJugador.piezaIBT){
                textoDialogo.text = "¡Muchas gracias por tu ayuda! Nunca te olvidaré.";
            }
            dialogo.SetActive(true);
        }
    }

    public void SiguienteDialogo()
    {
        //Verifica si el minijuego ya se jugó
        if(CargarJugador.piezaIBT){
            textoDialogo.text = "¡Muchas gracias por tu ayuda! Nunca te olvidaré.";
            dialogo.SetActive(false);
            Time.timeScale = 1;
        }
        else{
            //Función que se asocia al botón de siguiente en la caja de diálogo, se encargará de pasar al siguiente diálogo o de cargar la escena del minijuego
            contadorDialogo++;
            //Dependiendo del contador del diálogo se desplegarán los diferentes diálogos para el personaje
            if(contadorDialogo == 2)
            {
                textoDialogo.text = "Estaba haciendo una investigación de campo en este desierto cuando sentí un piquete en mi cuello, unas horas después me empecé a sentir mal y terminé cayendo en este hoyo";
            }
            else if(contadorDialogo == 3)
            {
                textoDialogo.text = "Creo que me picó un mosquito infectado con malaria, necesito que me hagas un favor, ¿Podrías analizar una muestra de mi sangre y decirme cuál es mi estado? estoy muy débil para hacerlo yo misma";
            }
            else if(contadorDialogo == 4)
            {
                textoDialogo.text = "Te prometo que si me ayudas te daré algo a cambio ¿Qué dices?";
            }
            else if(contadorDialogo == 6)
            {
                contadorDialogo = 1;
                Time.timeScale = 1;
                SceneManager.LoadScene("MinijuegoIBT"); //Cuando el contadro sea 5 se dirigirá al minijuego de IBT para el caso de la Biotecnología
            }
            else if(contadorDialogo == 7)
            {
                contadorDialogo = 1;
                textoDialogo.text = "¿Huh? ¿Hay alguien ahí?\nEs un milagro, jamás creí que alguien me encontraría\nMe presento, soy Bia la biotecnóloga";
                Time.timeScale = 1;
                dialogo.SetActive(false);
            }
            else
            {
                contadorDialogo = 1;
                textoDialogo.text = "¿Huh? ¿Hay alguien ahí?\nEs un milagro, jamás creí que alguien me encontraría\nMe presento, soy Bia la biotecnóloga";
                textoPanelAyudar.text = "¿Quieres ayudar a Bia?";
                panelAyudar.SetActive(true);
            }  
        }
        
    }

    public void Ayudar()
    {
        //Función que va asociada al botón que se tenga que presionar para ayudar al personaje
        //Se desactiva el panel de ayuda y el diálogo del personaje cambia 
       if(dialogo.activeSelf)
       {
           panelAyudar.SetActive(false);
           contadorDialogo = 5;
           textoDialogo.text = "¡Muchas gracias!\nTe prometo que no te arrepentirás";
       }
    }

    public void NoAyudar()
    {
        //Función que va asociada al botón que se tenga que presionar para no ayudar al personaje
        //Se desactiva el panel de ayuda y el diálogo del personaje cambia 
        if(dialogo.activeSelf)
        {
            panelAyudar.SetActive(false);
            contadorDialogo = 6;
            textoDialogo.text = "Oh...\nComprendo, supongo que tendré que ingeniármelas yo misma\nQué cruel eres...";
        }
    }
}
