using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Permite regresar la nave a sus coordenadas y rotación iniciales
 * Autor: Erick Bustos
 */
public class ReiniciarAnimacion : MonoBehaviour
{
    // Referencia a la nave
    public GameObject nave;
    public GameObject boton;
    
    // Referencia al botón Play
    public Button botonplay;
    
    // Referencia al botón de eliminar línea de código
    public Button botoneliminar;
    
    // Función asignada al botón que resetea la animación en caso de no haber llegado a la meta
    public void Restart()
    {
        // Modificar Transform
        nave.transform.position = new Vector3((float) -8.5,(float) 4.5);
        // Modificar Rotación
        nave.transform.rotation =  Quaternion.Euler(0, 0, 0);
        // Ocultar botón
        boton.gameObject.SetActive(false);
        // Reactivar Botones
        botoneliminar.interactable = true;
        botonplay.interactable = true;
        
    }
}
