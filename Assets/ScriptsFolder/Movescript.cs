using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BedCameraUtilities;

public class Movescript : MonoBehaviour
{
    static GameObject [] csObjects = new GameObject[1];
    public Camera MainCam;
    // Mouse
    private float X;
    private float Y;
    private float speed = 85;
    private float Limit;
    //KeyBoard
    [SerializeField] float Horizontal;
    [SerializeField] private float Vertical;
    [SerializeField] private float speedWalk = 2.5f;
    [SerializeField] private float speedRun = 7;
    private Rigidbody Rb;
    float Value;
    public bool ItsRunning;
    // Raycast
    private Ray rayo;
    public static RaycastHit Golpe;
    // UI
    public Image Crosshair;
    // Pickable
    private Rigidbody rb;
    public bool Hand;
    private GameObject Pos;
    int Pickable;
    public Animator Animator;
    private static Light FlashLight;
    public static bool Flash;
    public static bool OnCamPos;
    //Materials Selector
    public Material DefaultMaterial;
    public Material ChangeMaterial;
    private Renderer RenderMaterial;
    public static bool FlashLightCns, OnPlayermove;
    private Outline[] RenderLines;
    [SerializeField] private string [] tags = {"Key","FlashLight"};
    public static bool OnAnimationStart;
    [SerializeField] private Canvas [] PlayerObjects;
    private void Awake()
    {
        csObjects[0] = GameObject.Find("Linterna_3DS");
        for (int i = 0; i < csObjects.Length; i++)
        {
            csObjects[i].SetActive(false);
        }
        Application.targetFrameRate = 144;
        Pos = GameObject.Find("Posicionador");
        Rb = gameObject.GetComponent<Rigidbody>();
        MainCam = this.gameObject.GetComponentInChildren<Camera>();
        Hand = false;
        Flash = false;
        OnCamPos = true;
        FlashLight = gameObject.GetComponentInChildren<Light>();
        FlashLight.enabled = Flash;
        RenderLines = FindObjectsOfType<Outline>();
        for (int i = 0; i < RenderLines.Length; i++)
        {
            RenderLines[i].enabled = false;
        }
        PlayerObjects = this.gameObject.GetComponentsInChildren<Canvas>();
    }
    private void Update()
    {
        AnimationStarts();
        MouseLocked();        
        Movement();
        RenderLines = FindObjectsOfType<Outline>();
    }
    public void AnimationStarts()
    {
        if (OnAnimationStart)
        {
            MainCam.enabled = false;
            for (int i = 0; i < PlayerObjects.Length; i++)
            {
                PlayerObjects[i].enabled = false;
            }
        }
        else
        {
            MainCam.enabled = true;
            for (int i = 0; i < PlayerObjects.Length; i++)
            {
                PlayerObjects[i].enabled = true;
            }
        }
    }
    public void Movement()
    {
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("Walk", true);
            Animator.SetBool("Back", false);
            Animator.SetBool("Right", false);
            OnPlayermove = true;
        }
        else if (!Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A)||!Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("Back", false);
            Animator.SetBool("Walk", false);
            OnPlayermove = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("Walk", false);
            Animator.SetBool("Back", true);
            Animator.SetBool("Right", false);
            OnPlayermove = true;
        }
        if (OnPlayermove !=true)
        {
            transform.Translate(0, 0, 0);
        }
        rayo.origin = MainCam.transform.position;
        rayo = MainCam.ScreenPointToRay(Input.mousePosition);
        rayo.direction = MainCam.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(rayo.origin, rayo.direction * 5);
        if (Physics.Raycast(rayo,out Golpe,2))
        {
            CrosshairStates();
            RenderMethod();
        }
        else
        {
            Crosshair.color = Color.white;
        }
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Limit = Mathf.Clamp(Limit, -90, 90);
        Limit -= Y*speed*Time.deltaTime;
        if (OnCamPos)// si no esta en pausa la camara se sigue moviendo 
        {
            X = Input.GetAxis("Mouse X");
            Y = Input.GetAxis("Mouse Y");
        }
        else
        {
            X = 0;// La camara Eje X no se mueve
            Y = 0;// La camara Eje Y no se mueve
        }     
        gameObject.transform.Rotate(0, X * speed * Time.deltaTime,0);
        MainCam.transform.localRotation = Quaternion.Euler(Limit, 0, 0);
        Rb.transform.Translate(Horizontal*Time.deltaTime*speedWalk, 0, Vertical * Time.deltaTime * speedWalk);
        if (TextSizer.Condition)
        {
            Crosshair.enabled = false;
        }
        else
        {
            Crosshair.enabled = true;
        }
        FlashLight.colorTemperature = 0.5f;
    }
    public static void OnFlashLightCondition()
    {
        while (Flash != true)    
        {
            int i = 0;
            csObjects[i].SetActive(true);
            Flash = true;
            FlashLight.enabled = Flash;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        FlashLightCns = true;
        return;
    }
    public void Linterna()
    {
        FlashLight.enabled = Flash;
        if (Input.GetKeyDown(KeyCode.F) && FlashLightCns)
        {
            Flash = !Flash;          
        }
    }
    public void MouseLocked()
    {
        if (Pause.OnPause == true || IA.AgentBool !=true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }       
    }
    public void CrosshairStates()
    {
        if (Golpe.collider.gameObject.tag == "Puerta"&& Casting.Llave == true || Golpe.collider.tag == "Cube" || Golpe.collider.gameObject.tag == "Cajon" || Golpe.collider.gameObject.tag == "Boton" || Golpe.collider.gameObject.tag == "Key"|| Golpe.collider.gameObject.tag == "Armario")
        {
            Crosshair.color = Color.blue;
       
        }
        else if (Golpe.collider.gameObject.tag == "Puerta" && Casting.Llave == false)
        {
            Crosshair.color = Color.red;
        }
        else
        {
            Crosshair.color = Color.white;
        }
    }

    public void RenderMethod()
    {
        for (int i = 0; i < RenderLines.Length; i++)
        {
            if (RenderLines[i] != null)
            {
                if (Golpe.collider.CompareTag("Key") || Golpe.collider.CompareTag("FlashLight") || Golpe.collider.CompareTag("Puerta") && Casting.Llave != true || Golpe.collider.CompareTag("Cer") || Golpe.collider.CompareTag("Cer"))
                {
                    Outline V;
                    V = Golpe.collider.gameObject.GetComponent<Outline>();
                    V.enabled = true;
                }
                else if(Golpe.collider.CompareTag("Puerta") && Casting.Llave)
                {
                    Outline V;
                    V = Golpe.collider.gameObject.GetComponent<Outline>();
                    V.OutlineColor = Color.green;
                    V.enabled = true;
                }
                else if (Golpe.collider.CompareTag("DoorAnimation"))
                {
                    Outline V;
                    V = Golpe.collider.gameObject.GetComponent<Outline>();
                    V.OutlineColor = Color.red;
                    V.enabled = true;
                }
                else
                {
                    for (int j = 0; j < RenderLines.Length; j++)
                    {
                        RenderLines[i].enabled = false;
                    }
                }         
            }
        }
    }
}