using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Permite guardar los datos del usuario de forma local durante la ejecucion del juego
Autor: Erick Hernández Silva y Jacqueline Zavala
*/
public class DatosUsuario : MonoBehaviour
{
    // Se crean las variables estáticas que se usarán para guardar los datos del usuario
    public static string username;  //Username y correo se usan para queries
    public static string correo;
    public static int idPartida;  //IDPartida se usa para comunicarse con el servidor web
}