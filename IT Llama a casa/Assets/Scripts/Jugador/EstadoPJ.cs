using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Mantiene información del estado del personaje (salud y piezas recolectadas)
Autores: David Rodríguez Fragoso, Edna Jacqueline Zavala Ortega, 
Erick Alberto Bustos Cruz, Erick Hernández Silva, Israel Sánchez Miranda
*/

public class EstadoPJ : MonoBehaviour
{
    //VARIABLES
    public int vidas = 3;                    //vidas del personaje
    public int piezas = 0;                  //Piezas recolectadas por el personaje
    public bool invulerable = false;
    
    public static EstadoPJ instance; //Referencia a la clase EstadoPJ
    //MÉTODOS
    private void Awake()
    {
        instance = this;    //Asignar la instancia al objeto ejecutado por el código
    }

    void Update()
    {
        //Función que se ejecuta cada frame, en este contexto evita que las vidas del jugador se excedan del límite de 3
        if(vidas > 3)
        {
            //Si las vidas superan el límite se "resetean" al valor máximo que es 3
            vidas = 3;
        }
    }
}
