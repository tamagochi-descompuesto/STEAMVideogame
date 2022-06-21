using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Permite que la cámara siga al personaje sin salirse de los límites
Autor: David Rodríguez Fragoso, Edna Jacqueline Zavala Ortega,
Erick Alberto Bustos Cruz, Erick Hernández Silva, Israel Sánchez Miranda
*/

public class MovCamara : MonoBehaviour
{
    //VARIABLES
    public GameObject personaje;

    void Update()
    {
        //Función que se manda a llamar durante cada frame, le pasa las coordenadas del personaje a la cámara para que lo siga
        //Se obtienen los valores que se le darán a la cámara
        float x = Mathf.Clamp(personaje.transform.position.x, -253.2f, 26.14f);
        float y = Mathf.Clamp(personaje.transform.position.y, -31.5f, 2.35f);
        float z = transform.position.z;

        //Se le mandan los valores a la cámara
        transform.position = new Vector3(x, y, z);
    }
}
