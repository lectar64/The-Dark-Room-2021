using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static float Times = 300;
    //<public static Text TimerText>;
    public static float Timespeed = 2;
    public Slider SlideXr;
   private void Start()
    {
        SlideXr.maxValue = Times;
        SlideXr.minValue = 0;
    }
    private void Update()
    {   
        SlideXr.value = Times; 
        Times -= Time.deltaTime*Timespeed;
    }
    public void OnDisable()
    {
        Times = 300;
    }
}
