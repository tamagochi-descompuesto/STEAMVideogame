using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Gestiona los aspectos relacionados con el diagnóstico del microarreglo tales como 
* la asignación interna o la selección del jugador
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class Diagnostico : MonoBehaviour
{
    //VARIABLES    
    public GameObject celdaMatrizJugador;  //Game Objects de ambos paneles de microarreglos
    public GameObject celdaMatrizCopiar;
    public GameObject panelTiempo;         //Panel del tiempo de copiado y memorizado
    public GameObject botonVerificar;      //Botón para verificar ambas matrices
    public GameObject panelDiagnostico;    //Panel de la escena que desplegará las opciones de diagnóstico
    public GameObject panelRecuerda;       //Panel de "tips" para el jugador
    public GameObject botDiag1;            //Botón del diagnóstico uno (etapa avanzada)
    public GameObject botDiag2;            //Botón del diagnótico dos (etapa temprana)
    public GameObject botCont;             //Botón de continuar, manda al jugador a la siguiente ronda
    public Text textoDiagnostico;          //Texto que indica si el diagnóstico fue correcto o incorrecto
    private Puntaje puntaje;               //Puntaje del jugador, instancia de clase Puntaje
    private bool diagnostico;              //Variable booleana que indica si el diagnóstico es avanzado o temprano
    private bool diagnosticoJugador;       //Variable booleana que indica el diagnóstico asignado por el jugador

    //MÉTODOS
    void Start()
    {
        //Función que se ejecuta en el primer frame
        puntaje = FindObjectOfType<Puntaje>();  //Se enlaza la instancia puntaje con un objeto de tipo Puntaje
    }

    public void MostrarDiagnostico()
    {
        //Función que muestra los elementos referentes al diagnóstico en pantalla
        //Los Game Objects que no se necesitan mostrar se ocultan y aparecen nuevos Game Objects
        celdaMatrizJugador.SetActive(false);
        celdaMatrizCopiar.SetActive(true);
        panelTiempo.SetActive(false);
        panelDiagnostico.SetActive(true);
        botDiag1.SetActive(true);
        botDiag2.SetActive(true);
        botCont.SetActive(false);
        panelRecuerda.SetActive(true);
    }

    public void OcultarDiagnostico()
    {
        //Función que oculta los elementos referentes al diagnóstico en pantalla
        //Se ocultan los Game Objects referentes al diagnóstico y se "resetea" el texto del diagnóstico
        panelTiempo.SetActive(true);
        panelDiagnostico.SetActive(false);
        panelRecuerda.SetActive(false);
        textoDiagnostico.text = "¿Cuál es el diagnóstico?";
    }

    public void AsignarDiagnostico()
    {
        //Función que asigna el diagnóstico real dependiendo de la cantidad de celdas rojas y verdes que haya
        if(GenerarMatriz.diagRojo > GenerarMatriz.diagVerde) //Si hay más celdas rojas que verdes
        {
            diagnostico = false; //El diagnóstico se toma como false, es decir, etapa avanzada
        }
        else if(GenerarMatriz.diagRojo < GenerarMatriz.diagVerde)
        {
            diagnostico = true;  //De lo contrario el diagnóstico es true, es decir, etapa temprana
        }
    }

    public void AsignarDiagnosticoPJ(bool diag)
    {
        //Función que se ejecuta cuando el jugador selecciona un botón de diagnóstico, le asigna un valor al diagnóstico del jugador
        //Parámetros: diag - variable booleana, hace referencia al diagnóstico del jugador, dependiendo del botón que presione se le asignará
        //true o false al diagnostico del jugador, siendo true un diagnóstico de etapa temprana y flase uno de etapa avanzada
        Transiciones.instance.sonidoClick.Play();
        diagnosticoJugador = diag;
        VerificarDiagnostico();  //Una vez asignado el diagnóstico se ejecuta la función de verificar diagnóstico
    }

    public void VerificarDiagnostico()
    {
        //Función que compara el diagnóstico real con el del jugador para verificar si su respuesta fue correcta
        if(diagnosticoJugador == diagnostico)
        {
            //Si el diagnóstico fue correcto se le notifica al jugador y se le asigna la puntuación correspondiente
            textoDiagnostico.text = "¡Tu diagnóstico es correcto!";
            puntaje.AsignarPuntosDiagnostico(1000);
        }
        else
        {
            //De lo contrario se le notifica al jugador y se le asigna una puntuación penalizada
            textoDiagnostico.text = "Tu diagnóstico es incorrecto :(";
            puntaje.AsignarPuntosDiagnostico(250);
        }
        //Se ocultan los botones de diagnóstico y se muestra el botón de continuar, permitiéndole al jugador ir a la siguiente ronda
        botDiag1.SetActive(false);
        botDiag2.SetActive(false);
        botCont.SetActive(true);
    }
}
