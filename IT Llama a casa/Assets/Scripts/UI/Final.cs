using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Código que le otorga sus funciones a la escena final
Autor: Israel Sánchez Miranda
*/

public class Final : MonoBehaviour
{
    //VARIABLES
    //Diálogo final
    private string[] dialogo = new string[]{"Gracias a ti, IT logró regresar a su hogar.\n\nDe parte del equipo ACC esperamos que te hayas divertido.\n\nGracias por jugar.\n\n:)"};
    private string text;   //Texto para reemplazar dentro de la escena
    public Text texto;    //Texto de la escena
    public AudioSource tecleo;  //Sonido de tecleo

    //MÉTODOS
    void Start()
    {
        //Función que se ejecuta en antes del primer frame, en este contexto inicia una corrutina para escribir el texto
        StartCoroutine(Dialogo());
    }

    public IEnumerator Dialogo()
    {
        //Función que se encarga de darle una animación al texto como si se estuviera escribiendo
        foreach(string palabra in dialogo)
        {
            //Se itera sobre las palabras del diálogo y después sobre sus letras
            foreach(char letra in palabra)
            {
                //Se concatena en un texto
                text += letra;
                //Se remplaza el texto de la escena
                texto.text = text;
                tecleo.Play();
                //Se espera por cierto tiempo
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void Regresar()
    {
        //Función asociada a un botón que regresa al jugador al menú principal
        StopAllCoroutines();
        SceneManager.LoadScene("MenuPrincipal");
    }
}
