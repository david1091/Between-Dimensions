using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    //Para almacenar el punto de destino
    public GameObject target;
    //Para controlar si empieza o no la transicion
    bool start = false;
    //Para controlar si la transicion es de entrada o salida
    bool isFadeIn = false;
    //Opacidad inicial del cuadrado de transicion
    float alpha = 0;
    //Transicion de un segundo
    float fadeTime = 1f;
    //Cambio Música
    [SerializeField]
    private MusicManager musicManager;
    [SerializeField]
    private AudioClip music;



    GameObject area;


    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        area = GameObject.FindGameObjectWithTag("Area");

    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            other.GetComponent<Animator>().enabled = false;
            other.GetComponent<PlayerController>().enabled = false;

            //Inicia la transicion
            FadeIn();

            //Hace una espera de un segundo antes de iniciar la segunda fase de la transicion
            yield return new WaitForSeconds(fadeTime);

            //Actualiza la posicion
            other.transform.position = target.transform.GetChild(0).transform.position;

            FadeOut();
            other.GetComponent<Animator>().enabled = true;
            other.GetComponent<PlayerController>().enabled = true;

            //iniciar una corutina para mostrar el nombre de la zona
            StartCoroutine(area.GetComponent<Area>().ShowArea(target.name));

            //Aztualmiza la música dependiendo de la sala
            musicManager.changeLoop(true);
            musicManager.changeMusic(target.GetComponent<Warp>().music);
        }

    }

    //Dibuja un cuadrado con opacidad encima de la pantalla simulando la transicion
    void OnGUI()
    {

        //Si no empieza la transicion sale del evento directamente
        if (!start)
            return;

        //Si ha empezado crea un color con opacidad inicial de 0
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        //Crea una textura temporal para rellenar la pantalla
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        // Dibuja la textura sobre la pantalla
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        // Controlar la transparencia
        if (isFadeIn)
        {
            //Si aparece aumenta la opacidad
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            //Si desaparece disminuye la opacidad
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);

            //Si la opacidad llega a 0 finaliza la transicion
            if (alpha < 0) start = false;
        }
    }

    //metodo para activar la transicion de entrada
    void FadeIn()
    {
        start = true;
        isFadeIn = true;
    }

    //metodo para activar la transicion de salida
    void FadeOut()
    {
        isFadeIn = false;
    }
}
