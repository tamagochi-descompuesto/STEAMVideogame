using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Script para prevenir que el usuario deje un inputfield vacío
 * Se rellena automáticamente con cero
 * Autor: Erick Bustos
 */
public class ValidarInputField : MonoBehaviour
{
    // Referencia al Inputfield
    public InputField inputfield;

    // Checa si el texto del inputfield de los botónes para agregar bloques fue dejado vacío 
    public void CheckForEmpty()
    {
        // Si está vacío rellenarlo con 0
        if (inputfield.text.Length == 0)
        {
            inputfield.text = "0";
        }
    }
}
