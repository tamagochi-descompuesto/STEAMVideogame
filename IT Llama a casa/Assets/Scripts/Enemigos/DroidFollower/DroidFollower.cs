using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que el DroidFollower detecte si el jugador está en el rango de visión y 
persigue al jugador si lo está. Sino, regresa a su lugar de origen.
Además activa la animaicción de ataque cuando esté en rango
Autor: Erick Hernández Silva
*/
public class DroidFollower : MonoBehaviour
{
    private Animator anim;   
    public float radioVision;
    public int velocidad;
    private Rigidbody2D rigidbody;
    GameObject jugador;
    private Vector2 posicionInicial;
    public SpriteRenderer sprJugador;   //Sprite Renderer del personaje, orientación
    public float tiempoInvulnerable;
    //Da invulnerabilidad al jugador
    private IEnumerator Invulnerabilidad()
    {
        EstadoPJ.instance.invulerable = true;
        yield return new WaitForSeconds (tiempoInvulnerable);
        EstadoPJ.instance.invulerable = false;
    }
    //Simula un "blink" al ser atacado el jugador
    private IEnumerator animAtacado()
    {
        sprJugador.sortingOrder = -1000;
        yield return new WaitForSeconds (tiempoInvulnerable/3);
        sprJugador.sortingOrder = 0;
        yield return new WaitForSeconds (tiempoInvulnerable/3);
        sprJugador.sortingOrder = -1000;
        yield return new WaitForSeconds (tiempoInvulnerable/3);
        sprJugador.sortingOrder = 0;
        
    }
    //Cuando el jugador entra al collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")   //Si el Collider no es de una pieza entonces el personaje está en el piso
        {
            if(!EstadoPJ.instance.invulerable)
            {
                EstadoPJ.instance.vidas--;
                HUD.instance.ActualizarVidas();
                StartCoroutine(Invulnerabilidad());
                StartCoroutine(animAtacado());
            }
        }
        //Si entra al transportador
        else if(other.gameObject.tag == "Transportador")
        {
            //Lo regresa a su posicion inicial
           RegresarPosicionInicial();
        }
    }
    public void RegresarPosicionInicial(){
        gameObject.transform.position = posicionInicial;
    }
    void Start()
    {
        posicionInicial = transform.position;
        //obtenemos el gameobject del jugador
        jugador = GameObject.FindGameObjectWithTag("Player");
        //rigidbody del enemigo
        rigidbody = GetComponent<Rigidbody2D>();
        posicionInicial = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1.5f,gameObject.transform.position.z);
        anim = GetComponent<Animator>();  
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
            //Si la distancia entre el jugador y el enemigo es menor a 2, cambia la animacion
            if(distancia<2){
                anim.SetBool("JugadorEnRango",true);
            }//Si sale del rango de ataque, lo deja
            else{
                anim.SetBool("JugadorEnRango",false);
            }
        }
        else{
            destino = posicionInicial;
            //print("Jugador fuera de rango de vision");
        }
        //Obtenemos una velocidad corregida usando el tiempo entre frames
        float velocidadCorregida = velocidad * Time.deltaTime;
        //movemos al enemigo hacia el jugador
        transform.position = Vector2.MoveTowards(transform.position,destino,velocidadCorregida);
        
    }
}
