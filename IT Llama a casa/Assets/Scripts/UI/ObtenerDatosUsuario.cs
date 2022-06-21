using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Obtiene la información del correo del jugador y lo 
* despliega en el campo de la escena Editar Perfil
*Autores: Erick Hernández Silva, Jacqueline Zavala.
*/
public class ObtenerDatosUsuario : MonoBehaviour
{
    public InputField nuevoCorreo;
    
    public void Start()
    {
        // Función que asigna al campo de texto de la escena el correo electrónico
        // del usuario para que este vea el correo registrado y lo pueda editar.
        nuevoCorreo.text = DatosUsuario.correo;
        
    }

}
