using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
*Script que le da sus funcionalidades a la escena del inicio
* Autor: Israel Sánchez
*/

public class Inicio : MonoBehaviour
{
    //VARIABLES
    //Primer diálogo
    private string[] dialogo = new string[]{"Bitácora del capitán.\nHan pasado días desde que se perdió la comunicación con el centro de mando. La energía y el oxígeno están por agotarse, si no sucede un milagro pronto, creo que esto será el fin . . ."};
    private string text;   //Texto para reemplazar dentro de la escena
    private int counter = 0;   //Contador que determina los eventos
    public Text texto;    //Texto de la escena
    public GameObject panelAlarma;    //Panel para simular alarma 
    public AudioSource tecleo;  //Sonido de tecleo
    public AudioSource alarma;  //Sonido de alarma
    public AudioSource explosion; //Explosión de la nave

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

    public IEnumerator Alarma()
    {
        //Corrutina para activar la alarma, su panel y el sonido
        //Se activa el sonido de la alarma
        alarma.Play();
        while(counter == 2)
        {
            //Se activa y desactiva el panel de la alarma
            panelAlarma.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            panelAlarma.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void PasarTexto()
    {
        //Función asociada a un botón que pasa y cambia el texto de la escena
        //Se le suma uno al contador
        counter++;
        if(counter == 1)
        {
            //Se actualiza el diálogo, se resetea el texto y se vuelve a escribir todo
            StopAllCoroutines();
            text = "";
            dialogo = new string[]{"Alerta del sistema: Peligro de colisión, se recomienda tomar acciones evasi..."};
            StartCoroutine(Dialogo());
        }
        else if(counter == 2)
        {  
            //Se manda a llamar la corrutina de la alarma
            StartCoroutine(Alarma());
        }
        else if(counter == 3)
        {
            //Se detiene la alarma
            alarma.Stop();
            //Se reproduce el sonido de la explosión
            explosion.Play();
            StopAllCoroutines();
            //Se actualiza el diálogo, se resetea el texto y se vuelve a escribir todo
            text = "";
            dialogo = new string[]{"Misión actual:\nrecolectar las 2 piezas faltantes para reparar la nave."};
            StartCoroutine(Dialogo());
        }
        else
        {
            //Se carga la siguiente escena
            SceneManager.LoadScene("MenuInicial");
        }
    }
}
