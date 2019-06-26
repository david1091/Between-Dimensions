using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mostrarSala : MonoBehaviour
{

    public SpriteRenderer dark;
    float decremento = 1;
    float temporizador = 1;
    bool enter = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        dark.GetComponent<SpriteRenderer>().enabled = true;//activa las zonas oscuras
    }

    // Update is called once per frame
    void Update()
    {
        if (enter == true)
        {//inicia un temporizador en el que se ejecuta la animacion de mostrar
            temporizador -= Time.deltaTime * 20;
            if (temporizador < 0)
            {
                transicion();
                temporizador = 1;
            }
        }
    }
    void transicion()
    {
        decremento -= 0.1f;
        dark.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, decremento);//va cambiando el translucido de la sala

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//comprueba que el objeto que lo activa sea el jugador
        {
            enter = true;
        }
    }
}
