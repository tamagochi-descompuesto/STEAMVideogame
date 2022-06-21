using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* Script que le da sus funcionalidades a la escena del inicio.
* La funcionalidad principal es iniciar sesión o registrarse.
* Autor: Erick Bustos
*/

public class MenuInicial : MonoBehaviour
{
    public void IniciaSesion()
    {
        // Función que carga la escena de iniciar sesión.
        SceneManager.LoadScene("MenuIniciarSesion");
    }

    public void Registro()
    {
        // Función que abre en el navegador del usuario el formulario de registro.
        Application.OpenURL("http://18.116.89.34:8080/jugador/formularioRegistro#");
    }
    public void Salir()
    {
        // Función que permite salir del juego (finalizar la aplicación).
        Application.Quit();
    }
}