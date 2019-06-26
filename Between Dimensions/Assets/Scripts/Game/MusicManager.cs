using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicGame;
    [SerializeField]
    private AudioClip music1;



    // Start is called before the first frame update
    void Start()
    {
        //Inicializa la música.
        musicGame = GetComponent<AudioSource>();
        musicGame.loop = true;
        musicGame.clip = music1;
        musicGame.time = AudioSave.minutes;
        musicGame.Play();
    }

    public void changeMusic(AudioClip clip)
    {
        AudioSave.minutes = 0;
        musicGame.clip = clip;
        musicGame.Play();
    }

    public void changeLoop(bool i)
    {
        musicGame.loop = i;
    }
}
