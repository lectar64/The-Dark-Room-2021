using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casting : MonoBehaviour
{
    public List<AudioClip>Sounds = new List<AudioClip>();// 0 Open // 1 Close
    public static int Value;
    private Movescript Script_Move;
    public Text GravityText;
    private Camera Cam;
    public Ray rayo;
    public RaycastHit Col;
    public Rigidbody ObjectsPick;
    private float OnRandomForce;
    public bool OnBoxhand;
    private GameObject Posicionador;
    bool Gravity;
    public GameObject Player;
    private bool Rotacion;
    //BoolStates
    public static bool Doorbool;
    public static bool Llave;
    public bool MuebleBool;
    public bool Armario;
    private Lightscenescript Light;
    public List<Animator> Animators = new List<Animator>();//Añade una lista no definida de Animaciones
    private void Awake()
    {
        Posicionador = GameObject.Find("Posicionador");
        Player = GameObject.Find("Player");
        Script_Move = FindObjectOfType<Movescript>().GetComponent<Movescript>();
    }
    void Start()
    {
        OnBoxhand = false;
        Cam = FindObjectOfType<Camera>();
        Cam = Cam.GetComponent<Camera>();
        OnRandomForce = Random.Range(600, 800);// Fuerza aleatoria de 600 a 800      
    }


    void Update()
    {
        OnboxHave();
        rayo.origin = Cam.transform.position;// el rayo se encuentra en la camara
        rayo.direction = Cam.transform.TransformDirection(Vector3.forward);//Direccion recta
        rayo = Cam.ScreenPointToRay(Input.mousePosition);// Rayo situado en la posicion del mouse
        if (Timer.Times <=0)
        {
            Llave = false;
            Doorbool = true;
            Animators[0].SetBool("Door", Doorbool);
        }
        if (Input.GetKeyDown(KeyCode.E) && TextSizer.Condition != true)
        {
            
            if (Physics.Raycast(rayo, out Col, 2))
            {
                if (Col.collider.tag == "Cube" || Col.collider.tag == "Sphere")
                {
                    
                    IfComponentNull();
                    OnBoxhand = !OnBoxhand;// la variable true o false se transforma en opuesta 
                    ObjectsPick = Col.collider.gameObject.GetComponent<Rigidbody>();
                    OnboxHave();
                }
                else if (Col.collider.tag == "Puerta")
                {
                    if (Llave)
                    {                        
                        Doorbool = !Doorbool;
                        Animators[0] = Col.collider.gameObject.GetComponent<Animator>();
                        Animators[0].SetBool("Door",Doorbool);                      
                    }
                }
                else if (Col.collider.tag =="Cajon")
                {
                    AudioSource DrawerSound;
                    DrawerSound = Col.collider.gameObject.GetComponent<AudioSource>();
                    if (MuebleBool != false)
                    {
                        DrawerSound.PlayOneShot(Sounds[0]); 
                    }
                    else
                    {
                        DrawerSound.PlayOneShot(Sounds[1]);
                    }
                    MuebleBool = !MuebleBool;
                    Animators[1] = Col.collider.gameObject.GetComponent<Animator>();
                    Animators[1].SetBool("State", MuebleBool);                    
                }
                else if (Col.collider.tag == "Boton")
                {
                    Light = Col.collider.gameObject.GetComponent<Lightscenescript>();
                    Light.Onlight = !Light.Onlight;
                }
                else if(Col.collider.tag == "Key"|| Col.collider.name == "Key")
                {
                    Llave = true;
                    Destroy(Col.collider.gameObject);
                    return;
                }
                else if (Col.collider.tag == "Armario")
                {
                    Animators[2] = Col.collider.gameObject.GetComponentInParent<Animator>();
                    Armario = !Armario;
                    Animators[2].SetBool("sak", Armario);
                }
                else if (Col.collider.tag == "FlashLight")
                {
                    Movescript.OnFlashLightCondition();// llama funcion
                    Destroy(Col.collider.gameObject);
                }
            }
            else
            {
                OnBoxhand = false;
            }
        }
    }
    public void OnboxHave()
    {
        if (OnBoxhand)// Podria haber puesto el RigidBody con Un GetParent Que convierte el Objeto en Padre
        {
            OntrowObject();
            //IfComponentNull();
            ObjectsPick = Col.collider.gameObject.GetComponent<Rigidbody>();
            ObjectsPick.collisionDetectionMode = CollisionDetectionMode.Continuous;
            if (Col.collider.tag == "Cube" || Col.collider.tag == "Sphere")
            {
                //IfComponentNull();// Llama al metodo IfComponentNull y comprueba el estado del componente
                Col.collider.gameObject.transform.position = Posicionador.transform.position;
                ObjectsPick.velocity = Vector3.zero;// no obtiene Velocidad osea le asigna velocidad 0;
                Col.collider.gameObject.transform.LookAt(Player.transform.position);// Asigna a la Inteligencia artificial Un punto visual que es el personaje      
                if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(rayo, out Col, 2) && OnBoxhand)// Error Solucionado ;)
                {
                    if (Col.collider.tag == "Cube" || Col.collider.tag == "Sphere")
                    {
                        ObjectsPick = null;
                    }
                }
            }
        }
        else if (OnBoxhand == false)
        {
            ObjectsPick = null;
        }
    }
    public void OntrowObject()// Tirar Objeto
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnBoxhand = false;
            ObjectsPick = Col.collider.gameObject.GetComponent<Rigidbody>();// Añade una fuerza al objeto en el que este el Raycast si la direccion del Raycast esta tocando el collider "Cubo" y ademas detecta colision este obtendra el componente del objeto con la colision y al lanzarlo utilizara ese Rigidbody
            ObjectsPick.AddForce(rayo.direction * OnRandomForce); 
        }
    }
    public void IfComponentNull()// está función comprueba si tiene o no el componente
    {
        if (Col.collider.tag == "Cube")
        {
            if (ObjectsPick == null)// Si no tiene asignada ningun componente, que obtenga el componente de la colision del Rayo
            {
              ObjectsPick = Col.collider.gameObject.AddComponent<Rigidbody>();
            }
        }      
    }
}
 