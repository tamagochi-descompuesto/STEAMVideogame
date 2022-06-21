using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que un enemigo se mueva en el eje x,y o ambos.
Autor: Erick Hernández Silva
*/
public class movimientoEnemigo : MonoBehaviour
{
    public bool destinoAlcanzado = false;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprRenderer;
    public float velocidad;
    private Vector2 posicionInicial;
    public float x;
    public float y;
    public bool flip;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        //obtenemos el gameobject del jugador
        //rigidbody del enemigo
        rigidbody = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //hacia donde nos moveremos
        Vector2 destino = new Vector2(x,y);
        //Obtenemos una velocidad corregida usando el tiempo entre frames
        float velocidadCorregida = velocidad * Time.deltaTime;
        if(!destinoAlcanzado){
            if(flip){
                sprRenderer.flipX = false;
            }            
            //Movemos al destino
            transform.position = Vector2.MoveTowards(transform.position,destino,velocidadCorregida);    
            //Si llegamos a la ubicación destino, nos vamos a la posición inicial
            if(transform.position.x == destino.x && transform.position.y == destino.y){
                destinoAlcanzado = true;
            }
        }
        if(destinoAlcanzado){
            if(flip){
                sprRenderer.flipX = true;
            }
            //movemos al enemigo hacia la posicion inicial
            transform.position = Vector2.MoveTowards(transform.position,posicionInicial,velocidadCorregida);
            //Si llegamos a la posicion inicial, vamos al destino
            if(transform.position.x == posicionInicial.x && transform.position.y == posicionInicial.y){
                destinoAlcanzado = false;
            }
        }
    }
}
