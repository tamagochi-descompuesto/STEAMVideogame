using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
/*
 * Permite determinar si el jugador choca con un enemigo o pasa al siguiente nivel.
 * Al chocar la nave con un enemigo ejecuta la animación de una explosión y regresa al jugador a la posición inicial.
 * Al colisionar con el planeta de destino, se desactivan los enemigos del nivel actual, se despliegan los
 * enemigos del nuevo nivel y se regresa al jugador a la posición inicial.
 *
 * Autor: Erick Bustos
 */
public class Nave2 : MonoBehaviour
{
    
    // Nivel Actual
    public static int nivelActual = 1;
    
    // Enemigos de cada nivel
    public  GameObject enemigosNivel1;
    public  GameObject enemigosNivel2;
    public  GameObject enemigosNivel3;
    public  GameObject enemigosNivel4;
    public  GameObject enemigosNivel5;
    
    // Referencia al botón Play
    public Button botonplay;
    
    // Referencia al botón de eliminar línea de código
    public Button botoneliminar;
    
    // Referencia al botón reiniciar
    public GameObject reiniciar;
    
    // Puntos acumulados
    public static int puntaje = 0;
    
    // Puntos del último nivel
    public static int puntosnivel;
    
    // Referencia panel nivel completado
    public GameObject nivelCompletado;
    
    // Referencia al texto donde se agregan los puntos e
    public Text puntosTexto;
    
    // Referencia al campo de texto de panel puntos
    public Text textoPanelPuntos;
    
    // Referencia al panel de minijuego completado
    public GameObject minijuegoCompletado;
    
    // Referencia al campo de texto del panel minijuegoCompletado
    public Text textoMinijuegoCompletado;
    
    // Panel donde se agregan las líneas de código
    public GameObject scrollPanel;
    
    //  DateTime inicio de la jugada
    public string dateTimeInicioJugada;
    
    // DateTime fin de la jugada
    public string dateTimeFinJugada;
    
    // Audio Source Explosión
    public AudioSource explosion;
    
    // Audio Source Éxito
    public AudioSource exito;
    
