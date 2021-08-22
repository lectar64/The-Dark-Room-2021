using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postprocessing : MonoBehaviour
{
    public static Postprocessing ints;// Singleton o instancia indestructible en cada escena
    private void Awake()//Solucionado con posibles arreglos, incompleto//Funciona ;) 
    {
        if (ints == null)
        {
            ints = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
