using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
* Permite darle funcionalidad a los botones de retorno.
* Autor: Jacqueline Zavala
*/

public class RegresarMenu : MonoBehaviour
{
    // Función que permite cargar la escena del Menú Principal.
    public void Regresar()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
    
}
