using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
/*
* Código que le otorga todas sus funciones a la matriz a copiar y sirve como esqueleto del juego
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class GenerarMatriz : MonoBehaviour
{
    //VARIABLES
    public Image bloque1;              //Imágenes que representan cada celda del microarreglo            
    public Image bloque2;
    public Image bloque3;
    public Image bloque4;
    public Image bloque5;
    public Image bloque6;
    public Image bloque7;
    public Image bloque8;
    public Image bloque9;
    public Image bloque10;
    public Image bloque11;
    public Image bloque12;
    public Image bloque13;
    public Image bloque14;
    public Image bloque15;
    public Image bloque16;
    public Sprite rojo;                //Sprites que modificarán la imagen de cada celda de la matriz
    public Sprite amarillo;
    public Sprite verde;
    public GameObject panelFinal;      //Game Object que hace referencia al panel del final del juego
    public GameObject zonaJuego;       //Game Object que hace referencia a toda la zona del juego
    public Text textoRonda;            //Texto que indica en que ronda se encuentra el juego
    public static Image[,] matriz;     //Matriz de imágenes que representa la matriz a copiar
    public static int ronda = 1;       //Ronda actual del juego
    private int probabilidadRojo;      //Variable que indica la probabilidad de que aparezca una celda de color rojo
    private int probabilidadAmarillo;  //Variable que indica la probabilidad de que aparezca una celda de color amarillo
    private int probabilidadVerde;     //Variable que indica la probabilidad de que aparezca una celda de color verde
    public static int diagRojo;        //Variable que indica la cantidad de celdas rojas que hay, se asocia con el diagnóstico real
    public static int diagVerde;       //Variable que indica la cantidad de celdas verdes que hay, se asocia con el diagnóstico real
    private string fechaInicial;
    public AudioSource audioRonda;
    

    //MÉTODOS
    void Start()
    {
        //Función que se ejecuta antes del primer frame
        //Debe de haber una pantalla de inicio y este método debe de estar en un botón
        textoRonda.text = ronda.ToString();  //Se le indica al jugador de la ronda en la que está
        //Se crea una nueva matriz de imágenes con las imágenes del microarreglo
        matriz = new Image[,]{{bloque1, bloque2, bloque3, bloque4}, {bloque5, bloque6, bloque7, bloque8}, {bloque9, bloque10, bloque11, bloque12}, {bloque13, bloque14, bloque15, bloque16}};
        AsignarColor();  //Se manda a llamar a la función asignar color
    }

    void Awake()
    {
        fechaInicial = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }

    private void AsignarColor()
    {
        //Función que le asigna un color a cada imagen del microarreglo
        System.Random random = new System.Random();  //Se crea una instancia de la liberría Random
        AsignarProbabilidad();  //Se manda a llamar a asignar probabilidad 
        diagRojo = 0;  //Se inicializan los valores de ambos diagnósticos
        diagVerde = 0;
        for(int i = 0; i < 4; i++)  //Se inicia el ciclo para recorrer las filas de la matriz
        {   
            for(int j = 0; j < 4; j++)  //Se inicia el ciclo para recorrer las columnas de la matriz
            {
                int num = random.Next(101);  //Se esocge un número random entre 0 y 100
                if(num <= probabilidadRojo)  //Si el número cae entre las probabilidades del color rojo
                {
                    //La source image de la imagen en esa celda cambia a rojo y se le suma un uno al diagnóstico rojo
                    matriz[i, j].sprite = rojo;
                    diagRojo++;
                }
                else if(probabilidadRojo < num && num <= probabilidadAmarillo)  //Si el número cae entre las posibilidades del color amarillo
                {
                    //La source image de la imagen en esa celda cambia a amarillo
                    matriz[i, j].sprite = amarillo;
                }
                else if(probabilidadAmarillo < num && num <= probabilidadVerde)  //Finalmente, si el número cae entre las posibilidades del color verde
                {
                    //La source image de la imagen en esa celda cambia a verde y se le suma un uno al diagnóstico verde
                    matriz[i, j].sprite = verde;
                    diagVerde++;
                }
            }
        }
        if(diagRojo == diagVerde)
        {
            //Si hay un mismo número de celdas verdes que rojas se vuelve a llamar al método
            AsignarColor();
        }
    }

    public void CambiarRonda()
    {
        //Función que se encarga de cambiar la ronda en la que el juego se encuentra, va asociada a un botón
        ronda++;  //Se le suma un uno a la ronda
        textoRonda.text = ronda.ToString();  //Se cambia el texto de la ronda
        audioRonda.Play();
        if(ronda > 3)
        {
            // variable local que obtiene la fecha en la que se finalizó el minijuego
            string fechaFinal = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            //Si ya es la ronda final se tendrá que mostrar el panel final del juego
            ronda = 1;  //Se reseta la ronda
            StartCoroutine(SubirJugada(fechaInicial, fechaFinal, (int)Puntaje.puntosTotal));
            CargarJugador.puntuacionGlobal += (int)Puntaje.puntosTotal;
            panelFinal.SetActive(true);
            zonaJuego.SetActive(false);
        }
        else
        {
            //De lo contrario se vuelve a asignar un color a cada celda de la matriz a copiar
            AsignarColor();
            Tiempo.instance.textoMemoCop.text = "Memoriza";  //Se "resetea" el texto del tiempo
        }
    }

    public void CargarEscena()
    {
        //Función que carga la escena del mapa, se asocia a un botón, se debe de ejecutar cuando el minijuego acaba
        PlayerPrefs.SetInt("piezaIBT",1);
        PlayerPrefs.Save();
        CargarJugador.piezaIBT = true;
        //Sube la jugada a la BD
        SceneManager.LoadScene("Mapa");
        Time.timeScale = 1;
 
        
    }

    public void AsignarProbabilidad()
    {
        //Función que asigna una probabilidad a cada color dependiendo de la ronda
        if(ronda == 1)
        {
            //Si es la primer ronda las probabilidades serán las siguientes:
            probabilidadRojo = 45;      //El color rojo tendrá 45% de probabilidades de salir
            probabilidadAmarillo = 65;  //El amarillo tendrá 20% de probabilidades de salir
            probabilidadVerde = 100;    //El verde tendrá 35% de probabilidades de salir
        }
        else if(ronda == 2)
        {
            //Si es la segunda ronda:
            probabilidadRojo = 20;      //El rojo tendrá 20% de probabilidades
            probabilidadAmarillo = 40;  //El amarillo 20% de probabilidades
            probabilidadVerde = 100;    //El verde 60% de probabilidades
        }
        else if(ronda == 3)
        {
            //Si es la tercera ronda
            probabilidadRojo = 30;      //El rojo tendrá 30% de probabilidades
            probabilidadAmarillo = 60;  //El amarillo tendrá 30% de probabilidades
            probabilidadVerde = 100;    //El verde tendrá 40% de probabilidades
        }
    }
    //Estructura de una jugada
     public struct Jugada{
        public string minijuego;
        public string fechaInicio;
        public string fechaFinal;
        public int puntaje;
        public int PartidaIdPartida;

    }
    private Jugada datosJugada;
    //Sube la jugada a la BD
    private IEnumerator SubirJugada(string fechaInicio, string fechaFinal, int puntaje){
        datosJugada.minijuego = "IBT";
        datosJugada.fechaInicio = fechaInicio;
        datosJugada.fechaFinal = fechaFinal;
        datosJugada.puntaje = puntaje;
        datosJugada.PartidaIdPartida = DatosUsuario.idPartida;
        //Encapsular los datos que suben a la red
        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datosJugada));
        UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/jugadas/agregarJugada",forma);
        yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
        if (request.downloadHandler.text == "success"){// 200
            yield return new WaitForSeconds(1);
        }
        else{
            print(request.downloadHandler.text);
            yield return new WaitForSeconds(3);
            StartCoroutine(SubirJugada(fechaInicio,fechaFinal,puntaje));
        }
    }
}
