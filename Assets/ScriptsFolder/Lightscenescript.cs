using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightscenescript : MonoBehaviour
{
    public bool Onlight;
    private GameObject Light;
    public Material OnTrueMaterial;
    public Material OnFalseMaterial;
    public Renderer RenderMaterial;
    private int RandomValue;
    private void Awake()
    {
        Onlight = true;
        Light = GameObject.Find("Luz habitacion");
        RenderMaterial = gameObject.GetComponent<Renderer>();
        RandomValue = Random.RandomRange(3,5);
    }
    /*private void Update()
    {
        StartCoroutine(OnlighTrandom());
        if (Onlight)
        {            
            RenderMaterial.material = OnTrueMaterial;
            Light.SetActive(true);
        }
        else
        {
            RenderMaterial.material = OnFalseMaterial;
            Light.SetActive(false);
        }
    }
    IEnumerator OnlighTrandom()
    {
        yield return new WaitForSeconds(RandomValue);
        Onlight = !Onlight;
    }*/
}
