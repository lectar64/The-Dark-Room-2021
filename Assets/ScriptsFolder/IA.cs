using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IA : MonoBehaviour
{
    public static bool AgentBool;// True o false 
    public NavMeshAgent Agent;
    public GameObject Player;
    private Animator IAanimator;
    private float AgentSpeed = 400;
    public int distancia = 10;
    public  enum State {Activado,Desactivado}// Añade Dos Estados 
    public static State StateIA;
    public AudioClip JumpsCares;
    private float Times = 4,tr;

    private void Awake()
    {
        AgentBool = true;
        StateIA = State.Desactivado;
        IAanimator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Timer.Times <= 0 && AgentBool == true)
        {
            StateIA = State.Activado;
        }
        else
        {
            StateIA = State.Desactivado;
        }
        IABehaviour();
        if (AgentBool != true)
        {
            tr += Time.deltaTime;
            if (tr > Times)
            {
                Pause.Canvaslose.SetActive(true);
            }
        }
    }
    public void IABehaviour()
    {
        if (StateIA == State.Activado)
        {
            Agent.GetComponent<NavMeshAgent>().enabled = true;
            IAanimator.SetBool("State", true);
            Agent.speed = AgentSpeed * Time.deltaTime;
            Agent.SetDestination(Player.transform.position);
            Agent.transform.LookAt(Player.transform.position);
            Agent.Resume();
            Debug.ClearDeveloperConsole();
        }
        else if (StateIA == State.Desactivado)
        {
            Agent.speed = 0;
            IAanimator.SetBool("State", false);
            Agent.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
    public bool AudioJumpScare(bool Jumps)
    {
        if (Jumps)
        {
            AudioSource AudioObj;
            AudioObj = gameObject.GetComponent<AudioSource>();
            AudioObj.PlayOneShot(JumpsCares);
            return Jumps;
        }
        else
        {
            return Jumps;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        AgentBool = false;
        AudioJumpScare(true);
    }
}
