using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Script que contiene las funciones de verificar las matrices y mostrarle los resultados de esta verificación al jugador
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class VerificarMatrices : MonoBehaviour
{
    //VARIABLES
    public GameObject pantallaResultado;        //Game Object de la pantalla del resultado
    public GameObject celdaMatrizMemorizar;    //Game Object que hace referencia a la matriz a memorizar
    public GameObject celdaMatrizJugador;      //Game Object que hace referencia a la matriz del jugador
    public GameObject botonContinuar;         //Game Object que hace referencia al botón de continuar para pasar a la fase de diagnóstico
    public GameObject botonContinuarPerfecto; //Game Object que hace referencia al botón de continuar si el jugador tiene una puntuación perfecta (en casillas)
    public GameObject botonReintentar;       //Game Object que hace referencia al botón de reintentar
    public GameObject botonVerificar;        //Botón para verificar la matriz
    public Sprite noColor;                   //Imagen que representa un botón "no coloreado"
    public Text textoRes;                    //Texto de resultados del jugador
    private Tiempo tiempo;                   //Instancia de tipo Tiempo
    private Puntaje puntaje;                 //Instancia de tipo Puntaje
    private int counter;                     //Contador que verifica cuantas casillas tuvo bien el jugador
    private int sinColor;                    // Contador que verifica si existen casillas no coloreadas en la matriz

    

    //MÉTODOS
    void Start()
    {
        //Función que se ejecuta antes del primer frame
        //Se enlazan las instancias con sus respectivos objetos
        tiempo = FindObjectOfType<Tiempo>();
        puntaje = FindObjectOfType<Puntaje>();
    }
    
    public void RevisarMatrices()
    {

        //Función que se encarga de revisar cuantas casillas de la matriz del jugador son iguales a las de la matriz a copiar
        Transiciones.instance.sonidoClick.Play();
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                //Se recorren las matrices
                if(GenerarMatriz.matriz[i, j].sprite == MatrizJugador.matriz[i, j].image.sprite)
                {
                    //Si la casilla actual de la matriz a copiar es la misma que la casilla actual de la matriz del jugador
                    counter++;  //Se suma uno al contador de casillas correctas
                }
            }
        }
        PantallaResultado();  //Después de verificar la matriz se ejecuta el método de pantalla de resultado
    }

    public void PantallaResultado()
    {
        //Función que muestra al jugador una pantalla con los resultados de verificar las matrices
        pantallaResultado.SetActive(true);
        Time.timeScale = 0;  //Se pausa el juego
        if(counter == 1)
        {
            //Si solo se tiene una casilla correcta el mensaje se hace en singular (casilla en vez de casillas)
            textoRes.text = "Tienes " + counter.ToString() + " casilla correcta de 16\n ¿Deseas reintentarlo?";

        }
        else if(counter == 16)
        {
            //Si tiene todas las casillas bien se le notificará que tuvo una puntuación perfecta
            textoRes.text = "¡Felicidades, tuviste una puntuación perfecta!";
            botonContinuar.SetActive(false);
            botonContinuarPerfecto.SetActive(true);  //Se desactivan los otros botones y solo se muestra el de continuar, el jugador no podrá reintentar
            botonReintentar.SetActive(false);
        }
        else
        {
            //Cualquier otro caso le mostrará al jugador sus resultados
            textoRes.text = "Tienes " + counter.ToString() + " casillas correctas de 16\n ¿Deseas reintentarlo?";
        }
    }

    public void DesactivarResultadoReintentar()
    {
        //Función que desactiva la pantalla de resultados cuando el jugador decide reintentar, va asignada a un botón
        Time.timeScale = 1;  //Se resume el juego
        pantallaResultado.SetActive(false);
        counter = 0;  //Se resetea el contador
        celdaMatrizJugador.SetActive(false);
        celdaMatrizMemorizar.SetActive(true);
        tiempo.textoMemoCop.text = "Memoriza";  //Se resetea el texto
    }

    public void DesactivarResultadoContinuar(bool esPerfecto)
    {
        //Función que desactiva la pantalla de resultados cuando el jugador decide continuar, va asignada a un botón 
        Time.timeScale = 1;  //Se resume el juego
        pantallaResultado.SetActive(false);
        if(esPerfecto)
        {
            //Si la puntuación es perfecta se ocultará el botón de perfecto y se volverán a mostrar los demás botones
            botonContinuar.SetActive(true);
            botonContinuarPerfecto.SetActive(false);
            botonReintentar.SetActive(true);
        }
        //Al continuar se asignan los puntos relacionados a la presición de copiado y al tiempo restante
        puntaje.AsignarPuntosPrecision(counter);     
        puntaje.AsignarPuntosTiempo(tiempo.tiempoRestante);
        counter = 0;  //Se resetea el contador
    }

    public void EstaColoreado()
    {
        //Función que se encarga de revisar si al menos una casilla de la matriz del jugador no está coloreada 
        sinColor = 0;
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                //Se recorren las matrices
                if(MatrizJugador.matriz[i, j].image.sprite == noColor)
                {
                    //Si la casilla actual de la matriz no tiene color
                    sinColor++;  //Se suma uno al contador de casillas no coloreadas

                }
            }
        }
        if (sinColor == 0)
        {
            //Si no hay ninguna casilla sin colorear, se mostrará el botón de verificar
            botonVerificar.SetActive(true);
        }
    }
}
