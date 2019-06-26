using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource musicGame;
    [SerializeField]
    private AudioClip music1;
    // Start is called before the first frame update
    void Start()
    {
        //Inicializa la música.
        musicGame = GetComponent<AudioSource>();
        musicGame.clip = music1;
        musicGame.Play();

    }
    public float getMin()
    {
        return musicGame.time;
    }
}
