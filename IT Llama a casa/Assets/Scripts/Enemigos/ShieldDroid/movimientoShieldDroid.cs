using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que el ShieldDroid detecte si el jugador está en el rango de visión y 
persigue al jugador si lo está. Sino, regresa a su lugar de origen.
Autor: Erick Hernández Silva
*/
public class movimientoShieldDroid : MonoBehaviour
{
    public float radioVision;
    public int velocidad;
    private Rigidbody2D rigidbody;
    GameObject jugador;
    private Vector2 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        //obtenemos el gameobject del jugador
        jugador = GameObject.FindGameObjectWithTag("Player");
        //rigidbody del enemigo
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //hacia donde nos moveremos
        Vector2 destino = posicionInicial;
        //obtenemos la distancia que hay entre el jugador y el enemigo
        float distancia = Vector2.Distance(jugador.transform.position,transform.position);
        //si está dentro del rango sobreescribiremos el destino
        if(distancia<radioVision){
            destino = jugador.transform.position;
            //print("Jugador en rango de vision");
        }
        else{
            destino = posicionInicial;
            //print("Jugador fuera de rango de vision");
        }
        //Obtenemos una velocidad corregida usando el tiempo entre frames
        float velocidadCorregida = velocidad * Time.deltaTime;
        //movemos al enemigo hacia el jugador
        transform.position = Vector2.MoveTowards(transform.position,destino,velocidadCorregida);
        //Si se detecta una pared, el enemigo saltará para intentar sortearla
        if(detectaPared.paredDetectada) rigidbody.velocity = new Vector2(rigidbody.velocity.x, 6);
        //print(rigidbody.velocity.y.ToString());
    }
}
