using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Playables;
using System;

public class MenuPause : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer cursor;

    [SerializeField]
    private Text hide_hud;

    [SerializeField]
    private Text screen;

    [SerializeField]
    private Text cursorColor;

    private GameObject panelPause;
    private GameObject panelOptions;
    private GameObject panel;
    private GameObject hud;
    private string colour;


    bool active_hud = true;
    bool active_screen = false;

    // Start is called before the first frame update
    void Start()
    {
        colour = "white";
        panelPause = GameObject.Find("MenuPause");
        panel = GameObject.Find("Menu");
        panelOptions = GameObject.Find("MenuOption");
        hud = GameObject.Find("HUD");
        try
        {
            panelOptions.SetActive(false);
        }catch(NullReferenceException e)
        {

        }
    }

    public void Resume()//Quita el menu y vuelve el juego
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
        Cursor.visible = false;
    }

    public void OptionsMenu()//Activa el menu de las opciones y desactiva el de pausa
    {
        panel.SetActive(false);
        panelOptions.SetActive(true);

    }

    public void HideHUD()//cambia el valor que muestra el HUD
    {
        if (active_hud == true)
        {
            hud.SetActive(false);

            hide_hud.text = "Off";
            active_hud = false;
        }
        else
        {
            hud.SetActive(true);
            hide_hud.text = "On";
            active_hud = true;
        }

    }

    public void changeScreen()//cambia la pantalla entre modo ventana y ventana completa
    {

        if (active_screen == false)
        {
            Screen.SetResolution(1024, 768, false);
            screen.text = "On";
            active_screen = true;
        }
        else
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
            screen.text = "Off";
            active_screen = false;
        }

    }

    public void changeColorCursor()//cambia el color del cursor
    {
        switch (colour)
        {
            case "white":
                cursor.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
                cursorColor.color = new Color(255, 0, 0, 1);
                colour = "red";
                break;
            case "red":
                cursor.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 1);
                cursorColor.color = new Color(0, 0, 255, 1);
                colour = "blue";
                break;
            case "blue":
                cursor.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 1);
                cursorColor.color = new Color(0, 255, 0, 1);
                colour = "green";
                break;
            case "green":
                cursor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 1);
                cursorColor.color = new Color(255, 255, 0, 1);
                colour = "yellow";
                break;
            case "yellow":
                cursor.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                cursorColor.color = new Color(0, 0, 0, 1);
                colour = "black";
                break;
            case "black":
                cursor.GetComponent<SpriteRenderer>().color = new Color(255, 0, 255, 1);
                cursorColor.color = new Color(255, 0, 255, 1);
                colour = "pink";
                break;
            case "pink":
                cursor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                cursorColor.color = new Color(255, 255, 255, 1);
                colour = "white";
                break;
        }
    }

    public void BackPauseMenu()//Activa el menu de pausa y desacctiva el de opciones
    {
        panelOptions.SetActive(false);
        panel.SetActive(true);

    }

    public void BackMainMenu()//Carga el menu inicial - Cambia de escena
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()//Reinicia el juego - Reinicia la misma escena
    {
        AudioSave.minutes = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
