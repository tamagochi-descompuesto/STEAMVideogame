using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; //jSON CONVERT
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
/*
Permite que se suban los datos de la partida cuando el jugador pasa un punto de control
*(un collider punto de control)
Autores: Erick Hernández Silva y Jacqueline Zavala
*/
public class PuntoGuardado : MonoBehaviour
{
    //Crea una instancia de tipo punto de guardado
    public static PuntoGuardado instance;
    public float guardadoY;//Posición en Y donde el jugador reaparecerá
    public float guardadoX;//Posición en X donde el jugador reaparecerá
    private void Start()
    //Se crea la instancia
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si el jugador consigue ambas piezas y completa el juego
        if(CargarJugador.piezaIBT && CargarJugador.piezaITC)
        {
            //Se reinician las preferencias
            PlayerPrefs.SetFloat("ultimaPosicionX",-8.67f);
            PlayerPrefs.SetFloat("ultimaPosicionY",1.13f);
            PlayerPrefs.SetInt("vidas",3);
            PlayerPrefs.SetInt("piezaIBT",0);
            PlayerPrefs.SetInt("piezaITC",0);
            PlayerPrefs.Save();
            //Se llama al método Partida Completada
            PuntoGuardado.instance.PartidaCompletada();
            //Se carga la escena final
            SceneManager.LoadScene("EscenaFinal");
        }
        else
        {
            //Guarda la posición del jugador al pasar por el collider
            if(other.gameObject.tag == "Player")
            {
                //Se guarda la posición donde debe reaparecer el jugador en las preferencias
                PlayerPrefs.SetFloat("ultimaPosicionX",guardadoX);
                PlayerPrefs.SetFloat("ultimaPosicionY",guardadoY);
                //Se guardan las vidas del jugador en las preferencias
                PlayerPrefs.SetInt("vidas",EstadoPJ.instance.vidas);
                //Si el jugador tiene la pieza de IBT, se guarda en las preferencias
                if(CargarJugador.piezaIBT)
                {
                    PlayerPrefs.SetInt("piezaIBT",1);
                }
                PlayerPrefs.Save();
                //Llama al metodo guardarPuntoControl
                guardarPuntoControl();
            }
        }
    }
    //Estructura del formato de guardado de partida para enviar al servidor web
    public struct Partida{
        public string username;
        public int idPartida;
        public int puntuacionAcumulada;
        public int vidas;
        public int inventario;
        public string estatus;

    }
    //Se crea un objeto de la estructura
    private Partida datosPartida;
    //Guarda los datos de la partida en general
    private IEnumerator guardarPartida(string estatus, bool partidaFinalizada){
        print(DatosUsuario.idPartida.ToString());
        datosPartida.username = DatosUsuario.username;
        datosPartida.idPartida = DatosUsuario.idPartida;
        datosPartida.puntuacionAcumulada = CargarJugador.puntuacionGlobal;
        datosPartida.vidas = EstadoPJ.instance.vidas;
        datosPartida.inventario = EstadoPJ.instance.piezas;
        datosPartida.estatus = estatus;
        //Encapsular los datos que suben a la red
        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datosPartida));
        //Si la partida no está finalizada
        if (!partidaFinalizada)
        {
            UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/partida/guardarPartida",forma);
            yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
            if (request.downloadHandler.text == "success"){// 200
            }
            else
            {
                print(request.downloadHandler.text);
                yield return new WaitForSeconds(3);
                guardarPuntoControl();
            }
        }
        else
        {
            //Si lo ha terminado o ha muerto
            UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/partida/finalizarPartida",forma);
            DatosUsuario.idPartida = 0;
            yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
            if (request.downloadHandler.text == "success"){// Si hubo éxito
                DatosUsuario.idPartida = 0;
            }
            else //Si no hubo éxito en el update
            { 
                if(estatus == "Perdido")
                {
                    StartCoroutine(guardarPartida("Perdido", true));
                }
                else
                {
                    StartCoroutine(guardarPartida("Finalizada", true));
                }
                
            }
            
        }

    }
    //Método que inicia la co-rutina para guardar la partida en progreso

    public void guardarPuntoControl()
    {
        StartCoroutine(guardarPartida("En progreso", false));
    }
    //Método que inicia la co-rutina para guardar la partida al finalizarla
    public void PartidaCompletada()
    {
        StartCoroutine(guardarPartida("Finalizada", true));
    }
    //Método que inicia la co-rutina para guardar la partida al morir
    public void PartidaPerdida()
    {
        StartCoroutine(guardarPartida("Perdido", true));
    }

    //Estructura usada para enviar la información de la jugada
    public struct Jugada{
        public string minijuego;
        public string fechaInicio;
        public string fechaFinal;
        public int puntaje;

    }
    private Jugada datosJugada;
    //Guarda los datos del minijuego jugado
    private IEnumerator guardarMinijuego(string minijuego, int puntaje, string fechaInicio, string fechaFinal){
        datosJugada.minijuego = minijuego;
        datosJugada.fechaInicio = fechaInicio;
        datosJugada.fechaInicio = fechaFinal;
        datosJugada.puntaje = puntaje;
        //Encapsular los datos que suben a la red
        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datosJugada));
        UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/jugador/editarPerfil",forma);
        yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
        if (request.downloadHandler.text != "success") // Si no tuvo exito
        {
            yield return new WaitForSeconds(5);
            guardarMinijuego(minijuego,puntaje,fechaInicio,fechaFinal);

        }

    }
    //Guarda la jugada con los parámetros especificados
    public void subirJugada(string minijuego, int puntaje, string fechaInicio, string fechaFinal)
    {
        StartCoroutine(guardarMinijuego(minijuego, puntaje,fechaInicio,fechaFinal));
    }
}
