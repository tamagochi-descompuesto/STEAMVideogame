using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Permite que el ShieldDroid detecte que golpeo al jugador y le resta una vida
Autor: Erick Hernández Silva
*/
public class ataqueShieldDroid : MonoBehaviour
{
    private Rigidbody2D rb2d;             //Rigidbody del personaje, físicas
    private Animator anim;                //Animator del personaje, animaciones
    private SpriteRenderer sprRenderer;   //Sprite Renderer del personaje, orientación
    public float tiempoInvulnerable;
    public SpriteRenderer sprJugador;
    private Vector3 posicionInicial;
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
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1.5f,gameObject.transform.position.z);
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("JugadorEnRango",detectaJugador.jugadorDetectado);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Función que se ejecuta en cuanto el personaje hace contacto con otro Collider, indicando que está en el piso
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
        else if(other.gameObject.tag == "Transportador")
        {
            
           gameObject.transform.position = posicionInicial;
        }
    }
}
