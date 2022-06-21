using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Detecta la colisión del jugador con la "lava"" del parkour. 
Regresa al jugador al inicio del parkour
Autor: Erick Hernández Silva
*/
public class ParkourCaida : MonoBehaviour
{
    //VARIABLES
    public float x;  //Posición del checkpoint del jugador
    public float y;
    public DroidFollower droid;

    //MÉTODOS
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta si la "lava" colisiona con otro Collider
        if(other.gameObject.CompareTag("Player"))
        {
            //Si el jugador entra en contacto con el collider se manda al jugador al checkpoint y se le resta una vida
            other.transform.position = new Vector2(x, y);
            EstadoPJ.instance.vidas--;
            HUD.instance.ActualizarVidas();
            droid.RegresarPosicionInicial();
        }
    }
}
