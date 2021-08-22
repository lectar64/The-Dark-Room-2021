using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManagement: MonoBehaviour
{
    //Codigo Incompleto
    public static bool OnPause;
    private GameObject PauseObJ;
    public static Pause Instance;
    public bool IfInstanceNull;
    public GameObject PrefabPause;
    private TextSizer Txg;
    public AudioClip JumpsCares;
    private void Awake()
    {
        if (IfInstanceNull)
        {
        }
        PauseObJ = GameObject.Find("Pause");
        OnPause = false;
        Txg = FindObjectOfType<TextSizer>();
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
        if (OnPause)
        {
            PauseObJ.SetActive(true);
            Time.timeScale = 0;
            Movescript.OnCamPos = false;
            Txg.gameObject.SetActive(false);
        }
        else if (OnPause == false)
        {
            PauseObJ.SetActive(false);
            Time.timeScale = 1;
            Movescript.OnCamPos = true;
            Txg.gameObject.SetActive(true);
        }
         if (OnPause == false && IA.AgentBool)
        {
            JumpScare(true);
        }
        else
        {
            JumpScare(false);
        }
    }
    public bool JumpScare(bool Jumps)
    {
        AudioSource JumpsC;
        GameObject AudioObj;
        AudioObj = GameObject.Find("IA");
        JumpsC = AudioObj.GetComponent<AudioSource>();
        JumpsC.PlayOneShot(JumpsCares);
        return Jumps;
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
