using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material Default;
    public Material ChangeM;
    public Renderer Render;
    public bool MaterialBool;
    private void Awake()
    {
        Render = gameObject.GetComponent<Renderer>();
    }
    private void Update()
    {
        Change();
    }
    public void Change()
    {
    }
}
