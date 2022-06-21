using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Permite ejecutar las instrucciones del usuario al presionar el botón play.
 * Se lee el vector isntrucciones y se ejecutan los movimientos dentro de la función
 * update. Se utilizan corrutinas para determinar cuando debe dejarse de ejecutar una
 * animación y continuar con la siguiente.
 * Autor: Erick Bustos
 */
public class MoverPersonaje2 : MonoBehaviour
{
    // Referencia al transform de la nave
    private Transform transformNave;
    private Rigidbody2D rb2D;

    // Índice que me permite iterar sobre el vector de instrucciones
    public static int contadordelvector = 0;
    
    // Identificador de la instrucción actual (1 = avanzar, 2 = esperar, 3 = girar izq, 4 = girar der)
    public static int instruccionactual;
    
    // Booleano que inicializa la corrutina contador cuando se comienza a ejecutar una nueva instrucción
    public static bool comenzaracontar = true;
    
    // Booleano de detiene por completo la ejecución de movimiento si el usuario pasa el nivel o muere
    public static bool ejecuta = false;

    // Referencia al botón Play
    public Button botonplay;
    
    // Referencia al botón de eliminar línea de código
    public Button botoneliminar;
    
    // Referencia al botón de reiniciar proceso
    public GameObject reiniciar;
    
    // Arreglo con las intrucciones
    public static List<List<int>> instrucciones = new List<List<int>>(); 
    
    

    void Start()
    {
        transformNave = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Función Asociada a Botón Play
    public void EjecutaCodigo()
    {
        if (InsertaBloques.numBloque > 0)
        {
            print("Angulo Inicial: " + transform.rotation.z);

            // Desactivamos la interactividad de los botones
            botoneliminar.interactable = false;
            botonplay.interactable = false;
            
            contadordelvector = 0;
            comenzaracontar = true;
     
            
            // Iniciamos la ejecución
            ejecuta = true;
            
        }
    }
    
    
    void Update()
    {
        
        //Si acabamos de leer todas las instrucciones dejar de ejecutar
        if (contadordelvector == instrucciones.Count & contadordelvector != 0)
        {
            ejecuta = false;
            
            // Si el personaje solo rotó y no avanzó reiniciar
            if (transform.rotation.z != 0)
            {
                contadordelvector = 0;
                reiniciar.SetActive(true);
            }
            
            // Si el personaje se quedó atorado y no perdió ni ganó permitir regresarlo a la posición inicial
            if ((rb2D.velocity.x == 0 | rb2D.velocity.x == 0) & (!(transform.position.x <= -8.5) | !(transform.position.y >= 4.5)))
            {
                // Mover contador del vector de instrucciones a 0
                contadordelvector = 0;
                reiniciar.SetActive(true);
                
                
            }
            
        }
        
        
        if (ejecuta)
        {
            // Obtener tipo de intrucción
            instruccionactual = instrucciones[contadordelvector][0];
            
            // Avanzar
            if (instruccionactual == 1)
            {
                float ntime = Time.deltaTime * 2;
                transform.Translate(Vector3.right * ntime);
                if (comenzaracontar)
                {
                    
                    StartCoroutine(EsperaAvanzar(instrucciones[contadordelvector][1]));
                    comenzaracontar = false;
                }
                
            }
            // Esperar
            else if (instruccionactual == 2)
            {
                if (comenzaracontar)
                {
                    StartCoroutine(Espera(instrucciones[contadordelvector][1]));
                    comenzaracontar = false;
                }
                
            }
            // Girar Izquierda
            else if (instruccionactual == 3)
            {
                transform.Rotate(0,0,Time.deltaTime * 45);
                if (comenzaracontar)
                {
                    StartCoroutine(EsperaRotacion());
                    comenzaracontar = false;
                }
                
            }
            // Girar Derecha
            else if (instruccionactual == 4)
            {
                transform.Rotate(0,0,Time.deltaTime * -45);
                if (comenzaracontar)
                {
                    StartCoroutine(EsperaRotacion());
                    comenzaracontar = false;
                }
                
            }
        }
        else
        {
            StopAllCoroutines();
        }

        
        
    }

    // Rutina que cuenta el tiempo que deben ejecutarse La instrucción avanzar
    // y al terminar, frena su ejecución y permite ejecutar una nueva intrucción
    public IEnumerator EsperaAvanzar(int tiempo)
    {
        Debug.Log("AQUI EJECUTAMOS ESPERA");
        Debug.Log("Y EJECUTA ES" + ejecuta.ToString());
        
        yield return new WaitForSeconds(tiempo/2f);
        if (ejecuta)
        {
            contadordelvector += 1;
        }
        comenzaracontar = true;
        
    }
    
    // Rutina que cuenta el tiempo que deben ejecutarse la instrucción parar
    // y al terminar, frena su ejecución y permite ejecutar una nueva intrucción
    public IEnumerator Espera(int tiempo)
    {
        Debug.Log("AQUI EJECUTAMOS ESPERA");
        Debug.Log("Y EJECUTA ES" + ejecuta.ToString());
        
        yield return new WaitForSeconds(tiempo/2f);
        if (ejecuta)
        {
            contadordelvector += 1;
        }
        comenzaracontar = true;
        
    }
    
    // Rutina que cuenta el tiempo que deben ejecutarse las instrucciones relacionadas con girar
    // y al terminar, frena su ejecución y permite ejecutar una nueva intrucción
    public IEnumerator EsperaRotacion()
    {
        Debug.Log("AQUI EJECUTAMOS ESPERA ROTACION");
        Debug.Log("Y EJECUTA ES " + ejecuta.ToString());
              
        yield return new WaitForSeconds(2);
        Debug.Log("Y la ROTACION ES" + transform.rotation.z);
        if (ejecuta)
        {
            contadordelvector += 1;
        }
        comenzaracontar = true;
    }
}
