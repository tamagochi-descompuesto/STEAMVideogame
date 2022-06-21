using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite cargar el progreso del jugador en el minijuego
Autor: Erick Hernández Silva
*/
public class CargarJugador : MonoBehaviour
{
    public static bool piezaIBT;
    public static bool piezaITC;
    public static int puntuacionGlobal;
    // Start is called before the first frame update
    private void Awake()
    {
        //Revisa la ultima posicion de guardado del jugador
        float ultimaPosicionX = PlayerPrefs.GetFloat("ultimaPosicionX",-8.67f);
        float ultimaPosicionY = PlayerPrefs.GetFloat("ultimaPosicionY",1.13f);
        //Transporta al jugador a esa posicion de guardado
        gameObject.transform.position = new Vector2(ultimaPosicionX,ultimaPosicionY);
        //Revisa que minijuegos ya terminamos satisfactoriamente.
        if(PlayerPrefs.GetInt("piezaIBT",0) == 0){
            piezaIBT = false;
        }
        else{
            piezaIBT = true;
        }
        if(PlayerPrefs.GetInt("piezaITC",0) == 0){
            piezaITC = false;
        }
        else{
            piezaITC = true;
        }
        EstadoPJ.instance.vidas = PlayerPrefs.GetInt("vidas",3);
        HUD.instance.ActualizarVidas();
        HUD.instance.ActualizarPiezas();
    }
    
}
