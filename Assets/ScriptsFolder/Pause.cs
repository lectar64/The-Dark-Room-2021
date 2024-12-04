using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    //Codigo Incompleto
    public static GameObject BedroomCamera;
    public Text TextCers;
    public static bool OnPause;
    private GameObject PauseObJ;
    public static GameObject Canvaslose;
    public static CanvasManagement Instance;
    public bool IfInstanceNull;
    public GameObject PrefabPause;
    private TextSizer Txg;
    public AudioClip JumpsCares;
    public static GameObject CanvasDie;
    private GameObject[]Cers;
    [SerializeField] private bool OnMissionLayer;
    public Animator MissionLayer;
    public static int Cer {get; set;}
    private Animation AnimationFinal;
    private void Awake()
    {
        PauseObJ = GameObject.Find("Pause");
        OnPause = false;
        Txg = FindObjectOfType<TextSizer>();
        Canvaslose = GameObject.Find("CanvasLose");
        Canvaslose.SetActive(false);
        CanvasDie = GameObject.Find("CanvasDie");
        CanvasDie.SetActive(false);
        Cers = GameObject.FindGameObjectsWithTag("Cer");
    }

    private void Start()
    {
        TextCers.CrossFadeAlpha(0,0.96f,true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Movescript.OnAnimationStart == false)
        {
            OnPause = !OnPause;//el booleano tomara el valor contrario a su valor original     
        }

        if (Input.GetKeyDown(KeyCode.Q) && OnPause != true)
        {
            OnMissionLayer = !OnMissionLayer;
            MissionLayer.SetBool("OnMissionLayer",OnMissionLayer);
        }
        if (Casting.Llave)
        {
            string cs;
            cs = Cers.Length.ToString();
            TextCers.CrossFadeAlpha(1,0.96f,true);
            TextCers.text = Cer+"/"+ Cers.Length;
        }
        if (Cer == Cers.Length)
        {
            TextCers.CrossFadeAlpha(0, 2f, true);
            Movescript.OnAnimationStart = true;
            AnimationFinal.Play();
        }
         OnPauseGame();
    }
    public void OnPauseGame()//Pausa
    {
        if (OnPause && IA.AgentBool != false)//Si esta pausado el juego y el booleano que determina el movimiento del enemigo es verdadero, se iniciara el pausa
        {
            PauseObJ.SetActive(true);
            Time.timeScale = 0;
            Movescript.OnCamPos = false;
            Txg.gameObject.SetActive(false);
        }
        else if (OnPause == false && IA.AgentBool != false)//Si es falso el booleano de pausa el juego unicamente desactivara el pausa
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
