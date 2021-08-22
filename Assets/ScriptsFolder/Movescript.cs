using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private float Horizontal;
    private float Vertical;
    private float speedWalk = 2.5f;
    private float speedRun = 7;
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
        MainCam = FindObjectOfType<Camera>().GetComponent<Camera>();
        Hand = false;
        Flash = false;
        OnCamPos = true;
        FlashLight = gameObject.GetComponentInChildren<Light>();
        FlashLight.enabled = Flash;
    }
    private void Update()
    {
        MouseLocked();        
        Movement();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync("Room-2");
        }
    }
    public void Movement()
    {  if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A))
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "House")
        {
           FlashLightCns = false;
           return;
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
}
