using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que el SmallMonster detecte si chocó con una pared
Autor: Erick Hernández Silva
*/
public class SmallMonsterChoque : MonoBehaviour
{
    //Variable publica estática que permite saber al small monster si chocó con pared
    public static bool SmallMonsterChoco;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Mapa")){
            SmallMonsterChoco = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Mapa")){
            SmallMonsterChoco = false;
        }
    }
}
