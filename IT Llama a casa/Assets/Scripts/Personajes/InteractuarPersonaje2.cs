using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
* Script que se encarga de la interacción del jugador con los personajes no jugables (diálogos, hablar con ellox, etc.)
* Autor: Israel Sánchez Miranda
*/

public class InteractuarPersonaje2 : MonoBehaviour
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
            if(CargarJugador.piezaITC)
            {
                textoDialogo.text = "¡Muchas gracias por tu ayuda! Te deseo un buen viaje a casa.";
            }
            dialogo.SetActive(true);
        }
    }

    public void SiguienteDialogo()
    {
        if(CargarJugador.piezaITC){
            textoDialogo.text = "¡Muchas gracias por tu ayuda! Te deseo un buen viaje a casa.";
            dialogo.SetActive(false);
            Time.timeScale = 1;
        }
        else{
            //Función que se asocia al botón de siguiente en la caja de diálogo, se encargará de pasar al siguiente diálogo o de cargar la escena del minijuego
        contadorDialogo++;
        //Dependiendo del contador del diálogo se desplegarán los diferentes diálogos para el personaje
        if(contadorDialogo == 2)
        {
            textoDialogo.text = "Estaba buscando un programa en este laboratorio viejo, era de un viejo amigo, pero tal parece que los robots lo destrozaron todo";
        }
        else if(contadorDialogo == 3)
        {
            textoDialogo.text = "A pesar de eso logré encontrar lo que buscaba, pero activé el sistema de seguridad y me quedé atrapado, de no ser por ti me habría quedado aquí por siempre";
        }
        else if(contadorDialogo == 4)
        {
            textoDialogo.text = "¿Qué dices? ¿Qué necesitas ayuda para arreglar un sistema de piloto automático? No digas más, pero no puedo hacerlo solo ¿Me echarías una mano?";
        }
        else if(contadorDialogo == 6)
        {
            contadorDialogo = 1;
            Time.timeScale = 1;
            SceneManager.LoadScene("MinijuegoITC"); //Cuando el contadro sea 5 se dirigirá al minijuego de IBT para el caso de la Biotecnología
        }
        else if(contadorDialogo == 7)
        {
            contadorDialogo = 1;
            textoDialogo.text = "¿Eh? ¿Cómo llegaste aquí?\nYa veo, así que conoces a bia, en ese caso me presento, soy Grop, uno de los más grandes programadores que conocerás en tu vida";
            Time.timeScale = 1;
            dialogo.SetActive(false);
        }
        else
        {
            contadorDialogo = 1;
            textoDialogo.text = "¿Eh? ¿Cómo llegaste aquí?\nYa veo, así que conoces a bia, en ese caso me presento, soy Grop, uno de los más grandes programadores que conocerás en tu vida";
            textoPanelAyudar.text = "¿Quieres ayudar a Grop?";
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
            textoDialogo.text = "¡Manos a la obra!";
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
            textoDialogo.text = "Vaya...\nNo esperaba eso, no estaba programado\nEntonces, creo que ya puedes irte...";
        }
    }
}
