using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json; //jSON CONVERT
/*
Despliega el top 10 de puntuaciones en las partidas
Autor: Erick Hernández Silva
*/
public class MejoresPuntuaciones : MonoBehaviour
{
    //Textbox donde se desplegara la lista de jugadores
    public Text textJugadores;
    //Textbox donde se desplegara la lista de puntuaciones
    public Text textPuntuaciones;
    void Awake()
    {
        StartCoroutine(ObtenerPuntuaciones());

    }

    // Manda una peticion al servidor web y despliega el TOP 10
    private IEnumerator ObtenerPuntuaciones(){
        //Encapsular los datos que suben a la red
        UnityWebRequest request = UnityWebRequest.Get("http://18.116.89.34:8080/partida/mejoresPuntuaciones");
        yield return request.SendWebRequest(); //Regresa, ejecuta y espera....
        //el servidor responde con un string formateado con los datos dle top 10
        if (request.downloadHandler.text != "error"){// 200
            String registros = request.downloadHandler.text;
            //se guarda cada registro en un array
            string[] jugadores = registros.Split('|');
            string topJugadores = "JUGADOR \n \n";
            string topPuntuaciones = "PUNTOS \n \n";
            //comenzamos a concatenar en cada string para posteriormente
            //desplegar en los textbox
            for (int x = 0; x < jugadores.Length-1; x+=2)
            {
                topJugadores += jugadores[x] + "\n";
                topPuntuaciones += jugadores[x+1] + "\n";

            }
            //se despliega el texto concatenado
            textPuntuaciones.text = topPuntuaciones;
            textJugadores.text = topJugadores;
        }
        else{
            //si ocurre un error, se despliega este mensaje
            textJugadores.text = "Ocurrió un error :(";
            textPuntuaciones.text= "Vuelve a intentarlo";
        }
    }

}
