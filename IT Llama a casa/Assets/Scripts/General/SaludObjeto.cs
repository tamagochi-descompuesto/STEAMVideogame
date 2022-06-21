using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script que le proporciona sus comportamientos a los objetos que curarán la vida del personaje
Autor: Israel Sánchez Miranda
*/

public class SaludObjeto : MonoBehaviour
{
    //MÉTODOS
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta si la pieza colisiona con otro Collider
        if(other.gameObject.CompareTag("Player"))
        {
            //Solamente si tenemos menos de 3 vidas se podrá usar el objeto curativo
            if(EstadoPJ.instance.vidas < 3)
            {
                //Si el game object que recolecta la moneda es el jugador entonces:
                GetComponent<SpriteRenderer>().enabled = false;   //Se deja de renderizar la pieza

                //Se destruye el objeto
                Destroy(gameObject, 0.4f);

                //Se restaura y actualiza la vida del jugador
                EstadoPJ.instance.vidas += 1;
                HUD.instance.ActualizarVidas();
            }
            
        }
    }
}
