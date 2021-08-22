using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UwUEngine;

public class Menu : MonoBehaviour
{
    //Canvas
    public Canvas CanvasMainMenu;
    public Canvas CCanvasOptions;
    public Canvas GameSelector;
    public Canvas Credits;
    private GameObject Options;
    //Component Options
    public enum StateMenu {GameTitle,Options,GameSelector,Credits,Quit};
    private StateMenu St_GameMenuManagement;
    public Slider SliderVGA;// Slider Para Los Graficos 
    public int SaveConf;
    public static string[] GraficosArrays = { "Very Low", "Low", "Medium", "High" };
    private GameObject Handle;
    private Dictionary<string, bool>Lenguage = new Dictionary<string, bool>();

    private void Awake()
    {
        //LenguageManager(true);
        Lenguage.Add("EN", false);
        Lenguage.Add("ESP", false);
        SliderVGA.value = PlayerPrefs.GetFloat("ValueHandle", SliderVGA.value);
        Handle = GameObject.Find("Handle");
        SaveConf = PlayerPrefs.GetInt("Save");
        QualitySettings.SetQualityLevel(SaveConf);
        CanvasMainMenu = CanvasMainMenu.GetComponent<Canvas>();
        CCanvasOptions = CCanvasOptions.GetComponent<Canvas>();
        St_GameMenuManagement = StateMenu.GameTitle;
        Options = GameObject.Find("Options");
    }
    private void Update()
    {
        if (St_GameMenuManagement == StateMenu.GameTitle)
        {
            CanvasMainMenu.enabled = true;
            CCanvasOptions.enabled = false;
            Credits.enabled = false;
        }
        else if (St_GameMenuManagement == StateMenu.Credits){
            CanvasMainMenu.enabled = false;
            CCanvasOptions.enabled = false;
            Credits.enabled = true;
        }
        else if (St_GameMenuManagement == StateMenu.Options)
        {
            Text Options_txt;
            CCanvasOptions.enabled = true;
            CanvasMainMenu.enabled = false;
            Credits.enabled = false;
            if (SliderVGA.value == 0)
            {
                Options_txt = Options.GetComponent<Text>();
                PlayerPrefs.SetFloat("ValueHandle", SliderVGA.value);
                PlayerPrefs.SetInt("Save",0);
                SaveConf = PlayerPrefs.GetInt("Save");
                QualitySettings.SetQualityLevel(SaveConf);
                Options_txt.text = "Options : Very Low";
            }
            else if (SliderVGA.value > 0.2)
            {
                Options_txt = Options.GetComponent<Text>();
                PlayerPrefs.SetFloat("ValueHandle", SliderVGA.value);
                PlayerPrefs.SetInt("Save", 1);
                SaveConf = PlayerPrefs.GetInt("Save");
                QualitySettings.SetQualityLevel(SaveConf);
                Options_txt.text = "Options : Low";
            }
             if(SliderVGA.value > 0.4) 
            {
                Options_txt = Options.GetComponent<Text>();
                PlayerPrefs.SetFloat("ValueHandle", SliderVGA.value);
                PlayerPrefs.SetInt("Save", 2);
                SaveConf = PlayerPrefs.GetInt("Save");
                QualitySettings.SetQualityLevel(SaveConf);
                Options_txt.text = "Options : Medium";
            }
            else if (SliderVGA.value > 0.6)
            {
                Options_txt = Options.GetComponent<Text>();
                PlayerPrefs.SetFloat("ValueHandle", SliderVGA.value);
                PlayerPrefs.SetInt("Save", 3);
                SaveConf = PlayerPrefs.GetInt("Save");
                QualitySettings.SetQualityLevel(SaveConf);
                Options_txt.text = "Options : High";
            }
            if (SliderVGA.value > 0.8)
            {
                Options_txt = Options.GetComponent<Text>();
                PlayerPrefs.SetFloat("ValueHandle", SliderVGA.value);
                PlayerPrefs.SetInt("Save", 4);
                SaveConf = PlayerPrefs.GetInt("Save");
                QualitySettings.SetQualityLevel(SaveConf);
                Options_txt.text = "Options : Very High";
            }
            else if (SliderVGA.value > 1)
            {
                Options_txt = Options.GetComponent<Text>();
                PlayerPrefs.SetFloat("ValueHandle", SliderVGA.value);
                PlayerPrefs.SetInt("Save", 6);
                SaveConf = PlayerPrefs.GetInt("Save");
                QualitySettings.SetQualityLevel(SaveConf);
                Options_txt.text = "Options : Ultra";
            }
        }
    }
    /*public bool LenguageManager(bool lenguage)//Necesita que el Dictionary guarde las varibles booleanas para funcionar 31/07/21:17:00 En Trabajo
    {
        if (lenguage != false)
        {
            int TextIntegerManager = 0;
            int LengthValue = 6;
            List<Text> TextManagement = new List<Text>(LengthValue);
            while(TextIntegerManager <= 10)
            {
                TextIntegerManager++;
                TextManagement[TextIntegerManager] = FindObjectOfType<Text>();
            }
        }
        else
        {

        }
        return lenguage;
    }*/
    public void Button(string MethodName) { SendMessage(MethodName); }
     void GameSelectorRoom() { St_GameMenuManagement = StateMenu.GameSelector; }//Selector;Default
     void CanvasOptions(){
        St_GameMenuManagement = StateMenu.Options;
     }//Options;Default
    void MainMenu(){St_GameMenuManagement = StateMenu.GameTitle; }//Selector;Default
    void Creditss(){St_GameMenuManagement = StateMenu.Credits;}
   void QuitApplication() { Application.Quit(); }
    public void LoadScene(string NameScene)
    {
        SceneManager.LoadSceneAsync(NameScene);
    }
}