    // Ver si está en colisión
    public static bool enColision = true;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cuando el jugador completa el nivel
        if (other.gameObject.CompareTag("Destino"))
        {
            exito.Play();
            Debug.Log("DESTINO");
            // Actualizar nivel
            nivelActual += 1;

            // Cambiar de enemigos, actuaizar puntos y desplegar paneles de cambio de nivel
            if (nivelActual == 2)
            {
                // Calcular puntos nivel
                puntosnivel = 2000 * 6 / MoverPersonaje2.instrucciones.Count;
                // Sumar puntos al total
                puntaje += puntosnivel;
                // Actualizar puntos en el display del juego
                puntosTexto.text = puntaje.ToString();
                // Actualizar puntos en la pantalla de nivel completado
                textoPanelPuntos.text = puntosnivel.ToString();
                // Desplegar panel de nivel completado
                nivelCompletado.SetActive(true);
                // Cambiar los enemigos por los del siguiente nivel
                enemigosNivel1.SetActive(false);
                enemigosNivel2.SetActive(true);
                
            }
            else if (nivelActual == 3)
            {
                puntosnivel = 2000 * 3 /MoverPersonaje2.instrucciones.Count;
                puntaje += puntosnivel;
                puntosTexto.text = puntaje.ToString();
                textoPanelPuntos.text = puntosnivel.ToString();
                nivelCompletado.SetActive(true);
                enemigosNivel2.SetActive(false);
                enemigosNivel3.SetActive(true);
            }
            else if (nivelActual == 4)
            {
                puntosnivel = 2000 * 3 /MoverPersonaje2.instrucciones.Count;
                puntaje += puntosnivel;
                puntosTexto.text = puntaje.ToString();
                textoPanelPuntos.text = puntosnivel.ToString();
                nivelCompletado.SetActive(true);
                enemigosNivel3.SetActive(false);
                enemigosNivel4.SetActive(true);
            }
            else if (nivelActual == 5)
            {
                puntosnivel = 2000 * 6 /MoverPersonaje2.instrucciones.Count;
                puntaje += puntosnivel;
                puntosTexto.text = puntaje.ToString();
                textoPanelPuntos.text = puntosnivel.ToString();
                nivelCompletado.SetActive(true);
                enemigosNivel4.SetActive(false);
                enemigosNivel5.SetActive(true);
            }
            else if (nivelActual == 6)
            {
                // Encuentra la hora en la que terminó el minijuego para registrarlo en la BD
                dateTimeFinJugada = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                print(dateTimeInicioJugada);
                print(dateTimeFinJugada);
                
                // Actualiza la puntuación total
                puntosnivel = 2000 * 9 /MoverPersonaje2.instrucciones.Count;
                puntaje += puntosnivel;
                // Guarda la información en preferencias y en CargarJugador
                CargarJugador.puntuacionGlobal += puntaje;
                CargarJugador.piezaITC = true;
                PlayerPrefs.SetInt("piezaITC",1);
                PlayerPrefs.Save();
                //Sube lo datos a la BD
                StartCoroutine(SubirJugada(dateTimeInicioJugada,dateTimeFinJugada,puntaje));
                // Actualiza la puntuación del Panel minijuegocompletado y lo despliega
                textoMinijuegoCompletado.text = puntaje.ToString();
                minijuegoCompletado.SetActive(true);
                
            }
            
            // Desactivar la ejecución de animaciones
            MoverPersonaje2.ejecuta = false;

            // Mover contador del vector de instrucciones a 0
            MoverPersonaje2.contadordelvector = 0;
            
            // Regresar la nave a su posición inicial
            transform.position = new Vector3((float) -8.5,(float) 4.5);
            transform.rotation =  Quaternion.Euler(0, 0, 0);
            
            // Reactivar Botones
            botoneliminar.interactable = true;
            botonplay.interactable = true;
            reiniciar.SetActive(false);
            
            // Limpiar bloques
            InsertaBloques.numBloque = 0;
            MoverPersonaje2.instrucciones = new List<List<int>>();
            foreach (Transform child in scrollPanel.transform)
            {
                Destroy(child.gameObject);
            }
            
        }
        else
        {
            Debug.Log("EXPLOSION");
            // Desactivar la ejecución de animaciones
            MoverPersonaje2.ejecuta = false;

            // Mover contador del vector de instrucciones a 0
            MoverPersonaje2.contadordelvector = 0;
            
            // Dejar de dibujar la nave
            GetComponent<SpriteRenderer>().enabled = false;
            
            // Activar la explosión
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            if (enColision)
            {
                // Activar sonido explosión
                explosion.Play();
                enColision = false;
            }
            
            
            // Llamar corrutina
            StartCoroutine(ContarExplosion());
            

        }
    }

    private void Awake()
    {
        dateTimeInicioJugada = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }
    

    private IEnumerator ContarExplosion()
    {
        
        // Espera medio segundo y desactiva la explosión
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        // Espera medio segundo y regresa la nave al punto inicial
        transform.position = new Vector3((float) -8.5,(float) 4.5);
        //transform.rotation =  Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.identity;
        //Dibujar la nave de nuevo
        enColision = true;
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(2);
        
        // Reactivar Botones
        botoneliminar.interactable = true;
        botonplay.interactable = true;   
    }

    // Estructura para subir datos a la BD
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
        // Asignar los datos a subir
        datosJugada.minijuego = "ITC";
        datosJugada.fechaInicio = fechaInicio;
        datosJugada.fechaFinal = fechaFinal;
        datosJugada.puntaje = puntaje;
        datosJugada.PartidaIdPartida = DatosUsuario.idPartida;
        
        //Encapsular los datos que suben a la red
        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datosJugada));
        UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/jugadas/agregarJugada",forma);
        yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
        if (request.downloadHandler.text == "success") // 200
        {
            yield return new WaitForSeconds(1);
        }
        else
        {
            print(request.downloadHandler.text);
            yield return new WaitForSeconds(3);
            StartCoroutine(SubirJugada(fechaInicio,fechaFinal,puntaje));
        }
    }
}
