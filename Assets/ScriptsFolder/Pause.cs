﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    //Codigo Incompleto
    public static bool OnPause;
    private GameObject PauseObJ;
    public static GameObject Canvaslose;
    public static CanvasManagement Instance;
    public bool IfInstanceNull;
    public GameObject PrefabPause;
    private TextSizer Txg;
    public AudioClip JumpsCares;
    public static GameObject CanvasDie;
    private void Awake()
    {
        PauseObJ = GameObject.Find("Pause");
        OnPause = false;
        Txg = FindObjectOfType<TextSizer>();
        Canvaslose = GameObject.Find("CanvasLose");
        Canvaslose.SetActive(false);
        CanvasDie = GameObject.Find("CanvasDie");
        CanvasDie.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause = !OnPause;     
        }
         OnPauseGame();
    }
    public void OnPauseGame()//Pausa
    {
        if (OnPause && IA.AgentBool != false)
        {
            PauseObJ.SetActive(true);
            Time.timeScale = 0;
            Movescript.OnCamPos = false;
            Txg.gameObject.SetActive(false);
        }
        else if (OnPause == false && IA.AgentBool != false)
        {
            PauseObJ.SetActive(false);
            Time.timeScale = 1;
            Movescript.OnCamPos = true;
            Txg.gameObject.SetActive(true);
        }
        else if (OnPause == false && IA.AgentBool !=true)
        {
            CanvasDie.SetActive(true);
        }
    }
    public void Continue()
    {
        OnPause = false;
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public bool OnGameRunning()
    {
        return OnPause;
    }
}
