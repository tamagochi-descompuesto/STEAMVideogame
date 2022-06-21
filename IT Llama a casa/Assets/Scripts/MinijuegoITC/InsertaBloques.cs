using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

/*
 * Permite al usuario agregar bloques de código haciendo uso de los botones
 * en la parte inferior.
 * 
 * Autor: Erick Bustos
 */
public class InsertaBloques : MonoBehaviour
{
    // Bloque que se va a crear (Prefabs diferentes para cada tipo de instrucción)
    public GameObject bloque;
    // Referencia al panel donde se van a agregar los bloques de código
    public GameObject parent;
    // Número que se le va a asignar a la línea de código
    public static int numBloque = 0;
    // Referencia al botón necesaria para extraer texto de los campos inputfield
    public GameObject boton;
    // Lista que se va a construir con la información a ejecutar
    private List<int> instruccion;
    
    // Texto ingresado en botón
    private Text textoBoton;
    
    // Referencia a la ventana de error en caso se input no válido
    public GameObject ventanaError;
    
    public void AgregarBloque()
    {
        instruccion = new List<int>();
        //Crea una nueva instancia de un cierto bloque de programación
        GameObject bloqueClon = Instantiate(bloque, parent.transform);
        numBloque += 1;
        // Asigno el número correspondiente a la línea de código
        bloqueClon.GetComponentInChildren<Text>().text = numBloque.ToString();
        
        // En caso de que se tengan estas etiquetas debemos preservar la información en el inputfield correspondiente
        if (boton.CompareTag("Avanzar") | boton.CompareTag("Frenar"))
        {
            // Extraer texto botón
            textoBoton = boton.GetComponentsInChildren<Text>()[2];
            
            
            if (String.Equals(textoBoton.text,"") | int.Parse(textoBoton.text) <= 0)
            {
                //Desplegar panel con error
                Destroy(bloqueClon);
                numBloque -= 1;
                ventanaError.gameObject.SetActive(true);
            }
            else
            {
                //  Copiar Número
                bloqueClon.GetComponentsInChildren<Text>()[2].text = textoBoton.text;
            
                if (boton.CompareTag("Avanzar"))
                {
                    instruccion.Add(1);
                } 
                else if (boton.CompareTag("Frenar"))
                {
                    instruccion.Add(2);
                }
            
                instruccion.Add(int.Parse(textoBoton.text));
                MoverPersonaje2.instrucciones.Add(instruccion); //CAMBIO
            }
            
            // Eliminar número ingresado en botón
            boton.GetComponentInChildren<UnityEngine.UI.InputField>().text = "0";
        }
        else if (boton.CompareTag("Izquierda"))
        {
            instruccion.Add(3);
            MoverPersonaje2.instrucciones.Add(instruccion); //CAMBIO
        }
        else if (boton.CompareTag("Derecha"))
        {
            instruccion.Add(4);
            MoverPersonaje2.instrucciones.Add(instruccion); //CAMBIO
        }
        
    }
}
