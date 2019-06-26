using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//coge los valores de la animacion
    }

    // Update is called once per frame
    public  IEnumerator ShowArea(string name)
    {
        anim.Play("Area_Show");//inicia la animacion de mostrar
        transform.GetChild(0).GetComponent<Text>().text = name;
        transform.GetChild(1).GetComponent<Text>().text = name;
        //cambia el texto a los hijos

        yield return new WaitForSeconds(1f);//hace una espera de un segundo
        anim.Play("Area_FadeOut");//inicia la animacion de desaparecer

    }

}
