using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Script que se encarga de los elementos relacionados a la puntuación del minijuego y sus funciones
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class Puntaje : MonoBehaviour
{
    //VARIABLES
    public Text puntos;          //Texto de la interfaz que le muestra al jugador cuantos puntos tiene
    public Text puntosFinal;    //Texto de la interfaz que le muestra al jugador su puntaje final
    public static float puntosTotal;  //Variable que almacena los puntos del jugador
    private Tiempo tiempo;     //Instancia de la clase Tiempo

    //MÉTODOS
    void Start()
    {
        //Función que se ejecuta antes del primer frame
        tiempo = FindObjectOfType<Tiempo>();  //Se enlaza la instancia tiempo con un objeto de tipo Tiempo
    }

    public void AsignarPuntosPrecision(int casillas)
    {
        //Función que se encarga de asignar la puntuación correcta al jugador basándose en el número de casillas que tuvo bien
        //Parámetros: casillas - entero que representa el número de casillas que tuvo bien el jugador
        if(tiempo.estaReintentando)
        {
            //Si el jugador está reintentando se le penalizará y recibirá la mitad de los puntos reales
            //Número limitado de reintentos
            puntosTotal += casillas * 50;
        }
        else
        {
            //De lo contrario se calcula la puntuacón real y se suma al total
            puntosTotal += casillas * 100;
        }
    }

    public void AsignarPuntosTiempo(float tiempoPuntos)
    {
        //Función que se encarga de asignar la puntuación correcta al jugador basándose en el tiempo sobrante que le quede después de haber copiado la matriz
        //Parámetros: tiempoPuntos - número de punto flotante que hace referencia al tiempo sobrante del jugador
        tiempoPuntos = Mathf.Floor(tiempoPuntos);  //Se redondea hacia abajo el tiempo sobrante
        if(tiempo.estaReintentando)
        {
            //Si el jugador está reintentando se le penalizará su puntuación
            puntosTotal += tiempoPuntos * 50;
        }
        else
        {
            //De lo contrario se calcula la puntuación real y se le suma al total
            puntosTotal += tiempoPuntos * 100;
        }
    }

    public void AsignarPuntosDiagnostico(int puntaje)
    {
        //Función que se encarga de asignar la puntuación correcta al jugador basándose en el puntaje de diagnóstico
        //Parámetros: puntaje - entero que hace referencia al puntaje recibido directamente del script de diagnóstico
        //Se toma el puntaje recibido y se le suma al total
        puntosTotal += puntaje;
    }

    public void GrabarPuntos()
    {
        //Función que "guarda" los puntos en los textos de la interfaz y se los muestra al jugador
        // También se redondea el puntaje final para registrar la partida en la base de datos
        Transiciones.instance.sonidoClick.Play();
        puntos.text = puntosTotal.ToString();
        puntosFinal.text = puntosTotal.ToString();
    }
}
