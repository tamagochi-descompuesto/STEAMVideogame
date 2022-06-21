using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Monitorea todos los comportamientos del HUD
Autores: David Rodríguez Fragoso, Edna Jacqueline Zavala Ortega, 
Erick Alberto Bustos Cruz, Erick Hernández Silva, Israel Sánchez Miranda
*/

public class HUD : MonoBehaviour
{
    //VARIABLES
    public Image Vida1;          //Corazones del personaje
    public Image Vida2;
    public Image Vida3;
    public Text Numpiezas;      //Texto que indica el número de piezas recolectadas
    public GameObject panelHelp; //Panel de ayuda con las instrucciones del juego
    public static HUD instance;  //Referencia a la clase HUD

    //MÉTODOS
    private void Awake()
    {
        //Se hace el enlace entre el script y el HUD
        instance = this;
    }

    public void ActualizarVidas()
    {
        //Actualiza los corazones del HUD basándose en las vidas que tiene el personaje
        int vidas = EstadoPJ.instance.vidas;
        if(vidas == 3)
        {
            Vida3.enabled = true;
            Vida2.enabled = true;
            Vida1.enabled = true;
        }
        else if(vidas == 2)
        {
            Vida3.enabled = false;
            Vida2.enabled = true;
            Vida1.enabled = true;
        }
        else if(vidas == 1)
        {
            Vida3.enabled = false;
            Vida2.enabled = false;
            Vida1.enabled = true;
        }
        else if(vidas == 0)
        {
            Vida1.enabled = false;
            PlayerPrefs.SetFloat("ultimaPosicionX",-8.67f);
            PlayerPrefs.SetFloat("ultimaPosicionY",1.13f);
            PlayerPrefs.SetInt("vidas",3);
            PlayerPrefs.SetInt("piezaIBT",0);
            PlayerPrefs.SetInt("piezaITC",0);
            PlayerPrefs.Save();
            PuntoGuardado.instance.PartidaPerdida();
            SceneManager.LoadScene("MenuPrincipal");//Nos vamos al menú principal al morir

        }
    }

    public void ActualizarPiezas()
    {
        //Función que actualiza el texto que indica cuantas piezas lleva recolectadas el personaje
        if(CargarJugador.piezaIBT == true)
        {
            EstadoPJ.instance.piezas += 1;
        }
        if(CargarJugador.piezaITC == true)
        {
            EstadoPJ.instance.piezas +=1 ;
        }
        Numpiezas.text = EstadoPJ.instance.piezas.ToString();
    }

    public void Help(bool activo)
    {
        //Función que despliega u oculta el panel de ayuda dependiendo de si está activo o no, va asignado a algún botón
        //Parámetros: activo, variable booleana que indica si el panel está activo o no
        panelHelp.SetActive(activo);
        Time.timeScale = activo ? 0 : 1;
    }
}
