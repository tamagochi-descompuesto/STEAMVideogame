using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Permite desplegar las instrucciones de nuevo
 * Autor: Erick Bustos
 */
public class DesplegarInstrucciones : MonoBehaviour
{
    // Referencia al primer panel de las instrucciones
    public GameObject panelInstrucciones;

    // Se despliega el primer panel de las instrucciones dando click en el botón con "?"
    public void DespliegaInstrucciones()
    {
        panelInstrucciones.SetActive(true);
    }
}
