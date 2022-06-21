using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Script que contiene la matriz de botones del jugador y sus funciones
* Autores: Jacqueline Zavala e Israel Sánchez
*/

public class MatrizJugador : MonoBehaviour
{
    //VARIABLES
    public Button boton1;            //Botones que conforman la matriz
    public Button boton2;
    public Button boton3;
    public Button boton4;
    public Button boton5;
    public Button boton6;
    public Button boton7;
    public Button boton8;
    public Button boton9;
    public Button boton10;
    public Button boton11;
    public Button boton12;
    public Button boton13;
    public Button boton14;
    public Button boton15;
    public Button boton16;
    public Sprite noColor;           //Imagen de reseteo del color de los botones de la matriz         
    public static Button[,] matriz;  //Matriz de botones del jugador

    //MÉTODOS
    void Start()
    {
        //Función que se ejecuta antes del primer frame
        //Se crea la matriz de botones metiéndole los botones del microarreglo
        matriz = new Button[,]{{boton1, boton2, boton3, boton4}, {boton5, boton6, boton7, boton8}, {boton9, boton10, boton11, boton12}, {boton13, boton14, boton15, boton16}};
    }

    public void ResetearColor()
    {
        //Función que resetea los colores de la matriz asignándoles un sprite sin color
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                //Se recorre toda la matriz
                matriz[i, j].image.sprite = noColor; //Se cambia el sprite de cada celda por uno sin color
            }
        }
    }
}
