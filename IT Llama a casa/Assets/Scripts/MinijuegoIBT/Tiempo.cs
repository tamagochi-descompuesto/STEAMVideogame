using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Código que manipula el tiempo dentro del minijuego y todas sus funciones
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class Tiempo : MonoBehaviour
{
    //VARIABLES
    public float tiempoRestante;                   //Variable que indica el tiempo restante para memorizar o copiar
    private int tiempoTotal = 25;                  //Variable que indica el tiempo total de la ronda
    private bool estaCorriendo = true;             //Variable que indica si el tiempo está corriendo
    private bool estaCopiando;                     //Variable que indica si el jugador está copiando
    public bool estaReintentando;                  //Variable que indica si el jugador está reintentando o no
    public Text textoMemoCop;                      //Texto que indica si el jugador debe de memorizar o copiar la matriz
    public GameObject matrizCopiar;                //Game Objects de las martrices del jugador y del juego
    public GameObject matrizJugador;                
    public GameObject botonVerificar;              //Botón que verifica las matrices             
    public Image barraTiempo;                      //Imagen de la barra de tiempo
    public static Tiempo instance;                //Instancia de tipo Tiempo
    private VerificarMatrices verificarMatrices;  //Instancia de tipo VerificarMatrices

    void Start()
    {
        //Función que se ejecuta antes del primer frame
        AsignarTiempo();  //Se manda a llamar la función de asignar tiempo
        matrizJugador.SetActive(false);  
        float segundos = Mathf.FloorToInt(tiempoRestante % 60);  //Se calculan los segundos restantes
        verificarMatrices = FindObjectOfType<VerificarMatrices>();  //Se enlaza verificarMatrices con un objeto de tipo VerificarMatrices
    }

    void Awake()
    {
        //Se crea una instancia de la clase Tiempo
        instance = this;
    }

    public void Update()
    {
        //Función que se ejecuta cada frame, en este caso funciona como el temporizador
        if(estaCorriendo)
        {   
            //Si el tiempo está corriendo:
            float segundos = Mathf.FloorToInt(tiempoRestante % 60); //Se calculan los segundos restantes
            if(segundos >= 0)
            {
                //Si todavía no se ha acabado el tiempo:
                tiempoRestante -= Time.deltaTime;  //Se le resta el diferencial de tiempo al tiempo inicial
                //Se cambia el largo de la barra de tiempo
                barraTiempo.rectTransform.localPosition = new Vector3(tiempoRestante * barraTiempo.rectTransform.rect.width / tiempoTotal - barraTiempo.rectTransform.rect.width, 0, 0);
            }
            else if(segundos < 0 && estaCopiando)
            {
                //Si ya se acabó el tiempo y el usuario está copiando:
                //Se resetean las variables
                estaCopiando = false;
                estaCorriendo = false;
                verificarMatrices.RevisarMatrices();  //Se manda a llamar la verificación de matrices
            }
            else
            {
                //Si ya se acabó el tiempo (en este caso de memorizar)
                //se muestra la matriz del jugador para que pueda copiar
                matrizJugador.SetActive(true);
                botonVerificar.SetActive(false);
                matrizCopiar.SetActive(false);  //Se oculta la matriz a memorizar
                AsignarTiempo();  //Se vuelve a asignar el tiempo
                textoMemoCop.text = "Copia";  //Se le indica al jugador que tiene que copiar
                estaCopiando = true;
            }
        }
    }

    public void ResetearContador(bool reintentar)
    {
        //Función que se encarga de resetear el contador, va asignada a botones
        //Parámetros: reintentar - indica si el botón presionado es el de reintentar o no
        Transiciones.instance.sonidoClick.Play();
        AsignarTiempo();  //Se vuelve a asignar el tiempo dependiendo de la ronda
        estaCorriendo = true;  //Se inidica que esta corriendo
        if(reintentar)  //Si el jugador está reintentando entonces se cambiará la variable estaReintentando
        {
            estaReintentando = true;
        }
        else
        {
            estaReintentando = false; 
        }
    }

    public void AsignarTiempo()
    {
        //Función que asigna el tiempo que el jugador tiene para memorizar o copiar la matriz dependiendo de la ronda
        if(GenerarMatriz.ronda == 1)
        {
            //Si es la primer ronda el jugador tendrá 30 segundos
            tiempoRestante = 22;
            tiempoTotal = 22;
        }
        else if(GenerarMatriz.ronda == 2)
        {
            //Si es la segunda ronda el jugador tendrá 25 segundos
            tiempoRestante = 20;
            tiempoTotal = 20;
        }
        else if(GenerarMatriz.ronda == 3)
        {
            //Si es la tercera ronda el jugador tendrá 15 puntos
            tiempoRestante = 18;
            tiempoTotal = 18;
        }
    }

    public void ResetearEstaCopiando()
    {
        //Función que resetea la variable estaCopiando, va asignada a un botón
        estaCopiando = false;
    }

    public void ResetearEstaCorriendo()
    {
        //Función que resetea la variable estaCorriendo, va asignada a un botón
        estaCorriendo = false;
    }
}
