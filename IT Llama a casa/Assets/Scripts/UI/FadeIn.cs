using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Reproduce el efecto Fade-In en la escena
Autores: David Rodríguez e Israel Sánchez
*/

public class FadeIn : MonoBehaviour
{
    //VARIABLES
    public Image imagenFondo; //La imagen de fondo
    void Start()
    {
        // Función que reproduce el efecto de Fade-In
        imagenFondo.CrossFadeAlpha(0, 1, true);
    }
}
