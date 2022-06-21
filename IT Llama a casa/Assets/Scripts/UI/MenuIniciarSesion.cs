using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking; //Para red. UnityWebRequest
using Newtonsoft.Json; //jSON CONVERT

/*
Permite darle funcionalidades a los componentes del menú
Autores: Edna Jacqueline Zavala Ortega y Erick Hernández Silva
*/

public class MenuIniciarSesion : MonoBehaviour
{
    //Campos con la información nombre y contraseña
    public InputField textoUsername;
    public Text textoPassword;
    public InputField inputPassword;
    public Text textoError;

    //Encapsular datos JSON
    public struct datosJugador{
        public string username;
        public string password;
    }
    private datosJugador datos; // creamos una variable de tipo datosJugador
    public void Regresar()
    {
        // Método que le da funcionalidad para regresar al menú inicial.
        SceneManager.LoadScene("MenuInicial");
    }

    private IEnumerator enviarDatosInicioSesion()
    {
        // Método que permite enviar a la aplicación web los datos de inicio de sesión y validarlos.
        datos.username = textoUsername.text;
        datos.password = inputPassword.text;
        //Encapsular los datos que suben a la red
        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datos));
        // Se envía la petición.
        UnityWebRequest request = UnityWebRequest.Post("http://18.116.89.34:8080/jugador/iniciarSesion", forma);
        yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
        if (request.downloadHandler.text != "failed"){// Si la petición es exitosa
             // Se procesa la respuesta para desplegarla en la escena.
            var datos = request.downloadHandler.text;
            Dictionary<string, string> datosJugador =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(datos);
            DatosUsuario.username = datosJugador["username"];
            DatosUsuario.correo = datosJugador["correo"];
            DatosUsuario.idPartida = Int32.Parse(datosJugador["idPartida"]);
            textoError.text = "Bienvenid@ " + textoUsername.text;
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("MenuPrincipal");
            textoUsername.text = "";
            textoPassword.text = "";
        }
        else
        {
            // Si el usuario es incorrecto, se despliega el mensaje en la pantalla durante unos cuantos segundos.
            textoError.text = "Usuario o contraseña incorrectos";
            yield return new WaitForSeconds(5);
            textoError.text = "";
            textoUsername.text = "";
            textoPassword.text = "";
        }
    }
    public void IniciarSesion()
    {
        // Método que le da la funcionalidad al menú para iniciar la co-rutina de Iniciar Sesión.
        StartCoroutine(enviarDatosInicioSesion());
    }
}
