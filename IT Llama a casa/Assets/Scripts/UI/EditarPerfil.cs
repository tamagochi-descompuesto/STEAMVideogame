using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json; //jSON CONVERT
using UnityEngine.UI;
using UnityEngine.Networking;

/*
* Permite editar la contraseña y el correo del jugador
* Autores: Jacqueline Zavala y Erick Hernández
*/
public class EditarPerfil : MonoBehaviour
{
    public InputField nuevaPassword;
    public InputField confirmaNuevaPassword;
    public InputField nuevoCorreo;
    public Text textoResultado;

    //Encapsular datos JSON
    public struct datosJugador{
        public string password;
        public string correo;
        public string username;
    }
    private datosJugador datos;
    private IEnumerator enviarDatosEditarPerfil()
    {
        // Función que permite enviar los datos producto de la edición del perfil
        // Los datos editables son la contraseña y el correo electrónico
        print("A" + nuevaPassword.text + "A");
        if (nuevaPassword.text != "")
        {
            if (nuevaPassword.text == confirmaNuevaPassword.text)
            {
                datos.password = nuevaPassword.text;
            }
            else
            {
                textoResultado.text = "Las contraseñas no coinciden";
                yield return new WaitForSeconds(10);
                textoResultado.text = "";
            }

        }

        datos.correo = nuevoCorreo.text;
        datos.username = DatosUsuario.username;
        
        //Encapsular los datos que suben a la red
        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datos));
        UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/jugador/editarPerfil",forma);
        yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
        if (request.downloadHandler.text == "success") // 200
        {
            DatosUsuario.correo = nuevoCorreo.text;
            textoResultado.text = "Información actualizada satisfactoriamente ";
            yield return new WaitForSeconds(10);
            textoResultado.text = "";
            nuevaPassword.text = "";
            confirmaNuevaPassword.text = "";
        }
        else
        {
            print(request.downloadHandler.text);
            textoResultado.text = "Hubo un error verifica la información";
            yield return new WaitForSeconds(10);
            textoResultado.text = "";
        }
    }    
    public void ActualizarPerfil()
    {
        // Método que permite comenzar la corrutina que envia los datos de Editar Perfil.
        StartCoroutine(enviarDatosEditarPerfil());
    }
}
